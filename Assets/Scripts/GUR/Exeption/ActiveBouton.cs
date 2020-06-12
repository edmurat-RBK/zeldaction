using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBouton : MonoBehaviour
{
    public GameObject ui;

    private void Start()
    {
        ui.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ui.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            ui.SetActive(false);
        }
    }
}
