using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Créateur : Guillaume Rogé 
/// Ce script permet de : 
/// - d'active une bool en fonction de quel côté le moulin est touché 
/// </summary>

public class Moulin : MonoBehaviour
{
    #region Variable
    [Header ("Temps que le moulin reste actif")]
    public float timeOfActivation;

    [HideInInspector]
    public bool hitGauche;
    [HideInInspector]
    public bool hitDroit;

    [HideInInspector]
    public bool moulinOnGauche;
    [HideInInspector]
    public bool moulinOnDroit;
    #endregion

    void Start()
    {
        moulinOnGauche = false;
        moulinOnDroit = false;
    }

    void Update()
    {
        ActivationGauche(); 
        ActivationDroit(); 
    }


    void ActivationGauche()
    {
        if (hitGauche == true)
        {
            hitGauche = false;
            StartCoroutine(ActivationTimeGauche());
        }
    }

    void ActivationDroit()
    {
        if (hitDroit == true)
        {
            hitDroit = false;
            StartCoroutine(ActivationTimeDroit());
        }

    }


    IEnumerator ActivationTimeGauche()
    {
        moulinOnGauche = true;
        yield return new WaitForSeconds(timeOfActivation);
        moulinOnGauche = false;
    } // Active la bool gauche

    IEnumerator ActivationTimeDroit()
    {
        moulinOnDroit = true;
        yield return new WaitForSeconds(timeOfActivation);
        moulinOnDroit = false;
    } // Active la bool droite
}
