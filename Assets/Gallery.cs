using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gallery : MonoBehaviour
{
    public GameObject galleryPanel;

    public GameObject pokyPicsParent;
    public GameObject canPicsParent;
    public Sprite[] pokyPics;
    public Sprite[] canPics;
    public Image mainImage;
    public TMP_Text playerNameText;

    public void OpenGallery()
    {
        galleryPanel.SetActive(true);
        SelectPoky();
    }

    public void CloseGallery()
    {
        galleryPanel.SetActive(false);
    }

    public void SelectPoky()
    {
        playerNameText.text = "Poky";
        mainImage.sprite = pokyPics[0];
        pokyPicsParent.SetActive(true);
        canPicsParent.SetActive(false);
    }

    public void SelectCan()
    {
        playerNameText.text = "Can";
        mainImage.sprite = canPics[0];
        pokyPicsParent.SetActive(false);
        canPicsParent.SetActive(true);
    }

    public void ShowPicPoky(int picID)
    {
        mainImage.sprite = pokyPics[picID];
    }

    public void ShowPicCan(int picID)
    {
        mainImage.sprite = canPics[picID];
    }
}
