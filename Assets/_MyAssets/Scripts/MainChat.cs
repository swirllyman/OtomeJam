using UnityEngine;
using TMPro;

public class MainChat : MonoBehaviour
{
    public GameObject chatPrefab;

    public TMP_InputField inputField;
    public Transform chatGroupParent;

    public void SubmitChat()
    {
        NewChat(GameManager.playerName, GameManager.playerIcon, inputField.text);
        inputField.text = "";
    }


    public void NewChat(string playerName, Sprite playerSprite, string chat)
    {
        GameObject newChatObject = Instantiate(chatPrefab, chatGroupParent);
        ChatObject chatObject = newChatObject.GetComponent<ChatObject>();
        chatObject.myNameText.text = playerName;
        chatObject.myImage.sprite = playerSprite;
        chatObject.myChatText.text = chat;
    }
}
