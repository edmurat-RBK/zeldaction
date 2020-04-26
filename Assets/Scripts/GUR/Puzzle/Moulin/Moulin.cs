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

    //[HideInInspector]
    public bool moulinOnGauche;
    //[HideInInspector]
    public bool moulinOnDroit;

    //[HideInInspector]
    public bool lockMoulinLeft;
    //[HideInInspector]
    public bool lockMoulinRight;
    #endregion

    void Start()
    {
        lockMoulinLeft = false;
        lockMoulinRight = false;

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
        if (hitGauche == true && lockMoulinLeft == false)
        {
            hitGauche = false;
            StartCoroutine(ActivationTimeGauche());
        }
    }

    void ActivationDroit()
    {
        if (hitDroit == true && lockMoulinRight == false)
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
