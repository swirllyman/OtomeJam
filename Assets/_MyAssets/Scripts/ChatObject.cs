using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatObject : MonoBehaviour
{
    public TMP_Text myNameText;
    public TMP_Text myChatText;
    public Image myIcon;
    public Sprite myImage;
    public Sprite mySprite;
    public Image myMask;
    public Sprite bot;
    public void changeSize()
    {
        RectTransform height = myChatText.canvas.GetComponent<RectTransform>();
        height.sizeDelta = new Vector2(980.7f, myChatText.preferredHeight);
        myChatText.rectTransform.sizeDelta = height.sizeDelta;
        RectTransform chatObjectRt = gameObject.GetComponent<RectTransform>();
        if ((myChatText.preferredHeight + 46) < chatObjectRt.sizeDelta.y)
        {

        }
        else
        {
            chatObjectRt.sizeDelta = new Vector2(1100f, (myChatText.preferredHeight + 46));
            gameObject.GetComponent<RectTransform>().sizeDelta = chatObjectRt.sizeDelta;
        }
        if (myImage == bot)
        {
            myMask.sprite = myImage;
            myIcon.sprite = myImage;
        }
        else
        {
            myIcon.sprite = myImage;
        }
    }
}
