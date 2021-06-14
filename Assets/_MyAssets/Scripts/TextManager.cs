using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class TextManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI testingTxt;
    string path = "Assets/_MyAssets/TextFiles/Day1.txt";
    string textChoice1;
    string textChoice2;
    string textChoice3;
    void Start()
    {
        string nextText = "";
        bool foundCode = false;
        foreach (string line in File.ReadLines(path))
        {
            if (foundCode == true)
            {
                if (line.Contains("#"))
                {
                    break;
                }
                nextText += line + "\n";

            }
            else
            {
                if (line.Contains("#112"))
                {
                    foundCode = true;
                }
            }
        }
        Debug.Log(nextText);
        testingTxt.text = nextText;
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

    public void sendNextText()
    {
        //send text to text area.
    }
}
