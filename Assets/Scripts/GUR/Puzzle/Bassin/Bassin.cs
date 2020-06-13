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
    public float speedVidage;

    [Header("Les différents etats du bassin")]
    public Sprite[] spriteBassin;

    public float remplissage;

    public bool isDestructible;
    public bool canVapeur;

    [HideInInspector]
    public bool actifBassin;

    [HideInInspector]
    public bool lockBassin;

    [HideInInspector]
    public bool fullDestroy;

    private SpriteRenderer bassinRenderer;
    public float bassinState;
    private bool canEmpty;
    public GameObject particule;


    private bool alreadyInList;
    public List<GameObject> enemyInRange = new List<GameObject>();
    #endregion

    void Start()
    {
        bassinRenderer = GetComponent<SpriteRenderer>();
        bassinState = maxStockage / spriteBassin.Length;

        if (canVapeur == true)
        {
            particule.SetActive(false);
        }

        fullDestroy = false;
        alreadyInList = false;
        lockBassin = false;
        canEmpty = false;
        actifBassin = false;
    }

    void Update()
    {
        enemyInRange.RemoveAll(list_item => list_item == null);

        VidageBassin();
        GestionActivation();
        GestionVisuel();

        if (fullDestroy == true && remplissage < maxStockage)
        {
            remplissage += Time.fixedDeltaTime * 2;
        }

        if (canVapeur == true)
        {
            if (enemyInRange.Count == 0)
            {
                particule.SetActive(false);
            }
        }
    }

    void GestionVisuel()
    {
        if (isDestructible == false)
        {
            if (remplissage <= 0)
            {
                bassinRenderer.sprite = spriteBassin[0];
            }

            else if (remplissage > 0 && remplissage < bassinState)
            {
                bassinRenderer.sprite = spriteBassin[1];
            }

            else if (remplissage > bassinState && remplissage < bassinState * 2)
            {
                bassinRenderer.sprite = spriteBassin[2];
            }

            else if (remplissage > bassinState * 2 && remplissage < bassinState * 3)
            {
                bassinRenderer.sprite = spriteBassin[3];
            }

            else if (remplissage > bassinState * 3 && remplissage < bassinState * 4)
            {
                bassinRenderer.sprite = spriteBassin[4];
            }

            else if (remplissage > bassinState * 4 && remplissage < bassinState * 5)
            {
                bassinRenderer.sprite = spriteBassin[5];
            }

            else if (remplissage > bassinState * 5 && remplissage < bassinState * 6)
            {
                bassinRenderer.sprite = spriteBassin[6];
            }

            else if (remplissage > bassinState * 7 && remplissage < bassinState * 8)
            {
                bassinRenderer.sprite = spriteBassin[7];
            }

            else if (remplissage > bassinState * 9 && remplissage < bassinState * 10)
            {
                bassinRenderer.sprite = spriteBassin[8];
            }

            else if (remplissage > bassinState * 10 && remplissage < bassinState * 11)
            {
                bassinRenderer.sprite = spriteBassin[9];
            }

            else if (remplissage > bassinState * 11 && remplissage < bassinState * 12)
            {
                bassinRenderer.sprite = spriteBassin[10];
            }

            else if (remplissage > bassinState * 12)
            {
                bassinRenderer.sprite = spriteBassin[11];
            }
        }

        if (isDestructible == true)
        {
            if (remplissage <= 0)
            {
                bassinRenderer.sprite = spriteBassin[0];
            }

            else if (remplissage > 0 && remplissage < bassinState)
            {
                bassinRenderer.sprite = spriteBassin[1];
            }

            else if (remplissage > bassinState && remplissage < bassinState * 2)
            {
                bassinRenderer.sprite = spriteBassin[2];
            }

            else if (remplissage > bassinState * 2 && remplissage < bassinState * 3)
            {
                bassinRenderer.sprite = spriteBassin[3];
            }

            else if (remplissage > bassinState * 3 && remplissage < bassinState * 4)
            {
                bassinRenderer.sprite = spriteBassin[4];
            }

            else if (remplissage > bassinState * 4 && remplissage < bassinState * 5)
            {
                bassinRenderer.sprite = spriteBassin[5];
            }

            else if (remplissage > bassinState * 5 && remplissage < bassinState * 6)
            {
                bassinRenderer.sprite = spriteBassin[6];
            }

            else if (remplissage > bassinState * 7 && remplissage < bassinState * 8)
            {
                bassinRenderer.sprite = spriteBassin[7];
            }

            else if (remplissage > bassinState * 9 && remplissage < bassinState * 10)
            {
                bassinRenderer.sprite = spriteBassin[8];
            }

            else if (remplissage > bassinState * 10 && remplissage < bassinState * 11)
            {
                bassinRenderer.sprite = spriteBassin[9];
            }

            else if (remplissage > bassinState * 11 && remplissage < bassinState * 12)
            {
                bassinRenderer.sprite = spriteBassin[10];
            }

            else if (remplissage > bassinState * 12 && remplissage < bassinState * 13)
            {
                bassinRenderer.sprite = spriteBassin[11];
            }

            else if (remplissage > bassinState * 13 && remplissage < bassinState * 14)
            {
                bassinRenderer.sprite = spriteBassin[12];
            }

            else if (remplissage > bassinState * 14 && remplissage < bassinState * 15)
            {
                bassinRenderer.sprite = spriteBassin[13];
            }
        }

    }

    void VidageBassin()
    {
        if (lockBassin == false)
        {
            if (remplissage > 0 && enemyInRange.Count >= 1 && lockBassin == false)
            {
                if (canVapeur == true)
                {
                    particule.SetActive(true);
                }
                remplissage -= Time.fixedDeltaTime * speedVidage;
            }
        }
    } // Diminue la valeur de remplissage en fonction du temps si un ennemi de feu et proche

    void GestionActivation()
    {
        if (lockBassin == false)
        {
            if (remplissage > 0)
            {
                actifBassin = true;
            }

            else if (remplissage <= 0)
            {
                actifBassin = false;
            }
        }
    } // Change l'etat d'une bool "activation" si il y a de l'eau ou non


    private void OnParticleCollision(GameObject other)
    {
        if (remplissage < maxStockage && lockBassin == false)
        {
            remplissage += valeurParticule;
        }
    } // Detecte si des particules de l'arosoire touche le bassin pour agmenter la valeur de remplissage

    private void  OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ennemi")
        {
            foreach (GameObject enemy in enemyInRange)
            {
                if (collision.gameObject == enemy)
                {
                    alreadyInList = true;
                }
            }
            if (alreadyInList == false)
            {
                enemyInRange.Add(collision.gameObject);
            }
            alreadyInList = false;
            //canEmpty = true;
        }
    } // Permet de detecter si un ennemi est proche du bassin ou non pour le vider

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ennemi")
        {
            enemyInRange.Remove(collision.gameObject);
            //canEmpty = false;
        }
    } // Permet de detecter si un ennemi s'éloigne du bassin
}
