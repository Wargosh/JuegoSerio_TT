using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoadImage : MonoBehaviour
{
    Image img;
    private void Awake()
    {
        img = GetComponent<Image>();
    }

    public void ShowImage(string image)
    {
        if (image.Contains("http"))
            LoadImageFromURL(image);
        else
            LoadImageFromResources(image);
    }

    private void LoadImageFromURL (string url) 
    {
        StartCoroutine(LoadImg(url));
    }

    private void LoadImageFromResources(string image)
    {
        img.sprite = Resources.Load<Sprite>("Images/User/" + image);
    }

    IEnumerator LoadImg(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            img.sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0, 0));
        }
    }
}
