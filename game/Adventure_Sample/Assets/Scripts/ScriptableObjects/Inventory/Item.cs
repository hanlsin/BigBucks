using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

// This simple script represents Items that can be picked
// up in the game.  The inventory system is done using
// this script instead of just sprites to ensure that items
// are extensible.
[CreateAssetMenu]
public class Item : ScriptableObject
{
    public Sprite sprite;

    public void Awake()
    {
        Debug.Log("Item: Awake: " + this.sprite.name);
        if (this.sprite.name == "Coffee")
        {
            LoadResource.instance.ChangeSprite("1", this.SetSprite);
        }
    }

    public void SetSprite(Sprite sprite)
    {
        if (sprite != null)
        {
            this.sprite = sprite;
        }
        else
        {
            // ignore
            return;
        }
    }
}

public class LoadResource : MonoBehaviour
{
    private static LoadResource minstance;
    public static LoadResource instance
    {
        get
        {
            if (minstance == null)
            {
                GameObject obj = new GameObject();
                minstance = obj.AddComponent<LoadResource>();
            }
            return minstance;
        }
    }

    public void ChangeSprite(string rid, System.Action<Sprite> act)
    {
        StartCoroutine(GetRequest(rid, act));
    }


    private IEnumerator GetRequest(string rid, System.Action<Sprite> act)
    {
        string url = "http://localhost:5000/resource?rid=" + rid;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                try
                {
                    byte[] pngBytes = webRequest.downloadHandler.data;
                    int len = pngBytes.Length;
                    Debug.Log("Got the image: size=" + len);

                    Texture2D texture = new Texture2D(512, 512);
                    texture.LoadImage(pngBytes);

                    Sprite sprite = Sprite.Create(texture,
                        new Rect(0, 0, texture.width, texture.height),
                        new Vector2(0, 0), 100);
                    act(sprite);
                } catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
        }
    }
}
