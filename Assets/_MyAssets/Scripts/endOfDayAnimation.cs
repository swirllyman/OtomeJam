using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endOfDayAnimation : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI dayText;
    DayManager timeTracker;
    Animation animation;
    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        timeTracker = manager.GetComponent<DayManager>();
        timeTracker.getTimeTracker().AddListener(timeToFadeOut);
        timeTracker.getDayChange().AddListener(dayChange);
        animation = gameObject.GetComponent<Animation>();
        gameObject.SetActive(false);
    }

    void timeToFadeOut(dayPhases night)
    {
        switch (night)
        {
            case dayPhases.NIGHT:
                Debug.Log("Night loop");
                gameObject.SetActive(true);
                animation.Play();
                break;
        }
    }
    void dayChange(int x)
    {
        dayText.text = "Day " + (x + 1);
    }
    void fadeDone()
    {
        gameObject.SetActive(false);
        timeTracker.nextDay();
    }

}
