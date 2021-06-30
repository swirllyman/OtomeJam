using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEditter : MonoBehaviour
{
    [SerializeField] private GameObject chatMessage;
    [SerializeField] private GameObject imageMessage;
    [SerializeField] private TextManager textManager;
    private GameObject currentText;
    private GameObject pastText;
    bool firstAttached = true;
    string textToRecieve;
    string textName;
    Sprite icon;
    Sprite selfie;
    public void creatingImageMsg(Sprite selfieToUse, string textName, Sprite icon)
    {
        selfie = selfieToUse;
        this.textName = textName;
        this.icon = icon;
        if (currentText == null)
        {
            currentText = Instantiate(imageMessage, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
            currentText.GetComponent<ChatObject>().mySprite.sprite = this.selfie;
            currentText.GetComponent<ChatObject>().myImage = this.icon;
            currentText.GetComponent<ChatObject>().myNameText.text = this.textName;
            currentText.GetComponent<ChatObject>().maskAndImage();
            textManager.readNextLine = true;
        }
        else
        {
            attachingToPastImage();
        }
    }

    void attachingToPastImage()
    {
        Invoke("moveAllPastImage", 0f);
    }

    void moveAllPastImage()
    {
        currentText.transform.position = currentText.transform.position + new Vector3(0, 0, 0);
        GameObject newMessage = Instantiate(imageMessage, currentText.transform.position + new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
        newMessage.GetComponent<ChatObject>().mySprite.sprite = selfie;
        newMessage.GetComponent<ChatObject>().myImage = icon;
        newMessage.GetComponent<ChatObject>().myNameText.text = textName;
        newMessage.GetComponent<ChatObject>().maskAndImage();
        pastText = currentText;
        currentText = newMessage;
        textManager.readNextLine = true;
    }

    public void creatingMessage(string textToRecieve, string textName, Sprite icon)
    {
        this.textToRecieve = textToRecieve;
        this.textName = textName;
        this.icon = icon;
        if (currentText == null)
        {
            currentText = Instantiate(chatMessage, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
            currentText.GetComponent<ChatObject>().myChatText.text = this.textToRecieve;
            currentText.GetComponent<ChatObject>().myImage = this.icon;
            currentText.GetComponent<ChatObject>().myNameText.text = this.textName;
            currentText.GetComponent<ChatObject>().changeSize();
            currentText.GetComponent<ChatObject>().maskAndImage();
            textManager.readNextLine = true;
        }
        else
        {
            attachingToPastText();
        }
    }
    void attachingToPastText()
    {
        Invoke("moveAllPastText", 0.5f);
    }
    void moveAllPastText()
    {
        currentText.transform.position = currentText.transform.position + new Vector3(0, 0, 0);
        GameObject newMessage = Instantiate(chatMessage, currentText.transform.position + new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
        newMessage.GetComponent<ChatObject>().myChatText.text = textToRecieve;
        newMessage.GetComponent<ChatObject>().myImage = icon;
        newMessage.GetComponent<ChatObject>().myNameText.text = textName;
        newMessage.GetComponent<ChatObject>().changeSize();
        newMessage.GetComponent<ChatObject>().maskAndImage();
        pastText = currentText;
        currentText = newMessage;
        textManager.readNextLine = true;
    }
}
