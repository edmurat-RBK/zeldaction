using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;

public class PNJDialog : MonoBehaviour
{
    private Collider2D triggerZone;
    [SerializeField]
    private BeginDialogue dialogueStart;
    [SerializeField]
    private GameObject button;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetButtonDown("X"))
            {
                dialogueStart.enabled = (true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            button.SetActive(false);
        }
    }
}
