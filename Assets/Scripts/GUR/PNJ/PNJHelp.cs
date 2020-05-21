using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class PNJHelp : MonoBehaviour
{
    public enum villageoi
    {
        villageoiFlamme,
        villageoiCascade,
        villageoiSoif,
          
    }

    public villageoi wichVillageoi;

    private bool canActivate;

    private GameObject playerStock;

    void Start()
    {
        canActivate = false;
    }

    private void Update()
    {
        if (wichVillageoi == villageoi.villageoiFlamme)
        {
            if (canActivate == true && UpgradesManager.List["bonusHealth 1"] == false)
            {
                if (Input.GetButtonDown("X"))
                {
                    GetComponent<UpgradeObject>().Gotcha();
                    playerStock.GetComponent<PlayerHealth>().maximumHealth += 1;
                    playerStock.GetComponent<PlayerHealth>().health += 1;
                    playerStock.GetComponent<HealthBar>().HealthSysteme();
                }
            }
        }

        if (wichVillageoi == villageoi.villageoiCascade)
        {
            if (canActivate == true && UpgradesManager.List["bonusHealth 2"] == false)
            {
                if (Input.GetButtonDown("X"))
                {
                    GetComponent<UpgradeObject>().Gotcha();
                    playerStock.GetComponent<PlayerHealth>().maximumHealth += 1;
                    playerStock.GetComponent<PlayerHealth>().health += 1;
                    playerStock.GetComponent<HealthBar>().HealthSysteme();
                }
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
