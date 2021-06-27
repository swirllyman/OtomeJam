using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatObject : MonoBehaviour
{
    public TMP_Text myNameText;
    public TMP_Text myChatText;
    public Image myImage;
    private void Start()
    {
        transform.localScale = myChatText.GetPreferredValues(myChatText.text);
    }
}
