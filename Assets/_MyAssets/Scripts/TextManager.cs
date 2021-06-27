using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class TextManager : MonoBehaviour
{
    [SerializeField] private int channel;
    [SerializeField] private DialogueEditter dialogueEditter;
    [SerializeField] string path = "";
    [SerializeField] string[] paths;
    [SerializeField] Characters pocky = new Characters();
    [SerializeField] Characters corn = new Characters();
    [SerializeField] TMPro.TextMeshProUGUI choice1Button;
    [SerializeField] TMPro.TextMeshProUGUI choice2Button;
    [SerializeField] TextManager newsTextManager;
    [SerializeField] TextManager pokyTextManager;
    [SerializeField] TextManager cornTextManager;
    public GameObject dialogueObj;
    public GameObject notification;
    public bool readNextLine = true;
    string choiceResult = "";
    string choice1 = "";
    string choice2 = "";
    Image image = null;
    DayManager timeTracker;
    textFileHolder textHolder;
    enum txtType { DIALOGUE, CHOICE, NEWS, END };
    string currentChoiceId = "";
    int currentNewsId = 1;
    txtType currentType;
    string currentGameChoice;
    static int dayForNews;
    int dayTracker;
    int gameTracker = 0;
    bool newsBeingRead = false;
    static bool isNewsDone = false;
    bool hotKeyHit = false;
    bool doneReading = false;
    bool canPlay = true;
    string[] gameNames = { "flows", "bash", "queen", "cry", "play", "ygg" };

    string newsUpdate = "";
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
        if (path != "")
        {
            switch (phase)
            {
                case dayPhases.MORNING:
                    if (channel == 1 || channel == 2)
                    {
                        newsBeingRead = true;
                        StartCoroutine(waitForText());
                        if (dialogueObj.activeSelf == false)
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
                    if (channel == 2 || channel == 3 || channel == 4 && canPlay == true)
                    {
                        StartCoroutine(waitForText());
                        if (channel == 2)
                        {
                            if (dialogueObj.activeSelf == false)
                            {
                                notification.SetActive(true);
                            }
                            else
                            {
                                notification.SetActive(false);
                            }
                        }
                    }
                    break;
                case dayPhases.NIGHT:
                    Debug.Log("testing night");
                    break;
            }
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
        string[] splitDialogue = null;
        gameTracker = 0;
        if (channel == 2)
        {
            isNewsDone = false;
        }
        yield return new WaitUntil(() => dialogueObj.activeSelf == true);
        if (path != "")
        {
            if (channel == 2 && newsBeingRead)
            {

                for (int i = 1; i <= dayForNews; i++)
                {
                    newsDays newsToRead = (newsDays)textHolder.newsText[i];
                    path = newsToRead.path1;
                    StartCoroutine(readingText(splitDialogue));
                    yield return new WaitUntil(() => doneReading == true);
                    gameTracker++;
                    doneReading = false;
                    path = newsToRead.path2;
                    StartCoroutine(readingText(splitDialogue));
                    yield return new WaitUntil(() => doneReading == true);
                    gameTracker++;
                    doneReading = false;
                }
                isNewsDone = true;
                currentNewsId++;
            }
            else
            {
                if (channel == 3 || channel == 4)
                {
                    yield return new WaitUntil(() => isNewsDone == true);
                    StartCoroutine(readingText(splitDialogue));
                    if (dialogueObj.activeSelf == false)
                    {
                        notification.SetActive(true);
                    }
                    else
                    {
                        notification.SetActive(false);
                    }
                    yield return new WaitUntil(() => doneReading == true);
                }
                else
                {

                    StartCoroutine(readingText(splitDialogue));
                    yield return new WaitUntil(() => doneReading == true);
                    doneReading = false;
                    if (channel == 2 && newsBeingRead == true)
                    {
                        StartCoroutine(waitForText());
                    }
                }
            }
            hotKeyHit = false;
            if (channel == 1)
            {
                yield return new WaitForSeconds(4f);
                timeTracker.afternoon();
            }
            else if (channel == 3 || channel == 4)
            {
                timeTracker.night();
            }
        }
    }

    IEnumerator readingText(string[] splitDialogue)
    {
        foreach (string line in File.ReadLines(path))
        {
            if (channel == 2)
            {

            }
            if (hotKeyHit == true)
            {
                continue;
            }
            yield return new WaitUntil(() => readNextLine == true);
            splitDialogue = line.Split('|');
            if (channel == 2)
            {

            }
            if (splitDialogue[0].Contains("@:"))
            {
                currentType = txtType.DIALOGUE;
            }
            else if (splitDialogue[0].Contains("*:"))
            {
                currentType = txtType.CHOICE;
            }
            else if (splitDialogue[0].Contains("~:" + currentNewsId.ToString()))
            {
                currentType = txtType.NEWS;
            }
            else if (splitDialogue[0].Contains("?/"))
            {
                if (newsUpdate == "")
                {

                }
                else
                {
                    settingText(newsUpdate, "News Bot", null);
                    newsUpdate = "";
                    readNextLine = false;
                }
                continue;
            }
            else if (splitDialogue[0].Contains("!:"))
            {
                currentType = txtType.END;
            }
            else if (splitDialogue[0].Contains("?:/"))
            {
                break;
            }
            else
            {
                continue;
            }
            settingState(splitDialogue);
        }
        doneReading = true;
    }
    void settingState(string[] splitDialogue)
    {
        Image icon = null;
        if (splitDialogue.Length < 2 || currentChoiceId.Contains(splitDialogue[1]) || splitDialogue[1].Contains("0"))
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
                    if (splitDialogue.Length > 4)
                    {
                        ReputationSystem repSystem = new ReputationSystem();
                        if (splitDialogue[4].Contains("C"))
                        {
                            repSystem.addingReputation(corn);
                        }
                        else if (splitDialogue[4].Contains("P"))
                        {
                            repSystem.addingReputation(pocky);
                        }
                    }

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
                    Debug.Log(currentGameChoice + " " + gameNames[gameTracker] + " " + gameTracker);
                    if (currentGameChoice.Contains(gameNames[gameTracker]))
                    {
                        if (newsUpdate != "")
                        {
                            newsUpdate += "\n" + splitDialogue[2];
                        }
                        else
                        {
                            newsUpdate = splitDialogue[2];
                        }
                    }
                    else
                    {
                        if (newsUpdate != "")
                        {
                            splitDialogue[2] = splitDialogue[2].Replace("<mark=#AEDEFB60>", "");
                            splitDialogue[2] = splitDialogue[2].Replace("</mark>", "");
                            newsUpdate += "\n" + splitDialogue[2];
                        }
                        else
                        {
                            splitDialogue[2] = splitDialogue[2].Replace("<mark=#AEDEFB60>", "");
                            splitDialogue[2] = splitDialogue[2].Replace("</mark>", "");
                            newsUpdate = splitDialogue[2];
                        }
                    }
                    break;
                case txtType.END:
                    if (channel == 1)
                    {
                        if (splitDialogue[2].Contains("C"))
                        {
                            cornTextManager.canPlay = true;
                            pokyTextManager.canPlay = false;
                        }
                        else if (splitDialogue[2].Contains("P"))
                        {
                            cornTextManager.canPlay = false;
                            pokyTextManager.canPlay = true;
                        }
                        newsBeingRead = false;
                        dayForNews = dayTracker;
                        newsTextManager.path = (string)textHolder.newsBotText[dayForNews];
                    }
                    if (channel == 2)
                    {
                        currentGameChoice = splitDialogue[2];
                        newsBeingRead = true;
                    }
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
                dayTracker = day;
                break;
            case 2:
                if (currentGameChoice != null)
                {
                    path = (string)textHolder.newsText[currentGameChoice];
                }
                break;
            case 3:
                // path = (string)textHolder.pokyDmText[day];
                break;
            case 4:
                path = (string)textHolder.canDmText[day];
                break;
        }

    }
}