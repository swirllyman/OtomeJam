using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsSubscription : MonoBehaviour
{
    Hashtable newsMap = new Hashtable();

    public string getNewsArticle(string subChoice)
    {
        if (subChoice != null)
        {
            return (string)newsMap[subChoice];
        }
        return null;
    }

    public void addToNewsMap(string gameName, string update)
    {
        newsMap.Add(gameName, update);
    }

    public void removeFromNewsMap(string gameName)
    {
        newsMap.Remove(gameName);
    }
}
