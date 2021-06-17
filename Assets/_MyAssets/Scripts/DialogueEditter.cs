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
    public void creatingMessage(string textToRecieve, string textName, Image icon)
    {
        chatMessage.GetComponent<ChatObject>().myChatText.text = textToRecieve;
        chatMessage.GetComponent<ChatObject>().myImage = icon;
        chatMessage.GetComponent<ChatObject>().myNameText.text = textName;
        if (currentText == null)
        {
            currentText = Instantiate(chatMessage, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
            textManager.readNextLine = true;
        }
        else
        {
            attachingToPastText();
        }
    }
    void attachingToPastText()
    {
        Invoke("moveAllPastText", 2.0f);
    }
    void moveAllPastText()
    {
        currentText.transform.position = currentText.transform.position + new Vector3(0, 0, 0);
        GameObject newMessage = Instantiate(chatMessage, currentText.transform.position + new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
        pastText = currentText;
        currentText = newMessage;
        textManager.readNextLine = true;
    }
}
