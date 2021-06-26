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
    public GameObject notification;
    public bool readNextLine = true;
    string choiceResult = "";
    string choice1 = "";
    string choice2 = "";
    Image image = null;
    DayManager timeTracker;
    textFileHolder textHolder;
    enum txtType { DIALOGUE, CHOICE, NEWS };
    string currentChoiceId = "";
    txtType currentType;
    bool hotKeyHit = false;
    string newsGameName;

    void Awake()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        textHolder = manager.GetComponent<textFileHolder>();
        timeTracker = manager.GetComponent<DayManager>();
        timeTracker.getTimeTracker().AddListener(startMainText);
        timeTracker.getDayChange().AddListener(onDayChange);
    }

    public void startMainText(dayPhases phase)
    {
        switch (phase)
        {
            case dayPhases.MORNING:
                if (channel == 1)
                {
                    StartCoroutine(waitForText());
                    if (gameObject.transform.GetChild(0).gameObject.activeSelf == false)
                    {
                        notification.SetActive(true);
                    }
                    else
                    {
                        notification.SetActive(false);
                    }
                }
                break;
            case dayPhases.AFTERNOON:
                if (channel == 2 || channel == 3 || channel == 4)
                {
                    StartCoroutine(waitForText());
                    if (gameObject.transform.GetChild(0).gameObject.activeSelf == false)
                    {
                        notification.SetActive(true);
                    }
                    else
                    {
                        notification.SetActive(false);
                    }

                }
                break;
            case dayPhases.NIGHT:
                Debug.Log("testing night");
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            hotKeyHit = true;
        }
    }
    IEnumerator waitForText()
    {
        string[] splitDialogue;
        if (path != "")
        {
            foreach (string line in File.ReadLines(path))
            {
                Debug.Log(gameObject + "am I working");
                if (hotKeyHit == true)
                {
                    continue;
                }
                yield return new WaitUntil(() => readNextLine == true);

                splitDialogue = line.Split('|');

                if (splitDialogue[0].Contains("@:"))
                {
                    currentType = txtType.DIALOGUE;
                }
                else if (splitDialogue[0].Contains("*:"))
                {
                    currentType = txtType.CHOICE;
                }
                else if (splitDialogue[0].Contains("~:" + currentChoiceId))
                {

                    currentType = txtType.NEWS;
                }
                else if (splitDialogue[0].Contains("?/"))
                {
                    break;
                }
                settingState(splitDialogue);
            }
            hotKeyHit = false;
            if (channel == 1)
            {
                yield return new WaitForSeconds(4f);
                timeTracker.night();
            }
            else if (channel == 3 || channel == 4)
            {
                timeTracker.night();
            }
        }
    }
    void settingState(string[] splitDialogue)
    {
        Image icon = null;
        if (currentChoiceId.Contains(splitDialogue[1]) || splitDialogue[1].Contains("0"))
        {
            switch (currentType)
            {
                case txtType.DIALOGUE:
                    readNextLine = false;
                    if (splitDialogue[2].Contains("CanOfCorn"))
                    {
                        icon = corn.getIcon();
                    }
                    else if (splitDialogue[2].Contains("PockyDaze"))
                    {
                        icon = pocky.getIcon();
                    }
                    splitDialogue[3] = splitDialogue[3].Replace("<he/she/they>", "she");
                    splitDialogue[3] = splitDialogue[3].Replace("<pnm>", "Kaine");
                    settingText(splitDialogue[3], splitDialogue[2], icon);
                    break;
                case txtType.CHOICE:
                    readNextLine = false;
                    GameObject button1 = choice1Button.gameObject.transform.parent.gameObject;
                    GameObject button2 = choice2Button.gameObject.transform.parent.gameObject;

                    button1.SetActive(true);
                    button2.SetActive(true);

                    button1.GetComponent<Button>().onClick.RemoveAllListeners();
                    button2.GetComponent<Button>().onClick.RemoveAllListeners();

                    button1.GetComponent<Button>().onClick.AddListener(delegate { taskOnClick(splitDialogue[2], splitDialogue[4], button1, button2); });
                    button2.GetComponent<Button>().onClick.AddListener(delegate { taskOnClick(splitDialogue[3], splitDialogue[5], button1, button2); });

                    settingChoices(splitDialogue);
                    break;
                case txtType.NEWS:

                    break;
            }
        }
    }
    void taskOnClick(string choice, string textToSend, GameObject button1, GameObject button2)
    {
        button1.SetActive(false);
        button2.SetActive(false);
        currentChoiceId = choice;
        settingText(textToSend, "Player Name", image);

    }
    void settingChoices(string[] splitDialogue)
    {
        choice1 = splitDialogue[4];
        choice2 = splitDialogue[5];
        choice1Button.text = choice1;
        choice2Button.text = choice2;
    }
    void settingText(string msgText, string name, Image icon)
    {
        sendNextText(msgText, name, icon);
    }
    public void sendNextText(string textToSend, string textName, Image icon)
    {
        dialogueEditter.creatingMessage(textToSend, textName, icon);
    }

    void onDayChange(int day)
    {
        switch (channel)
        {
            case 1:
                path = (string)textHolder.mainServerText[day];
                break;
            // case 2:
            //     path = (string)textHolder.newsText[newsGameName];
            //     break;
            case 3:
                path = (string)textHolder.canDmText[day];
                break;
            case 4:
                path = (string)textHolder.pokyDmText[day];
                break;
        }

    }
}