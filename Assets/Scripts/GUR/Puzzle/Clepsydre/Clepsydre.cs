using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Créateur : Guillaume Rogé
/// Ce script permet de : 
/// - Detecter la collision des particule d'eau pour incrémenté une valeur de remplissage
/// - Soustraire de temps qui passe à la valeur de remplissage
/// - Quand la valeur de remplissage est supérieur à 0 une bool passe actif
/// </summary>


public class Clepsydre : MonoBehaviour
{
    #region Variable
    [Header ("Gestion de l'eau de la clepsydre")]
    public float valeurParticule;
    public float maxStockage;

    [Header ("Pour équilibrer la clepsydre")]
    public float speedVidage;
    public float speedRemplissageKhameo;

    public float remplissage;

    [Header ("Sprite pour l'anim")]
    public Sprite[] wichSprite;

    private float stateTime;
    private SpriteRenderer clepsydreRenderer;

    [HideInInspector]
    public bool actifClepsydre;

    [HideInInspector]
    public bool lockClepsydre;

    [HideInInspector]
    public bool clepsydreHit;

    #endregion

    void Start()
    {
        stateTime = maxStockage / 8;
        clepsydreRenderer = GetComponent<SpriteRenderer>();
        lockClepsydre = false;
        actifClepsydre = false;
        clepsydreHit = false;
    }

    void Update()
    {
        Vidage();
        KhamehoHit();
        GestionVisuel();
        
    }

    void GestionVisuel()
    {
        if (remplissage <= 0)
        {
            clepsydreRenderer.sprite = wichSprite[0];
        }

        else if (remplissage > 0 && remplissage < stateTime)
        {
            clepsydreRenderer.sprite = wichSprite[1];
        }

        else if (remplissage > stateTime && remplissage < (stateTime * 2))
        {
            clepsydreRenderer.sprite = wichSprite[2];
        }

        else if (remplissage > (stateTime * 2) && remplissage < (stateTime * 3))
        {
            clepsydreRenderer.sprite = wichSprite[3];
        }

        else if (remplissage > (stateTime * 3) && remplissage < (stateTime * 4))
        {
            clepsydreRenderer.sprite = wichSprite[4];
        }

        else if (remplissage > (stateTime * 4) && remplissage < (stateTime * 5))
        {
            clepsydreRenderer.sprite = wichSprite[5];
        }
        
        else if (remplissage > (stateTime * 5) && remplissage < (stateTime * 6))
        {
            clepsydreRenderer.sprite = wichSprite[6];
        }

        else if (remplissage > (stateTime * 6))
        {
            clepsydreRenderer.sprite = wichSprite[7];
        }

    } // Fonction qui gére l'affiche des sprites en fonction du remplissage

    void KhamehoHit()
    {
        if (lockClepsydre == false)
        {
            if (clepsydreHit == true && remplissage < maxStockage)
            {
                remplissage += Time.fixedDeltaTime * speedRemplissageKhameo;
                clepsydreHit = false;
            }
        }
    } // fonction qui gére le remplissage de la clepsydre quand la khaméo le touche

    void Vidage()
    {
        if (lockClepsydre == false)
        {
            if (remplissage > 0)
            {
                actifClepsydre = true;
                remplissage -= Time.fixedDeltaTime * speedVidage;
            }

            else if (remplissage <= 0)
            {
                actifClepsydre = false;
            }    
        }
    } // Fonction qui s'occupe de soustraire la valeur de remplissage par le temps qui passe

    private void OnParticleCollision(GameObject other)
    {
        if (lockClepsydre == false)
        {
            if (remplissage < maxStockage)
            {
                remplissage += valeurParticule;
            }
        }
        
    } // Permet de détecter la collision de particule d'eau et d'incrémenter la valeur de remplissage
}
