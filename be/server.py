# -*- coding: utf-8 -*-

import os
import traceback
import requests

from flask import Flask, jsonify, request, send_file, send_from_directory
from flask_cors import CORS

import io

from PIL import Image, ImageDraw

app = Flask(__name__)
CORS(app)

resources = {}


@app.route('/resource', methods=['GET', 'POST'])
def handle_resource():
    if request.method == 'POST':
        try:
            rid = request.form['rid']
            dir = app.root_path + '/resources/' + rid
            resource = request.files['resource']

            if not os.path.exists(dir):
                os.makedirs(dir)

            resource.save(dir + '/' + resource.filename)
            resources[rid] = resource.filename
            return jsonify({
                "success": True,
                "rid": rid,
            })
        except Exception as e:
            traceback.print_exc()
            return jsonify({
                "error_msg": "fail to upload a resource {}: {}".format(rid, e)
            })
    elif request.method == 'GET':
        try:
            rid = request.args.get('rid')
            dir = app.root_path + '/resources/' + rid

            if rid not in resources:
                return jsonify({
                    "error_msg": "fail to find a resource {}".format(rid)
                }
                )

            """
            return send_file(dir + "/" + resources[rid],
                             attachment_filename=resources[rid],
                             as_attachment=True)
            """
            return send_file(dir + "/" + resources[rid])
        except Exception as e:
            traceback.print_exc()
            return jsonify({
                "error_msg": "fail to download a resource {}: {}".format(rid, e)
            })


@app.route('/game/<rid>', methods=['GET'])
def get_resource(rid):
    print("Resource ID = ", rid)

    try:
        imgb = requests.get('http://localhost:5000/resource?rid={}'.format(rid)).content;
        stream = io.BytesIO(imgb)
        img = Image.open(stream)
        img.save("test.png")
        return jsonify({
            "success": True,
        })
    except Exception as e:
        traceback.print_exc()
        return jsonify({
            "error_msg": "fail to get a resource for {}: {}".format(rid, e)
        })


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000)
