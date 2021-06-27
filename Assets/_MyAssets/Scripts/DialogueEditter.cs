using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEditter : MonoBehaviour
{
    [SerializeField] private GameObject chatMessage;
    [SerializeField] private TextManager textManager;
    private GameObject currentText;
    private GameObject pastText;
    bool firstAttached = true;
    string textToRecieve;
    string textName;
    Image icon;
    public void creatingMessage(string textToRecieve, string textName, Image icon)
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
        pastText = currentText;
        currentText = newMessage;
        textManager.readNextLine = true;
    }
}
