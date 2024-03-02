using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.IO;

public class ImageGenerator : MonoBehaviour
{
    public Renderer targetRenderer;
    public RawImage image1;
    public RawImage image2;
    public string prompt;
    public TMP_InputField input;
    public GameObject spinner1;
    public GameObject spinner2;
    public GameObject button1;
    public GameObject button2;
    public TestGallery gallery;
    public Texture2D baseTexture1;
    public Texture2D baseTexture2;
    public GameObject captionUI;
    public Button sendPostButton;

    public string url;
    byte[] data;
    private void Start()
    {
        sendPostButton.onClick.AddListener(() => { StartCoroutine(UploadUrl(url)); });
    }
    private IEnumerator GenerateImageFromText(string prompt, RawImage image, GameObject spinner, GameObject button)
    {
        button.SetActive(false);
        spinner.SetActive(true);
        string url = $"https://pollinations.ai/prompt/{prompt}";

        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {


            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {   
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
  
                image.color = Color.white;
                image.texture = texture;
                spinner.SetActive(false);
                button.SetActive(true);
            }
            else
            {
                Debug.LogError($"Failed to fetch image. Error: {www.error}");
                spinner.SetActive(false);
            }
        }
    }
    public void Generate()
    {
        prompt = input.text ?? prompt;
        StartCoroutine(GenerateImageFromText(prompt,image1,spinner1,button1));
        StartCoroutine(GenerateImageFromText(prompt+"2",image2,spinner2,button2));
    }
    public void PickOne()
    {
        url = $"https://pollinations.ai/prompt/{prompt}";
        gallery.pickedImage.texture = image1.texture;
        captionUI.SetActive(true);
        gallery._sendPostButton.gameObject.SetActive(false);
        sendPostButton.gameObject.SetActive(true);

        //this.gameObject.SetActive(false);
    }   
    public void PickTwo()
    {
        url = $"https://pollinations.ai/prompt/{prompt}2";
        gallery.pickedImage.texture = image2.texture;
        captionUI.SetActive(true);
        gallery._sendPostButton.gameObject.SetActive(false);
        sendPostButton.gameObject.SetActive(true);
        //this.gameObject.SetActive(false);
    }

    string ConvertSpacesToPercentEncoding(string url)
    {

        string formattedUrl = System.Uri.EscapeUriString(url);

        formattedUrl = formattedUrl.Replace(" ", "%20");
        Debug.Log(formattedUrl);

        return formattedUrl; // Assuming https:// prefix for URLs
    }
    IEnumerator UploadUrl(string url)
    {
        string formattedUrl = ConvertSpacesToPercentEncoding(url);
        WWWForm form = new WWWForm();
        form.AddField("aii", formattedUrl);
        using (UnityWebRequest www = UnityWebRequest.Post(gallery.imageURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {     
                Debug.LogError(www.error);
                Debug.LogError(gallery.imageURL);
   
            }
            else
            {

                Debug.Log("Image uploaded successfully: " + www.downloadHandler.text);
                Debug.Log(gallery.imageURL);
       
            }
        }
    }
}
