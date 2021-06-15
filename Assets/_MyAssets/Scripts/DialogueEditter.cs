using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEditter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject chatMessage;
    [SerializeField] private TextManager textManager;
    private GameObject currentText;
    private GameObject pastText;
    bool firstAttached = true;
    void Start()
    {
        // StartCoroutine(waitToAdjust(1.0f));
        // Debug.Log("I am hopg");
    }
    // IEnumerator waitToAdjust(float waitTime)
    // {

    // }
    public void creatingMessage(string textToRecieve, string textName, Image icon)
    {
        chatMessage.GetComponent<ChatObject>().myChatText.text = textToRecieve;
        chatMessage.GetComponent<ChatObject>().myImage = icon;
        chatMessage.GetComponent<ChatObject>().myNameText.text = textName;
        if (currentText == null)
        {
            currentText = Instantiate(chatMessage, new Vector3(5, -30, 0), Quaternion.identity, gameObject.transform);
            textManager.readNextLine = true;
        }
        else
        {
            attachingToPastText();
        }
    }
    void attachingToPastText()
    {
        if (firstAttached == true)
        {
            Debug.Log("in line 50");
            firstAttached = false;
            moveAllPastText();
        }
        else
        {
            pastText.transform.SetParent(currentText.transform);
            Invoke("moveAllPastText", 2.0f);
        }
    }
    int counter = 0;
    void moveAllPastText()
    {
        currentText.transform.position = currentText.transform.position + new Vector3(0, 12, 0);
        GameObject newMessage = Instantiate(chatMessage, new Vector3(5, -30, 0), Quaternion.identity, gameObject.transform);
        pastText = currentText;
        currentText = newMessage;
        if (counter == 3)
        {

        }
        else
        {

            Debug.Log(counter);
            textManager.readNextLine = true;
            counter++;
        }
    }
}
