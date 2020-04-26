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
    #region
    [Header ("Gestion de l'eau de la clepsydre")]
    public int valeurParticule;
    public int maxStockage;

    public float remplissage;

    [HideInInspector]
    public bool actifClepsydre;

    [HideInInspector]
    public bool lockClepsydre;

    [HideInInspector]
    public bool clepsydreHit;
    #endregion

    void Start()
    {
        lockClepsydre = false;
        actifClepsydre = false;
        clepsydreHit = false;
    }

    void Update()
    {
        Vidage();
        KhamehoHit();
    }

    void KhamehoHit()
    {
        if (lockClepsydre == false)
        {
            if (clepsydreHit == true)
            {
                remplissage = maxStockage;
                clepsydreHit = false;
            }
        }
    }
    void Vidage()
    {
        if (lockClepsydre == false)
        {
            if (remplissage > 0)
            {
                actifClepsydre = true;
                remplissage -= Time.fixedDeltaTime;
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
