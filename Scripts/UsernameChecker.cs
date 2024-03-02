using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UsernameChecker : MonoBehaviour
{
    public string serverURL = "https://twis.in/shop/hackdata/hack.php"; // Replace with your PHP script URL
    public string usernameToCheck;
    public string response;
    public SaveName saveName;
    public GameObject spinner;
    public bool isUsernameAvailable = false;

    IEnumerator CheckUsername()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameToCheck);
        spinner.SetActive(true);
        using (UnityWebRequest www = UnityWebRequest.Post(serverURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                response = www.downloadHandler.text;
                isUsernameAvailable = response == "Username saved successfully.";
                saveName.confirmationText.text = response;
                spinner.SetActive(false);
                Debug.Log("Username availability: " + response);
                if (isUsernameAvailable)
                {
                    saveName.confirmationText.color = Color.green;
                    saveName.nextButton.interactable = true;
                    PlayerPrefs.SetString("PlayerName", saveName.playerName);
                }
                else
                {
                    saveName.confirmationText.color = Color.red;
                    saveName.nextButton.interactable = false;
                }
            }
        } 
    }
    public void startCheck()
    {
        StartCoroutine(CheckUsername());
    }
}
