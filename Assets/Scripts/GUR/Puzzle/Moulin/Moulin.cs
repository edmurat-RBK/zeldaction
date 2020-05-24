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

    [HideInInspector]
    public bool lockMoulinLeft;
    [HideInInspector]
    public bool lockMoulinRight;

    private Animator anim;
    #endregion

    void Start()
    {
        anim = GetComponent<Animator>();
        lockMoulinLeft = false;
        lockMoulinRight = false;

        moulinOnGauche = false;
        moulinOnDroit = false;
    }

    void Update()
    {
        if (lockMoulinLeft == true)
        {
            moulinOnGauche = lockMoulinLeft;
        }

        if (lockMoulinRight == true)
        {
            moulinOnDroit = lockMoulinRight;
        }

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
        FindObjectOfType<AudioManager>().Play("Moulin actif");
        anim.SetBool("tourne", true);
        moulinOnGauche = true;
        yield return new WaitForSeconds(timeOfActivation);
        anim.SetBool("tourne", false);
        moulinOnGauche = false;
        FindObjectOfType<AudioManager>().Stop("Moulin actif");
    } // Active la bool gauche

    IEnumerator ActivationTimeDroit()
    {
        FindObjectOfType<AudioManager>().Play("Moulin actif");
        anim.SetBool("tourne", true);
        moulinOnDroit = true;
        yield return new WaitForSeconds(timeOfActivation);
        anim.SetBool("tourne", false);
        moulinOnDroit = false;
        FindObjectOfType<AudioManager>().Stop("Moulin actif");
    } // Active la bool droite
}
