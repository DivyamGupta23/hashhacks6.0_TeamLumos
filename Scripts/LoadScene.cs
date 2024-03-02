using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    
    public void Load(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    public void SetupDone()
    {
        PlayerPrefs.SetInt("setUpComplete", 2);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void LoadNameScene()
    {
        if (PlayerPrefs.GetInt("setUpComplete", 0) < 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("AppleScene");
        }
        else
        {
            Debug.Log("Setup already complete");
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }

    }
}
