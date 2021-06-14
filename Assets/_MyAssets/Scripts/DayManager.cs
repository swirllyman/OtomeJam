using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DayTracker : UnityEvent<int>
{

}
public class DayManager : MonoBehaviour
{
    DayTracker dayChange = new DayTracker();
    void Start()
    {
        StartCoroutine(AfterStart(2f));
    }

    IEnumerator AfterStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        onChangeOfDay(1);
    }
    void onChangeOfDay(int x)
    {
        dayChange.Invoke(x);
    }


    public DayTracker getDayChange()
    {
        return dayChange;
    }
}
