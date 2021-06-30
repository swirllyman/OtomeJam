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
    [SerializeField] MainChat mainChat = new MainChat();

    public int day = 1;
    public void startTheFuckingGame()
    {
        StartCoroutine(waitForStart());
    }
    public IEnumerator waitForStart()
    {
        yield return new WaitForSeconds(2f);
        onChangeOfDay(day);
    }
    public void onChangeOfDay(int x)
    {
        if (day == 4)
        {
            dayChange.Invoke(x);
            end();
        }
        else
        {
            dayChange.Invoke(x);
            morning();
        }
    }


    public void morning()
    {
        // News bot first thing and start of dialogue
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
        mainChat.EnablePanel(0);
        timeTracker.Invoke(dayPhases.NIGHT);
    }

    public void end()
    {
        timeTracker.Invoke(dayPhases.END);
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
