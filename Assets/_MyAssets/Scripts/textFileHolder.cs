using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class newsDays
{
    public string path1 = "";
    public string path2 = "";
}
public class textFileHolder : MonoBehaviour
{
    public Hashtable mainServerText = new Hashtable();
    public Hashtable canDmText = new Hashtable();
    public Hashtable pokyDmText = new Hashtable();
    public Hashtable newsText = new Hashtable();
    public Hashtable newsBotText = new Hashtable();

    public newsDays newsDay1 = new newsDays();
    public newsDays newsDay2 = new newsDays();
    public newsDays newsDay3 = new newsDays();
    private void Start()
    {
        newsDay1.path1 = flows;
        newsDay1.path2 = bashBros;
        newsDay2.path1 = forTheQueen;
        newsDay2.path2 = cryToResponsibility;
        newsDay3.path1 = doublePlays;
        newsDay3.path2 = yggdrasil;
        mainServerText.Add(1, mainServerDay1);
        mainServerText.Add(2, mainServerDay2);
        mainServerText.Add(3, mainServerDay3);
        canDmText.Add(1, canOfCornDay1);
        canDmText.Add(2, canOfCornDay2);
        canDmText.Add(3, canOfCornDay3);
        canDmText.Add(4, canOfCornDay4);
        pokyDmText.Add(1, pokyDazeDay1);
        pokyDmText.Add(2, pokyDazeDay2);
        pokyDmText.Add(3, pokyDazeDay3);
        pokyDmText.Add(4, pokyDazeDay4);
        newsText.Add(1, newsDay1);
        newsText.Add(2, newsDay2);
        newsText.Add(3, newsDay3);
        newsBotText.Add(1, newsBotDay1);
        newsBotText.Add(2, newsBotDay2);
        newsBotText.Add(3, newsBotDay3);
    }
    [SerializeField] private string mainServerDay1;
    [SerializeField] private string mainServerDay2;
    [SerializeField] private string mainServerDay3;
    [SerializeField] private string canOfCornDay1;
    [SerializeField] private string canOfCornDay2;
    [SerializeField] private string canOfCornDay3;
    [SerializeField] private string canOfCornDay4;
    [SerializeField] private string pokyDazeDay1;
    [SerializeField] private string pokyDazeDay2;
    [SerializeField] private string pokyDazeDay3;
    [SerializeField] private string pokyDazeDay4;
    [SerializeField] private string newsBotDay1;
    [SerializeField] private string newsBotDay2;
    [SerializeField] private string newsBotDay3;

    [SerializeField] private string flows;
    [SerializeField] private string bashBros;
    [SerializeField] private string forTheQueen;
    [SerializeField] private string cryToResponsibility;
    [SerializeField] private string yggdrasil;
    [SerializeField] private string doublePlays;
}
