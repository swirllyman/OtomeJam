using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGame : MonoBehaviour
{
    Animation animation;
    [SerializeField] TMPro.TextMeshProUGUI finalText;

    bool isDone = false;
    // Start is called before the first frame update
    void Start()
    {
        animation = gameObject.GetComponent<Animation>();
    }
    void Update()
    {
        if (isDone == true)
        {
            if (Input.GetKeyDown("space"))
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void startEnd(string finalTxt)
    {
        finalText.text = finalTxt;
        // animation.Play();
    }
    public void startFadeOut()
    {
        isDone = true;
        gameObject.SetActive(true);
    }
}
