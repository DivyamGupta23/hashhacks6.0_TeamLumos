using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class SaveName : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    public string playerName;
    public Button confirmButton;
    public Button nextButton;
    public UsernameChecker test;
    public TMP_Text confirmationText;
    private void Start()
    {
        confirmButton.onClick.AddListener(saveName);
        nextButton.interactable = false;
    }
    private void Update()
    {
        if (inputField.text.Length >= 1)
        {

            confirmButton.interactable= true;
        }
        else confirmButton.interactable = false;
    }
    void saveName()
    {
        if (inputField.text.Length >= 1) {

            playerName = inputField.text;
            test.usernameToCheck = string.Concat(playerName.Where(c => !char.IsWhiteSpace(c))).ToLower();
            test.startCheck();
           
     
           

        }
        
    }
}
