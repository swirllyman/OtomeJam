using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelManager : MonoBehaviour
{
    [SerializeField] GameObject[] channelObj;
    [SerializeField] GameObject[] channelChoices;
    [SerializeField] GameObject[] channelNotifications;

    [SerializeField] GameObject textChannels;

    public void settingChannel(int currentChannel)
    {
        for (int i = 0; i < channelObj.Length; i++)
        {
            if (i == currentChannel)
            {
                channelObj[i].SetActive(true);
                channelChoices[i].SetActive(true);
                channelNotifications[i].SetActive(false);
            }
            else
            {
                channelObj[i].SetActive(false);
                channelChoices[i].SetActive(false);
            }
        }
    }
}
