using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBois : MonoBehaviour
{
    public GameObject hitboxHaut;
    public GameObject hitboxBas;
    public GameObject hitboxDroite;
    public GameObject hitboxGauche;

    public bool canRedirect = true;
    public GameObject waterSlide;
    public GameObject teste;

    public string wichDirection;

    private void Start()
    {
        hitboxHaut.SetActive(false);
        hitboxBas.SetActive(false);
        hitboxDroite.SetActive(false);
        hitboxGauche.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 29)
        {
            switch (wichDirection)
            {
                case("Bas"):
                    waterSlide.SetActive(false);
                    teste.SetActive(false);
                    hitboxHaut.SetActive(true);
                    hitboxBas.SetActive(true);

                    hitboxDroite.SetActive(false);
                    hitboxGauche.SetActive(false);
                break;

                case ("Haut"):
                    waterSlide.SetActive(false);
                    teste.SetActive(false);
                    hitboxHaut.SetActive(true);
                    hitboxBas.SetActive(true);

                    hitboxDroite.SetActive(false);
                    hitboxGauche.SetActive(false);
                break;

                case ("Droite"):
                    waterSlide.SetActive(false);
                    teste.SetActive(false);
                    hitboxDroite.SetActive(true);
                    hitboxGauche.SetActive(true);

                    hitboxHaut.SetActive(false);
                    hitboxBas.SetActive(false);
                break;

                case ("Gauche"):
                    waterSlide.SetActive(false);
                    teste.SetActive(false);
                    hitboxDroite.SetActive(true);
                    hitboxGauche.SetActive(true);

                    hitboxHaut.SetActive(false);
                    hitboxBas.SetActive(false);
                break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 29)
        {
            waterSlide.SetActive(true);
            teste.SetActive(true);
            hitboxHaut.SetActive(false);
            hitboxBas.SetActive(false);
            hitboxDroite.SetActive(false);
            hitboxGauche.SetActive(false);
        }
    }
}
