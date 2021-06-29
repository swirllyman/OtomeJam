using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public static string playerName;
    public static Sprite playerIcon;
    public int saveDay;
    public LocalPlayerUI localUI;
    public Sprite[] allIcons;

    public PlayerAccount currentPlayerAccount;

    public static string GetPlayerPronouns()
    {
        return singleton.currentPlayerAccount.pronounID == 0 ? "They/Them" : singleton.currentPlayerAccount.pronounID == 1 ? "She/Her" : "He/Him";
    }

    private void Awake()
    {
        singleton = this;
    }

    #region Player Setup
    public void SetupLocalPlayer()
    {
        playerName = currentPlayerAccount.username;
        playerIcon = allIcons[currentPlayerAccount.iconID];
        localUI.myIcon.sprite = playerIcon;
        localUI.myNameText.text = playerName;
    }

    public void UpdateLocalPlayerPlaying(string newPlayingName)
    {
        localUI.currentlyPlayingText.text = newPlayingName;
    }
    #endregion

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

[System.Serializable]
public class LocalPlayerUI
{
    public Image myIcon;
    public TMP_Text myNameText;
    public TMP_Text currentlyPlayingText;
}