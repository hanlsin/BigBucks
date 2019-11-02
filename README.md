# BigBucks

## Backend
Backend supports REST API using Flask.

To run the backend server, you need to prepare virtual environment. 'requirements.txt' would be provided later.

### Prepare Environment
```bash
pip install pipenv
cd be/
pipenv shell
pipenv install
```

### Run Server
```bash
python server.py
```

## Frontend
Currently, the frontend is just a individual html file. So, click the `upload.html` or open the file on any browser.

### Input Fields
* resource ID
  * all files are distinguished by this ID.
* choose file
  * image file which will be downloaded and be shown as an asset in a game

## Smart Contract
TBD