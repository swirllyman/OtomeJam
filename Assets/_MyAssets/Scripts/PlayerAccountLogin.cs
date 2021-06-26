using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerAccountLogin : MonoBehaviour
{
    [Header("UI")]
    public GameObject[] UI_Panels;
    public PlayerAccountUI[] accountUI;

    public Button newAccountButton;
    public TMP_Text accountDeleteText;
    [Space]
    public PlayerAccount[] playerAccounts;
    public NewPlayerAccount currentSetupAccount;
    


    int currentAccountToDelete = 0;
    readonly string[] accountPrefs = { "ACCOUNT_ID_1", "ACCOUNT_ID_2", "ACCOUNT_ID_3" };
    readonly string[] accountInfo = { "ACCOUNT_INFO_1", "ACCOUNT_INFO_2", "ACCOUNT_INFO_3" };

    void Start()
    {
        CheckPrefs();
    }

    void CheckPrefs()
    {
        bool accountCreated = false;
        for (int i = 0; i < accountPrefs.Length; i++)
        {
            if (!PlayerPrefs.HasKey(accountPrefs[i])) PlayerPrefs.SetInt(accountPrefs[i], 0);
            if (!PlayerPrefs.HasKey(accountInfo[i])) PlayerPrefs.SetString(accountInfo[i], "");
            HideAccountUI(i);

            if (PlayerPrefs.GetInt(accountPrefs[i]) == 1)
            {
                string accountString = PlayerPrefs.GetString(accountInfo[i]);
                print(accountString);
                PlayerAccount account = JsonUtility.FromJson<PlayerAccount>(accountString);
                playerAccounts[i] = NewAccount(account);
                SetupUIForAccount(i, playerAccounts[i]);
                accountCreated = true;
            }
        }

        newAccountButton.interactable = HasFreeSpace();

        if (!accountCreated)
        {
            SwapPanels(1);
        }
        else
        {
            SwapPanels(0);
        }
    }

    #region UI Setup
    void SetupUIForAccount(int accountID, PlayerAccount account)
    {
        accountUI[accountID].playerIcon.enabled = true;
        accountUI[accountID].playerIcon.sprite = GameManager.singleton.allIcons[account.iconID];
        accountUI[accountID].playerName.text = account.username;
        accountUI[accountID].pronouns.text = account.pronounID == 0 ? "They/Them" : account.pronounID == 1 ? "She/Her" : "He/Him";
        accountUI[accountID].playTime.text = Mathf.Floor(account.playTime / 60).ToString("00") + ":" + Mathf.FloorToInt(account.playTime % 60).ToString("00");
        accountUI[accountID].deleteAccountButton.gameObject.SetActive(true);
        accountUI[accountID].loginButton.interactable = true;
    }

    void HideAccountUI(int accountID)
    {
        accountUI[accountID].playerIcon.enabled = false;
        accountUI[accountID].playerName.text = "";
        accountUI[accountID].pronouns.text = "";
        accountUI[accountID].playTime.text = "";
        accountUI[accountID].deleteAccountButton.gameObject.SetActive(false);
        accountUI[accountID].loginButton.interactable = false;
    }
    #endregion

    #region Account Deletion
    public void DeleteAccount(int accountID)
    {
        currentAccountToDelete = accountID;
        accountDeleteText.text = "Delete <b>Account " + (accountID+1) + "</b>?\n\n<b>Warning!</b>\nThis Cannot Be Undone.";
        SwapPanels(2);
    }

    public void ConfirmAccountDelete()
    {
        HideAccountUI(currentAccountToDelete);
        PlayerPrefs.SetInt(accountPrefs[currentAccountToDelete], 0);
        PlayerPrefs.SetString(accountInfo[currentAccountToDelete], "");
        playerAccounts[currentAccountToDelete].isCreated = false;

        SwapPanels(0);
        newAccountButton.interactable = HasFreeSpace();
    }
    #endregion

    #region Button Setters
    public void LoginToGame(int accountID)
    {
        //TODO: Setup Any Login Info For Returning Players
        GameManager.singleton.currentPlayerAccount = playerAccounts[accountID];
        GameManager.singleton.SetupLocalPlayer();
        GameManager.singleton.UpdateLocalPlayerPlaying("");

        SwapPanels(3);
    }

    public void CreateNewAccount()
    {
        PlayerAccount newPlayerAccount = new PlayerAccount
        {
            isCreated = true,
            playTime = 0.0f,
            username = currentSetupAccount.inputField.text,
            pronounID = currentSetupAccount.currentPronounID,
            iconID = currentSetupAccount.currentIconID
        };

        for (int i = 0; i < playerAccounts.Length; i++)
        {
            if (!playerAccounts[i].isCreated)
            {
                playerAccounts[i] = NewAccount(newPlayerAccount);
                SetupUIForAccount(i, newPlayerAccount);
                PlayerPrefs.SetInt(accountPrefs[i], 1);

                string accountSetString = JsonUtility.ToJson(playerAccounts[i]);
                print(accountSetString);
                PlayerPrefs.SetString(accountInfo[i], accountSetString);
                break;
            }
        }

        newAccountButton.interactable = HasFreeSpace();
        currentSetupAccount.inputField.text = "";
        SwapPanels(0);
    }

    public void SetCurrentProunoun(int pronounID)
    {
        currentSetupAccount.currentPronounID = pronounID;
    }

    public void SetCurrentIcon(int iconID)
    {
        currentSetupAccount.currentIconID = iconID;
    }

    public void SwapPanels(int panelID)
    {
        for (int i = 0; i < UI_Panels.Length; i++)
        {
            UI_Panels[i].SetActive(i == panelID);
        }
    }
    #endregion

    #region Helper Functions
    PlayerAccount NewAccount(PlayerAccount newAccount)
    {
        PlayerAccount newPlayerAccount = new PlayerAccount
        {
            isCreated = newAccount.isCreated,
            playTime = newAccount.playTime,
            username = newAccount.username,
            pronounID = newAccount.pronounID,
            iconID = newAccount.iconID
        };
        return newPlayerAccount;
    }

    bool HasFreeSpace()
    {
        for (int i = 0; i < playerAccounts.Length; i++)
        {
            if (!playerAccounts[i].isCreated)
                return true;
        }

        return false;
    }
    #endregion
}

[System.Serializable]
public struct PlayerAccountUI
{
    public Image playerIcon;
    public Button loginButton;
    public Button deleteAccountButton;
    public TMP_Text playerName;
    public TMP_Text pronouns;
    public TMP_Text playTime;
}

[System.Serializable]
public struct PlayerAccount
{
    public bool isCreated;
    public string username;
    public int pronounID, iconID;
    public float playTime;
} 

[System.Serializable]
public class NewPlayerAccount
{
    public TMP_InputField inputField;
    public int currentPronounID, currentIconID;
}