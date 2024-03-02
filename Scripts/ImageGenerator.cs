using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
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
                image.texture = texture;
                image.color = Color.white;
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
        prompt = input.text;
        StartCoroutine(GenerateImageFromText(prompt,image1,spinner1,button1));
        StartCoroutine(GenerateImageFromText(prompt+"2",image2,spinner2,button2));
    }
    public void PickOne()
    {
        gallery.pickedImage.texture = image1.texture;
        gallery.texture = (Texture2D)image1.texture;
        gallery.data = ((Texture2D)image1.texture).GetRawTextureData();
    }   
    public void PickTwo()
    {
        gallery.pickedImage.texture = image2.texture; 
        gallery.texture = (Texture2D)image2.texture;
        gallery.data = ((Texture2D)image2.texture).GetRawTextureData(); 
    }
}
