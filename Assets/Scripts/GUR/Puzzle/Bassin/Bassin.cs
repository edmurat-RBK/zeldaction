using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Créateur : Guillaume Rogé
/// Ce script permet de :
/// - Detecter la collision des particule d'eau pour incrémenté une valeur de remplissage
/// - Soustraire de temps qui passe à la valeur de remplissage quand un ennemi de feu et proche
/// - Quand la valeur de remplissage est supérieur à 0 une bool passe actif
/// - En fonction de la quantité d'eau change le visuel
/// </summary>

public class Bassin : MonoBehaviour
{
    #region Variable

    [Header ("Gestion de l'eau du bassin")]
    public float valeurParticule;
    public int maxStockage;

    [Header ("Les différents etats du bassin")]
    public GameObject[] bassin;

    private bool canEmpty;

    [HideInInspector]
    public float remplissage;

    [HideInInspector]
    public bool actifBassin;

    #endregion

    void Start()
    {
        bassin[0].SetActive(true);
        bassin[1].SetActive(false);
        bassin[2].SetActive(false);
        bassin[3].SetActive(false);

        canEmpty = false;
        actifBassin = false;
    }

    void Update()
    {
        VidageBassin();
        GestionVisuel();
        GestionActivation();
    }


    void VidageBassin()
    {
        if (remplissage > 0 && canEmpty == true)
        {
            remplissage -= Time.fixedDeltaTime;
        }
    } // Diminue la valeur de remplissage en fonction du temps si un ennemi de feu et proche

    void GestionVisuel()
    {
        if (remplissage > 0 && remplissage <= 3)
        {
            bassin[0].SetActive(true);
            bassin[1].SetActive(false);
            bassin[2].SetActive(false);
            bassin[3].SetActive(false);
        }

        else if (remplissage > 3 && remplissage <= 6)
        {
            bassin[0].SetActive(false);
            bassin[1].SetActive(true);
            bassin[2].SetActive(false);
            bassin[3].SetActive(false);
        }

        else if (remplissage > 6 && remplissage <= 9)
        {
            bassin[0].SetActive(false);
            bassin[1].SetActive(false);
            bassin[2].SetActive(true);
            bassin[3].SetActive(false);
        }

        else if (remplissage > 9 && remplissage <= 12)
        {
            bassin[0].SetActive(false);
            bassin[1].SetActive(false);
            bassin[2].SetActive(false);
            bassin[3].SetActive(true);
        }
    } // Change le visuel du bassin en fonction de la valeur de remplissage

    void GestionActivation()
    {
        if (remplissage > 0)
        {
            actifBassin = true;
        }

        else if (remplissage <= 0)
        {
            actifBassin = false;
        }
    } // Change l'etat d'une bool "activation" si il y a de l'eau ou non


    private void OnParticleCollision(GameObject other)
    {
        if (remplissage < maxStockage)
        {
            remplissage += valeurParticule;
        }
    } // Detecte si des particules de l'arosoire touche le bassin pour agmenter la valeur de remplissage

    private void  OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ennemi")
        {
            canEmpty = true;
        }
    } // Permet de detecter si un ennemi est proche du bassin ou non pour le vider

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ennemi")
        {
            canEmpty = false;
        }
    } // Permet de detecter si un ennemi s'éloigne du bassin
}
