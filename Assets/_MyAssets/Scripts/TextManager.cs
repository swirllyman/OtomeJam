using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class TextManager : MonoBehaviour
{
    [SerializeField] private DialogueEditter dialogueEditter;
    [SerializeField] string path = "";
    [SerializeField] Characters pocky = new Characters();
    [SerializeField] Characters corn = new Characters();
    [SerializeField] TMPro.TextMeshProUGUI choice1Button;
    [SerializeField] TMPro.TextMeshProUGUI choice2Button;
    public bool readNextLine = true;
    string choiceResult = "";
    string choice1 = "";
    string choice2 = "";
    Image image = null;
    void Start()
    {
        StartCoroutine(waitForText());
    }

    IEnumerator waitForText()
    {
        string textToSend = "";
        string textName = "";
        Image icon = null;
        string[] splitDialogue;
        bool choices = false;
        if (path != "")
        {
            foreach (string line in File.ReadLines(path))
            {
                yield return new WaitUntil(() => readNextLine == true);
                splitDialogue = line.Split('|');
                if (choices == false)
                {
                    if (line.Contains("@:"))
                    {
                        if (splitDialogue[1].Contains("CanOfCorn"))
                        {
                            icon = corn.getIcon();
                        }
                        else if (splitDialogue[1].Contains("PockyDaze"))
                        {
                            icon = pocky.getIcon();
                        }
                        textToSend = splitDialogue[2];
                        textName = splitDialogue[1];
                        readNextLine = false;
                        sendNextText(textToSend, textName, icon);
                    }
                    else if (line.Contains("*:"))
                    {
                        choices = true;
                        choice1 = splitDialogue[1];
                        choice2 = splitDialogue[2];
                        choice1Button.text = choice1;
                        choice2Button.text = choice2;
                        readNextLine = false;
                    }
                    else
                    {
                        textName = "Server Bot";
                        icon = null;
                        textToSend = line;
                        readNextLine = false;
                        sendNextText(textToSend, textName, icon);
                    }
                }
                else
                {
                    if (!line.Contains("?/"))
                    {

                        if (splitDialogue[0].Contains(choiceResult))
                        {
                            Debug.Log(splitDialogue[0] + "  " + choiceResult);
                            if (splitDialogue[1].Contains("CanOfCorn"))
                            {
                                icon = corn.getIcon();
                            }
                            else if (splitDialogue[1].Contains("PockyDaze"))
                            {
                                icon = pocky.getIcon();
                            }
                            textToSend = splitDialogue[2];
                            textName = splitDialogue[1];
                            readNextLine = false;
                            sendNextText(textToSend, textName, icon);
                        }
                    }
                    else
                    {
                        choices = false;
                    }
                }
            }
        }
    }
    public void submitAnswer(string x)
    {
        choiceResult = x;
        if (x.Contains("A"))
        {
            sendNextText(choice1, "Player Name", image);
        }
        else if (x.Contains("B"))
        {
            sendNextText(choice2, "Player Name", image);
        }
    }
    public void sendNextText(string textToSend, string textName, Image icon)
    {
        dialogueEditter.creatingMessage(textToSend, textName, icon);
    }
}
