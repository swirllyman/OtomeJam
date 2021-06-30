using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class TextManager : MonoBehaviour
{
    [SerializeField] private int channel;
    [SerializeField] private DialogueEditter dialogueEditter;
    [SerializeField] private DialogueEditter dialogueEditterEndGame;
    [SerializeField] string path = "";
    [SerializeField] string[] paths;
    [SerializeField] Characters pocky = new Characters();
    [SerializeField] Characters corn = new Characters();
    [SerializeField] TMPro.TextMeshProUGUI choice1Button;
    [SerializeField] TMPro.TextMeshProUGUI choice2Button;
    [SerializeField] TextManager mainTextManager;
    [SerializeField] TextManager newsTextManager;
    [SerializeField] TextManager pokyTextManager;
    [SerializeField] TextManager cornTextManager;
    [SerializeField] Sprite botImage;
    [SerializeField] Sprite flowsImage;
    [SerializeField] Sprite bashImage;
    [SerializeField] Sprite queenImage;
    [SerializeField] Sprite cryImage;
    [SerializeField] Sprite yggImage;
    [SerializeField] Sprite playImage;
    [SerializeField] EndOfGame endOfGame;
    [SerializeField] GameObject endOfGameObj;
    [SerializeField] GameObject loginObj;
    [SerializeField] GameObject serverPanel;
    [SerializeField] MainChat mainChat;
    [SerializeField] ReputationSystem repSystem;
    public GameObject dialogueObj;
    public GameObject notification;
    public bool readNextLine = true;
    string choiceResult = "";
    string choice1 = "";
    string choice2 = "";
    Sprite image = null;
    DayManager timeTracker;
    textFileHolder textHolder;
    GameManager gameManager;
    enum txtType { DIALOGUE, CHOICE, NEWS, END, ENDGAME };
    string currentChoiceId = "";
    int currentNewsId = 1;
    txtType currentType;
    string currentGameChoice;
    string mainGameChoice;
    static int dayForNews;
    int dayTracker;
    int gameTracker = 0;
    bool newsBeingRead = false;
    static bool isNewsDone = false;
    bool hotKeyHit = false;
    public bool doneReading = false;
    bool canPlay = true;
    string[] gameNames = { "flows", "bash", "queen", "cry", "play", "ygg" };
    public Sprite[] mySelfies;
    string newsUpdate = "";
    [SerializeField] Sprite[] creditImages;
    void Awake()
    {
        // repSystem = new ReputationSystem();
        GameObject manager = GameObject.FindWithTag("Manager");
        gameManager = manager.GetComponent<GameManager>();
        textHolder = manager.GetComponent<textFileHolder>();
        timeTracker = manager.GetComponent<DayManager>();
        timeTracker.getTimeTracker().AddListener(startMainText);
        timeTracker.getDayChange().AddListener(onDayChange);

    }

    public void startMainText(dayPhases phase)
    {
        if (timeTracker.day == 4 && channel == 1)
        {
            path = "Assets/_MyAssets/TextFiles/finalGame.txt";
        }
        if (timeTracker.day == 4 && channel != 1)
        {
            if (channel == 3 || channel == 4)
            {
                if (canPlay == true)
                {
                    mainChat.EnablePanel(1);
                    isNewsDone = true;
                    StartCoroutine(waitForText());
                }
            }
        }
        else if (path != null)
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
                        if (channel == 2 || channel == 3 || channel == 4)
                        {
                            if (canPlay == true)
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
                        }
                        break;
                    case dayPhases.NIGHT:
                        Debug.Log("testing night");
                        break;
                    case dayPhases.END:
                        Debug.Log("Am i here");
                        if (channel == 3 || channel == 4)
                        {
                            if (canPlay == true)
                            {
                                mainChat.EnablePanel(1);
                                dialogueObj.SetActive(true);
                                isNewsDone = true;
                                StartCoroutine(waitForText());
                            }
                        }

                        break;
                }
            }
        }
    }
    IEnumerator EndTheGame()
    {

        //endOfGameObj.SetActive(true);
        endOfGame.startFadeOut();
        int counter = 0;
        string[] splitDialogue = null;
        yield return new WaitUntil(() => Input.GetKeyDown("space"));
        mainChat.EnablePanel(0);
        endOfGameObj.SetActive(true);
        yield return new WaitForSeconds(2f);
        splitDialogue = "ichika senpai#0422|Chloe Monet - Narrative Designer, Writer (CanOfCorn and NewsBot), Server Icon Artist \n \n Website: https://cmnakano.wixsite.com/portfolio \n Itch.io: https://bunnygirlsenpai.itch.io/ \n LinkedIN: https://www.linkedin.com/in/chloe-nakano-569154142/".Split('|');
        dialogueEditterEndGame.creatingMessage(splitDialogue[1], splitDialogue[0], creditImages[counter]);
        counter++;
        yield return new WaitForSeconds(2f);
        splitDialogue = "KingToDeath#9968|E.Sterling Sylvester - Writer (PockyDaze) \n \n Twitter: https://twitter.com/KingToDeath".Split('|');
        dialogueEditterEndGame.creatingMessage(splitDialogue[1], splitDialogue[0], creditImages[counter]);
        counter++;
        yield return new WaitForSeconds(2f);
        splitDialogue = "Swirllyman#1778| Joe Greive - UI Programmer \n  \n Website: https://swirllyman.itch.io/".Split('|');
        dialogueEditterEndGame.creatingMessage(splitDialogue[1], splitDialogue[0], creditImages[counter]);
        counter++;
        yield return new WaitForSeconds(2f);
        splitDialogue = "TheAugustGuy#4521| August Lay - Game Designer, Programmer \n LinkedIN: https://www.linkedin.com/in/august-lay/".Split('|');
        dialogueEditterEndGame.creatingMessage(splitDialogue[1], splitDialogue[0], creditImages[counter]);
        counter++;
        yield return new WaitForSeconds(2f);
        splitDialogue = "kroobez#8747| Greg Morton - UI Designer \n \n Website: https://greg-morton.squarespace.com/ \n Itch.io: https://itch.io/brontosaurus-g \n LinkedIn: https://linkedin.com/in/greg-morton-rgm/".Split('|');
        dialogueEditterEndGame.creatingMessage(splitDialogue[1], splitDialogue[0], creditImages[counter]);
        counter++;
        yield return new WaitForSeconds(2f);
        splitDialogue = "Awiola#9027| Awiola - Character Artist, Icon Artist \n \n Twitter: https://twitter.com/realAwiola".Split('|');
        dialogueEditterEndGame.creatingMessage(splitDialogue[1], splitDialogue[0], creditImages[counter]);
        counter++;
        yield return new WaitForSeconds(2f);
        splitDialogue = "EvanJames#9199| Evan James - Background Music, SFX \n \n Website: https://www.evanjamesaudio.com/ \n Twitter: https://twitter.com/EvanJamesAudio".Split('|');
        dialogueEditterEndGame.creatingMessage(splitDialogue[1], splitDialogue[0], creditImages[counter]);
        counter++;
        yield return new WaitForSeconds(6f);
        serverPanel.SetActive(false);
        loginObj.SetActive(true);
    }
    IEnumerator waitForText()
    {
        string[] splitDialogue = null;
        gameTracker = 0;
        if (channel == 2)
        {
            isNewsDone = false;
        }
        if (channel == 3 || channel == 4)
        {
            yield return new WaitUntil(() => isNewsDone == true);
            if (dialogueObj.activeSelf == false)
            {
                notification.SetActive(true);
            }
            else
            {
                notification.SetActive(false);
            }
        }
        yield return new WaitUntil(() => dialogueObj.activeSelf == true);
        if (path != "")
        {
            if (channel == 2 && newsBeingRead)
            {

                for (int i = 1; i <= dayForNews; i++)
                {
                    Debug.Log(dayForNews);
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
                newsBeingRead = false;
                isNewsDone = true;
                currentNewsId++;
            }
            else
            {
                if (channel == 3 || channel == 4)
                {
                    Debug.Log(isNewsDone + " is News Done");
                    yield return new WaitUntil(() => isNewsDone == true);
                    if (dialogueObj.activeSelf == false)
                    {
                        notification.SetActive(true);
                    }
                    else
                    {
                        notification.SetActive(false);
                    }
                    StartCoroutine(readingText(splitDialogue));
                    yield return new WaitUntil(() => doneReading == true);
                    doneReading = false;
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
            if (timeTracker.day != 4)
            {
                if (channel == 1)
                {
                    yield return new WaitForSeconds(4f);
                    timeTracker.afternoon();
                }
                else if (channel == 3 || channel == 4)
                {
                    yield return new WaitForSeconds(4f);
                    timeTracker.night();
                }
            }

        }
    }

    void FinishTheGame()
    {
        StartCoroutine(EndTheGame());
    }
    IEnumerator readingText(string[] splitDialogue)
    {
        foreach (string line in File.ReadLines(path))
        {
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
                    Sprite icon = null;
                    if (splitDialogue[2].Contains("Flows"))
                    {
                        splitDialogue[2] = splitDialogue[2].Replace("Flows", "Flows");
                        icon = flowsImage;
                    }
                    else if (splitDialogue[2].Contains("bash"))
                    {
                        splitDialogue[2] = splitDialogue[2].Replace("bash", "Bash Bros");
                        icon = bashImage;
                    }
                    else if (splitDialogue[2].Contains("Cry"))
                    {
                        splitDialogue[2] = splitDialogue[2].Replace("Cry", "Cry to Responsibility");
                        icon = cryImage;
                    }
                    else if (splitDialogue[2].Contains("queen"))
                    {
                        splitDialogue[2] = splitDialogue[2].Replace("queen", "For the Queen");
                        icon = queenImage;
                    }
                    else if (splitDialogue[2].Contains("play"))
                    {
                        splitDialogue[2] = splitDialogue[2].Replace("play", "Double Plays");
                        icon = playImage;
                    }
                    else if (splitDialogue[2].Contains("ygg"))
                    {
                        splitDialogue[2] = splitDialogue[2].Replace("ygg", "Yggdrasil");
                        icon = yggImage;
                    }
                    settingText(newsUpdate, splitDialogue[2], icon);
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
            else if (splitDialogue[0].Contains("!?:"))
            {
                currentType = txtType.ENDGAME;
            }
            else
            {
                continue;
            }
            settingState(splitDialogue);
        }
        doneReading = true;
        if (channel == 3 || channel == 4)
        {
            if (timeTracker.day == 4)
            {
                mainTextManager.FinishTheGame();
            }
        }
    }
    void settingState(string[] splitDialogue)
    {
        Sprite icon = null;
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
                    else if (splitDialogue[2].Contains("PlayerName"))
                    {
                        splitDialogue[2] = splitDialogue[2].Replace("PlayerName", gameManager.currentPlayerAccount.username);

                        icon = gameManager.allIcons[gameManager.currentPlayerAccount.iconID];
                    }
                    else
                    {
                        icon = botImage;
                    }
                    splitDialogue[3] = splitDialogue[3].Replace("<he/she/they>", gameManager.currentPlayerAccount.pronounID == 0 ? "they" :
                    gameManager.currentPlayerAccount.pronounID == 1 ? "she" : "he");
                    splitDialogue[3] = splitDialogue[3].Replace("<him/her/them>", gameManager.currentPlayerAccount.pronounID == 0 ? "them" :
                    gameManager.currentPlayerAccount.pronounID == 1 ? "her" : "him");
                    splitDialogue[3] = splitDialogue[3].Replace("<pnm>", gameManager.currentPlayerAccount.username);
                    if (splitDialogue.Length > 4)
                    {
                        if (splitDialogue[4].Contains("C"))
                        {
                            repSystem.addingReputation(corn);
                        }
                        else if (splitDialogue[4].Contains("P"))
                        {
                            repSystem.addingReputation(pocky);
                        }
                    }
                    if (splitDialogue[3].Contains("*Image*"))
                    {
                        Debug.Log("testing image");
                        dialogueEditter.creatingImageMsg(mySelfies[int.Parse(currentChoiceId)], splitDialogue[2], icon);
                    }
                    else
                    {
                        settingText(splitDialogue[3], splitDialogue[2], icon);
                    }
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
                case txtType.ENDGAME:
                    if (pocky.getReputation() >= corn.getReputation())
                    {
                        if (pocky.getReputation() >= 17)
                        {
                            currentChoiceId = "3";
                            endOfGame.startEnd(@"You learn PockyDaze's real name, Haruto, and give him yours as well. In your haste to speak and see each other for real, you enter a Zomm chat and talk for hours. He takes the opportunity to show you his cat, Jiji, who is just as talkative as he mentioned before. Eventually you're both pinged by Corn to play Alliance of Myth, but you both decide to ditch out to be with each other. 

Everything seems so different now that it's official. Haruto is so much more energetic and excited when he gets to see you. You get the feeling it takes him a long time to warm up to people. And that makes you pretty damn special. 

You're glad you really took the time to learn about the games and things that Haruto was interested in, to bring you to this point. All the decisions you made worked perfectly in your favor to win his heart. 

 A while later, Haruto invites you a convention and suggest you do a couple cosplay. You agree, tentatively. You're nervous when you meet him for the first time, but everything is exactly like you dreamed it would be. You can't believe the two of you have come this far, and you're also excited to see where all of this will take you. 

The future looks promising, and you're hopeful that this love will last.

FIN.");
                        }
                        else if (pocky.getReputation() <= 16 && pocky.getReputation() >= 13)
                        {
                            currentChoiceId = "2";
                            endOfGame.startEnd(@"You learn PockyDaze's real name, Haruto, and give him yours as well. The two of you spend the rest of the day chatting and hanging out. It’s a good time and he even sends you his latest map for Flows! and you send him your favourite music playlist. He insists on making a beat map you'll enjoy from the songs you shared with him. By the time you’re pinged in the Main Server by Corn to play Alliance of Myth, you’re tempted to ditch out and chill with Haruto longer.

 The rest of the month, you spend more and more time with Haruto. You even send him a few selfies to repay him for the ones he sent you. And you get into a Zomm call with him a few times. Despite how serious and introverted he seems, he's so energetic and excited when he gets to see you. 

 You’re glad you took the time to learn more about the games he played, so you could get the chance to get closer to him like this. You mentally pat yourself on the back for the good choices you made.

 A while later the two of you start dating. Eventually, CanOfCorn decides to leave the server, and while you’re sad to see him go, you’re also secretly happy for more alone time with Haruto. Only time can tell how long your online-relationship will last~

Happy gaming :)

FIN.");
                        }
                        else
                        {
                            currentChoiceId = "1";
                            endOfGame.startEnd(@"Despite the news that PockyDaze is interested in CanOfCorn, you help him figure out what he's going to say. When PockyDaze ends up messaging CanOfCorn, he finds it doesn't take much convincing for CanOfCorn to reciprocate his feelings. You're happy for them, but you're filled with disappointment because he didn’t pick you as his love interest...

The situation leaves you feeling like a third wheel for the remainder of the week, and on into the next. You wonder to yourself if there were better choices you could have made along the way. Maybe then, you’d be the one getting his attention. 

A while later, CanOfCorn and PockyDaze start dating. You decide it’s best to leave the lovebirds alone, and you quietly leave the server.

FIN.");
                        }
                    }
                    else
                    {
                        if (corn.getReputation() >= 17)
                        {
                            currentChoiceId = "3";
                            endOfGame.startEnd(@"You learn CanOfCorns’ real name, Izuki, and give him yours as well. The two of you spend the rest of the day on voice call, chatting about all sorts of random things. You really enjoy spending time with Izuki, and he even sends you more pictures of him, his dog and his hometown. By the time you’re pinged in the Main Server by Pocky to play Alliance of Myth, you both decide to ditch out to be with each other longer.

The rest of the month is a whirlwind of talking and gaming with Izuki -- you even fall asleep on voice call with him a few times. You find his voice soothing and comforting. You’re glad you really took the time to learn about the games and things that Izuki was interested in, to bring you to this point. All the decisions you made worked perfectly in your favor to win his heart.

A while later, you make plans to meet up with Izuki IRL. Eventually, PockyDaze decides to leave the server, and while it’s sad to see him go, you’re also happy for more couple-time with Izuki. You can’t believe the two of you have come this far, and you’re also excited to see where all of this will take you.

The future looks promising, and you’re hopeful that this love will last. 

FIN.");

                        }
                        else if (corn.getReputation() <= 16 && corn.getReputation() >= 13)
                        {
                            currentChoiceId = "2";
                            endOfGame.startEnd(@"You learn CanOfCorns’ real name, Izuki, and give him yours as well. The two of you spend the rest of the day chatting and hanging out. It’s a good time and he even sends you his favorite music playlist. By the time you’re pinged in the Main Server by Pocky to play Alliance of Myth, you’re tempted to ditch out and chill with Izuki longer.

The rest of the month, you spend more and more time with Izuki. You even get into voice call with him a few times -- his voice is as swoony as you thought it would be, and his personality and attitude are as energetic they seem through text. You’re glad you took the time to learn more about the games he played, so you could get the chance to get closer to him like this. You mentally pat yourself on the back for the good choices you made.

A while later the two of you start dating. Eventually, PockyDaze decides to leave the server, and while you’re sad to see him go, you’re also secretly happy for more alone time with Izuki. Only time can tell how long your online-relationship will last~

Happy gaming :)

FIN.");
                        }
                        else
                        {
                            currentChoiceId = "1";
                            endOfGame.startEnd(@"You spend the rest of the day talking to CanOfCorn, giving him relationship advice. At the end, he seems more confident and excited about starting something with Pocky. On the other hand, you're filled with disappointment because he didn’t pick you as his love interest...

The situation leaves you feeling like a third wheel for the remainder of the week, and on into the next. You wonder to yourself if there were better choices you could have made along the way. Maybe then, you’d be the one getting his attention. 

A while later, CanOfCorn and PockyDaze start dating. You decide it’s best to leave the lovebirds alone, and you quietly leave the server.

FIN.");
                        }
                    }
                    break;
                case txtType.END:
                    if (channel == 1)
                    {
                        if (splitDialogue[3].Contains("C"))
                        {
                            Debug.Log("Inside of End");
                            cornTextManager.canPlay = true;
                            pokyTextManager.canPlay = false;
                        }
                        else if (splitDialogue[3].Contains("P"))
                        {
                            cornTextManager.canPlay = false;
                            pokyTextManager.canPlay = true;
                        }
                        mainGameChoice = splitDialogue[2];
                        Debug.Log(mainGameChoice);
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
        else if (splitDialogue[1].Contains("*"))
        {
            if (channel == 1)
            {
                if (splitDialogue[4].Contains(mainGameChoice))
                {
                    currentChoiceId = splitDialogue[2];
                }
                else if (splitDialogue[5].Contains(mainGameChoice))
                {
                    currentChoiceId = splitDialogue[3];
                }
            }
            else if (channel == 3 || channel == 4)
            {
                if (channel == 3)
                {
                    currentChoiceId = repSystem.gettingSelfies(repSystem.checkingReputation(pocky), pocky).ToString();
                }
                else if (channel == 4)
                {
                    currentChoiceId = repSystem.gettingSelfies(repSystem.checkingReputation(corn), corn).ToString();
                }
            }
        }
    }
    void taskOnClick(string choice, string textToSend, GameObject button1, GameObject button2)
    {
        button1.SetActive(false);
        button2.SetActive(false);
        currentChoiceId = choice;
        image = gameManager.allIcons[gameManager.currentPlayerAccount.iconID];
        settingText(textToSend, gameManager.currentPlayerAccount.username, image);

    }
    void settingChoices(string[] splitDialogue)
    {
        choice1 = "A: " + splitDialogue[4];
        choice2 = "B: " + splitDialogue[5];
        choice1Button.text = choice1;
        choice2Button.text = choice2;
    }
    void settingText(string msgText, string name, Sprite icon)
    {
        sendNextText(msgText, name, icon);
    }
    public void sendNextText(string textToSend, string textName, Sprite icon)
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
            // case 2:
            //     if (currentGameChoice != null)
            //     {
            //         path = (string)textHolder.newsText[currentGameChoice];
            //     }
            //     break;
            case 3:
                path = (string)textHolder.pokyDmText[day];
                break;
            case 4:
                path = (string)textHolder.canDmText[day];
                break;
        }

    }
}