using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextChoiceManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMPro.TextMeshProUGUI choice1;
    [SerializeField] TMPro.TextMeshProUGUI choice2;
    [SerializeField] TMPro.TextMeshProUGUI choice3;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void choiceHasBeenMade(int x)
    {
        if (x == 1)
        {
            Debug.Log("I am 1");
        }
        else if (x == 2)
        {
            Debug.Log("I am 2");
        }
        else
        {
            Debug.Log("I am 3");
        }
    }

    public void nextChoices(string choice1, string choice2, string choice3)
    {
        this.choice1.text = choice1;
        this.choice2.text = choice2;
        this.choice3.text = choice3;
    }
}
