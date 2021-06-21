using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class TextManager : MonoBehaviour
{
    [SerializeField] private int channel;
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
    DayManager timeTracker;
    void Start()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        timeTracker = manager.GetComponent<DayManager>();
        timeTracker.getTimeTracker().AddListener(startMainText);
    }

    public void startMainText(dayPhases phase)
    {
        switch (phase)
        {
            case dayPhases.MORNING:
                if (channel == 1 || channel == 2)
                {
                    StartCoroutine(waitForText());
                }
                break;
            case dayPhases.AFTERNOON:
                if (channel == 2 || channel == 3 || channel == 4)
                {
                    StartCoroutine(waitForText());
                }
                Debug.Log("testing afternoon");
                break;
            case dayPhases.NIGHT:
                Debug.Log("testing night");
                break;
        }
    }
    IEnumerator waitForText()
    {
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
                        readNextLine = false;
                        settingText(splitDialogue[2], splitDialogue[1], icon);
                    }
                    else if (line.Contains("*:"))
                    {
                        choice1Button.gameObject.transform.parent.gameObject.SetActive(true);
                        choice2Button.gameObject.transform.parent.gameObject.SetActive(true);
                        choices = true;
                        settingChoices(splitDialogue);
                        readNextLine = false;
                    }
                    else
                    {
                        readNextLine = false;
                        settingText(line, "Server Bot", null);
                    }
                }
                else
                {
                    if (!line.Contains("?/"))
                    {

                        if (splitDialogue[0].Contains(choiceResult))
                        {
                            if (splitDialogue[1].Contains("CanOfCorn"))
                            {
                                icon = corn.getIcon();
                            }
                            else if (splitDialogue[1].Contains("PockyDaze"))
                            {
                                icon = pocky.getIcon();
                            }
                            readNextLine = false;
                            settingText(splitDialogue[2], splitDialogue[1], icon);
                        }
                    }
                    else
                    {
                        choices = false;
                    }
                }
            }
            if (channel == 1)
            {
                timeTracker.afternoon();
            }
            else if (channel == 3 || channel == 4)
            {
                timeTracker.night();
            }
        }
    }

    void settingChoices(string[] splitDialogue)
    {
        choice1 = splitDialogue[1];
        choice2 = splitDialogue[2];
        choice1Button.text = choice1;
        choice2Button.text = choice2;
    }
    void settingText(string msgText, string name, Image icon)
    {
        sendNextText(msgText, name, icon);
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
        choice1Button.gameObject.transform.parent.gameObject.SetActive(false);
        choice2Button.gameObject.transform.parent.gameObject.SetActive(false);
    }
    public void sendNextText(string textToSend, string textName, Image icon)
    {
        dialogueEditter.creatingMessage(textToSend, textName, icon);
    }
}
