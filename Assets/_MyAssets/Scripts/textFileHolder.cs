using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textFileHolder : MonoBehaviour
{
    public Hashtable mainServerText = new Hashtable();
    public Hashtable canDmText = new Hashtable();
    public Hashtable pokyDmText = new Hashtable();
    public Hashtable newsText = new Hashtable();
    private void Start()
    {
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
        newsText.Add("flows", flows);
        newsText.Add("bash", bashBros);
        newsText.Add("queen", forTheQueen);
        newsText.Add("cry", cryToResponsibility);
        newsText.Add("ygg", yggdrasil);
        newsText.Add("double", doublePlays);
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
    [SerializeField] private string flows;
    [SerializeField] private string bashBros;
    [SerializeField] private string forTheQueen;
    [SerializeField] private string cryToResponsibility;
    [SerializeField] private string yggdrasil;
    [SerializeField] private string doublePlays;
}
