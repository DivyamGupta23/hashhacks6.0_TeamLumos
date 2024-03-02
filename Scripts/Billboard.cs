using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Billboard : MonoBehaviour
{
    public Material material;
    public Texture2D savedTexture_renderer1;
    public string imageUrl = "";
    public Button updateButton;
    // Start is called before the first frame update
    void Start()
    {
        updateButton.onClick.AddListener(StartDownload);
        StartDownload();
    }

    // Update is called once per frame
    public void SetTexture()
    {
        material.mainTexture = savedTexture_renderer1;
    }

    public void StartDownload()
    {
        StartCoroutine(DownloadImage(SetTexture));
    }

    public IEnumerator DownloadImage(Action a)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return www.SendWebRequest();
        Debug.Log(imageUrl);
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to download image: " + www.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            savedTexture_renderer1 = texture;
            //targetRenderer.material.mainTexture = texture;
            a();
        }
    }
}
