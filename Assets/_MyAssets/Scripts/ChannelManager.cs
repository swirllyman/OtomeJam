using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelManager : MonoBehaviour
{
    [SerializeField] GameObject[] channelObj;
    [SerializeField] GameObject[] channelChoices;

    [SerializeField] GameObject textChannels;

    public void settingChannel(int currentChannel)
    {
        if (currentChannel == 0 || currentChannel == 1)
        {
            textChannels.SetActive(true);
        }
        else
        {
            textChannels.SetActive(false);
        }
        for (int i = 0; i < channelObj.Length; i++)
        {
            if (i == currentChannel)
            {
                channelObj[i].SetActive(true);
                channelChoices[i].SetActive(true);
            }
            else
            {
                channelObj[i].SetActive(false);
                channelChoices[i].SetActive(false);
            }
        }
    }
}
