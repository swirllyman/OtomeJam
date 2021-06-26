using UnityEngine;
using TMPro;

public class MainChat : MonoBehaviour
{
    public GameObject[] onlinePanels;

    public void EnablePanel(int panelID)
    {
        for (int i = 0; i < onlinePanels.Length; i++)
        {
            onlinePanels[i].SetActive(i == panelID);
        }
    }
}
