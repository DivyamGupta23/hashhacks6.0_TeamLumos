using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class FetchPlayerName : MonoBehaviour
{
    public string playerName;
    // Start is called before the first frame update
    void Start()
    {
        playerName = string.Concat(PlayerPrefs.GetString("PlayerName", "Unkown").Where(c => !char.IsWhiteSpace(c))).ToLower();
    }


}
