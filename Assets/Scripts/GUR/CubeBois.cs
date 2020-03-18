using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using movementPlayer;

public class CubeBois : MonoBehaviour
{
    public GameObject[] courantEau;

    public GameObject hitboxVerticale;
    public GameObject hitboxHorizontale;

    public bool canRedirect = true;

    [HideInInspector]
    public string wichDirection;

    private void Start()
    {
        courantEau = GameObject.FindGameObjectsWithTag("Courant");

        hitboxVerticale.SetActive(false);
        hitboxHorizontale.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 29)
        {
            switch (wichDirection)
            {
                case ("Bas"):
                    hitboxVerticale.SetActive(true);
                    hitboxHorizontale.SetActive(false);
                   
                break;

                case ("Haut"):
                    hitboxVerticale.SetActive(true);
                    hitboxHorizontale.SetActive(false);
                break;

                case ("Droite"):
                    hitboxVerticale.SetActive(false);
                    hitboxHorizontale.SetActive(true);
                break;

                case ("Gauche"):
                    hitboxVerticale.SetActive(false);
                    hitboxHorizontale.SetActive(true);
                break;
            }

            for (int i = 0; i < courantEau.Length; i++)
            {
                courantEau[i].SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 29)
        {
            for (int i = 0; i < courantEau.Length; i++)
            {
                courantEau[i].SetActive(true);
            }

            hitboxVerticale.SetActive(false);
            hitboxHorizontale.SetActive(false);
        }
    }
}
