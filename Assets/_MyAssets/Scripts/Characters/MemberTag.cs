using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberTag : MonoBehaviour
{
    [SerializeField] GameObject character;



    void Start()
    {
        Transform ignObject = gameObject.transform.GetChild(0);
        Transform imageObject = gameObject.transform.GetChild(1);
        Characters characterInfo = character.GetComponent<Characters>();
        ignObject.GetComponent<TMPro.TextMeshProUGUI>().text = characterInfo.getIgn();
        imageObject.GetComponent<Image>().sprite = characterInfo.getIcon();
    }
}
