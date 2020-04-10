using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Créateur : Guillaume Rogé
/// Ce script permet de :
/// - Detecter quelle hitBox du moulin est touché par le khaméo (gauche ou droite)
/// - Envoyé l'information au script moulin mère
/// </summary>

public class MoulinHitDetection : MonoBehaviour
{
    #region Variable
    [Header ("Quelle est l'hitBox")]
    public bool hitBoxGauche;
    public bool hitBoxDroite;

    [HideInInspector]
    public bool getHit;

    private void Start()
    {
        getHit = false;
    }
    #endregion

    void Update()
    {
        HitGauche();
        HitDroit();
    }

    void HitGauche()
    {
        if (getHit == true && hitBoxGauche == true)
        {
            getHit = false;
            GetComponentInParent<Moulin>().hitGauche = true;
        }
    } // Detecte si la hitBox gauche est touché

    void HitDroit()
    {
        if (getHit == true && hitBoxDroite == true)
        {
            getHit = false;
            GetComponentInParent<Moulin>().hitDroit = true;
        }
    } // Détecte si la hitBox droite est touché
}
