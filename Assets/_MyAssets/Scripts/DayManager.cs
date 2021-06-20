using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DayTracker : UnityEvent<int>
{

}
public class TimeTracker : UnityEvent<dayPhases>
{

}
public class DayManager : MonoBehaviour
{
    DayTracker dayChange = new DayTracker();
    TimeTracker timeTracker = new TimeTracker();
    void Start()
    {
        StartCoroutine(waitForStart());
    }
    IEnumerator waitForStart()
    {
        yield return new WaitForSeconds(2f);
        onChangeOfDay(1);
    }
    void onChangeOfDay(int x)
    {
        dayChange.Invoke(x);
        morning();
    }


    void morning()
    {
        // News bot first thing and start of dialogue
        Debug.Log("morning");
        timeTracker.Invoke(dayPhases.MORNING);
    }

    void afternoon()
    {
        // finishing main dialogue- aka news bot updates and start of dm's
        timeTracker.Invoke(dayPhases.AFTERNOON);
    }

    void night()
    {
        // Finishing dm's and ending day wrap up.
        timeTracker.Invoke(dayPhases.NIGHT);
    }

    public DayTracker getDayChange()
    {
        return dayChange;
    }

    public TimeTracker getTimeTracker()
    {
        return timeTracker;
    }
}
