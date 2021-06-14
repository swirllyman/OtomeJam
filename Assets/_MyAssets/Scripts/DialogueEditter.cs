using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEditter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject chatMessage;
    void Start()
    {
        GameObject newMessage = Instantiate(chatMessage, new Vector3(5, -30, 0), Quaternion.identity, gameObject.transform);
        newMessage.transform.parent = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
