using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class TextManager : MonoBehaviour
{
    [SerializeField] private DialogueEditter dialogueEditter;
    string path = "Assets/_MyAssets/TextFiles/Day1.txt";
    string textChoice1;
    string textChoice2;
    string textChoice3;
    ChatObject newChat = new ChatObject();
    [SerializeField] Characters pocky = new Characters();
    [SerializeField] Characters corn = new Characters();
    public bool readNextLine = true;
    void Start()
    {
        StartCoroutine(waitForText());
    }

    IEnumerator waitForText()
    {
        string textToSend = "";
        string textName = "";
        Image icon = null;
        bool isCharacterDialogue = false;
        foreach (string line in File.ReadLines(path))
        {
            yield return new WaitUntil(() => readNextLine == true);
            if (isCharacterDialogue == false)
            {
                if (line.Contains("@:PockyDaze"))
                {
                    textName = pocky.getIgn();
                    icon = pocky.getIcon();
                    isCharacterDialogue = true;
                    continue;
                }
                else if (line.Contains("@:CanOfCorn"))
                {
                    textName = corn.getIgn();
                    icon = corn.getIcon();
                    isCharacterDialogue = true;
                    continue;
                }
                else
                {
                    textName = "Server Bot";
                    icon = null;
                }
            }
            else
            {

            }
            textToSend = line;
            readNextLine = false;
            isCharacterDialogue = false;
            sendNextText(textToSend, textName, icon);
        }
    }
    public void settingUpNextText(int x)
    {

    }

    public void settingUpNextChoices(int x)
    {
        string textChoice1 = "";
        string textChoice2 = "";
        string textChoice3 = "";
        this.textChoice1 = textChoice1;
        this.textChoice2 = textChoice2;
        this.textChoice3 = textChoice3;
    }
    public void sendNextChoices()
    {
        gameObject.GetComponent<TextChoiceManager>().nextChoices(textChoice1, textChoice2, textChoice3);
    }

    public void sendNextText(string textToSend, string textName, Image icon)
    {
        dialogueEditter.creatingMessage(textToSend, textName, icon);
    }
}
