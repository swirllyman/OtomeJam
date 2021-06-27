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

    int day = 1;
    // void Start()
    // {
    //     StartCoroutine(waitForStart());
    // }
    public void startTheFuckingGame()
    {
        StartCoroutine(waitForStart());
    }
    public IEnumerator waitForStart()
    {
        Debug.Log("HELLO FUCKING");
        yield return new WaitForSeconds(2f);
        onChangeOfDay(day);
    }
    public void onChangeOfDay(int x)
    {

        dayChange.Invoke(x);
        morning();
    }


    public void morning()
    {
        // News bot first thing and start of dialogue
        Debug.Log("Morning");
        timeTracker.Invoke(dayPhases.MORNING);
    }

    public void afternoon()
    {
        // finishing main dialogue- aka news bot updates and start of dm's
        timeTracker.Invoke(dayPhases.AFTERNOON);
    }

    public void night()
    {
        // Finishing dm's and ending day wrap up.
        timeTracker.Invoke(dayPhases.NIGHT);
    }

    public DayTracker getDayChange()
    {
        return dayChange;
    }
    public void nextDay()
    {
        day++;
        onChangeOfDay(day);
    }
    public TimeTracker getTimeTracker()
    {
        return timeTracker;
    }
}
