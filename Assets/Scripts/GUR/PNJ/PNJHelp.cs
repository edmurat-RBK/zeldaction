using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class PNJHelp : MonoBehaviour
{
    public bool lockHp;
    public bool canActivate;

    private GameObject playerStock;

    void Start()
    {
        canActivate = false;
        lockHp = false;
    }

    private void Update()
    {
        if (canActivate == true && lockHp == false)
        {
            if (Input.GetButtonDown("X"))
            {
                lockHp = true;
                playerStock.GetComponent<PlayerHealth>().maximumHealth += 1;
                playerStock.GetComponent<PlayerHealth>().health += 1;
                playerStock.GetComponent<HealthBar>().HealthSysteme();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canActivate = true;
            playerStock = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canActivate = false;
        }
    }

}
