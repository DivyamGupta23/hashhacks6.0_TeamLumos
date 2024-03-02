using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatMessag : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;

    public void SetText(string str)
    { messageText.text = str; }
}
