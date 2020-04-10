﻿using System.Collections;
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

    [HideInInspector]
    public float remplissage;

    [HideInInspector]
    public bool actifClepsydre;
    #endregion

    void Start()
    {
        actifClepsydre = false;
    }

    void Update()
    {
        Vidage();
    }

    void Vidage()
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
    } // Fonction qui s'occupe de soustraire la valeur de remplissage par le temps qui passe

    private void OnParticleCollision(GameObject other)
    {
        if (remplissage < maxStockage)
        {
            Debug.Log("detection trigger");
            remplissage += valeurParticule;
        }
        
    } // Permet de détecter la collision de particule d'eau et d'incrémenter la valeur de remplissage
}