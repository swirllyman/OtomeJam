using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public static string playerName;
    public static Sprite playerIcon;

    public Sprite playerIconSprite;
    private void Awake()
    {
        singleton = this;

        playerName = "You";
        playerIcon = playerIconSprite;
    }
}
