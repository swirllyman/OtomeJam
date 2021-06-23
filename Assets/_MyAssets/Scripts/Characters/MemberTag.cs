using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberTag : MonoBehaviour
{
    [SerializeField] GameObject character;
    GameObject manager;



    void Start()
    {
        manager = GameObject.FindWithTag("Manager");
        DayManager dayManager = manager.GetComponent<DayManager>();
        dayManager.getDayChange().AddListener(addingGamerTag);
        dayManager.getTimeTracker().AddListener(timeTest);
    }

    void addingGamerTag(int x)
    {
        Transform ignObject = gameObject.transform.GetChild(0);
        Transform imageObject = gameObject.transform.GetChild(1);
        Characters characterInfo = character.GetComponent<Characters>();
        ignObject.GetComponent<TMPro.TextMeshProUGUI>().text = characterInfo.getIgn();
        // imageObject.GetComponent<Image>().sprite = characterInfo.getIcon().sprite;
    }

    void timeTest(dayPhases phase)
    {
    }
}
