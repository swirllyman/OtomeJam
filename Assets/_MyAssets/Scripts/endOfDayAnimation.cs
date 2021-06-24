using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endOfDayAnimation : MonoBehaviour
{
    DayManager timeTracker;
    Animation animation;
    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        timeTracker = manager.GetComponent<DayManager>();
        timeTracker.getTimeTracker().AddListener(timeToFadeOut);
        animation = gameObject.GetComponent<Animation>();
    }

    void timeToFadeOut(dayPhases night)
    {
        switch (night)
        {
            case dayPhases.NIGHT:
                animation.Play();
                break;
        }
    }

    void fadeDone()
    {
        timeTracker.nextDay();
    }

}
