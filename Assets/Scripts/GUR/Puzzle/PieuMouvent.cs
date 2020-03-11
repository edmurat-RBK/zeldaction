using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Créateur : Guillaume Rogé
/// Ce script permet de :
/// - Detecter le changement d'etat d'un bool d'une plaque de pression lié au pieu
/// - Bouge le pieu d'arrière à avant selon un temps et une vitesse donné
/// - Change le comportement du pieu avec une bool
/// </summary>
public class PieuMouvent : MonoBehaviour
{
    #region Variable 
    [Header ("Glisser la plaque de pression au qui l'active")]
    public GameObject plaqueDePression;

    [Header ("Dans quel direction ce retracte le pieu")]
    public string inWichDirection;

    [Header ("Vitesse de mouvement du pieu")]
    public float speedRetractage;
    public float speedRemiseEnPlace;

    [Header ("Temps de chaque phase")]
    public float tempsRetractage;
    public float tempsRemiseEnPlace;

    [Header ("Coché si le pieu doit rester retracté quand la plaque est active")]
    public bool stayActivate = false;

    private bool canRetracte = true;
    Vector2 retractage;
    Vector2 remiseEnPlace;
    #endregion

    private void Start()
    {
        switch (inWichDirection)
        {
            case ("Droite"):
                retractage = Vector2.right;
                remiseEnPlace = Vector2.left;
                break;

            case ("Gauche"):
                retractage = Vector2.left;
                remiseEnPlace = Vector2.right;
                break;

            case ("Haut"):
                retractage = Vector2.up;
                remiseEnPlace = Vector2.down;
                break;

            case ("Bas"):
                retractage = Vector2.down;
                remiseEnPlace = Vector2.up;
                break;
        } // Detecte la direction dans la quelle bouge le pieu et change les vecteur en conséquence
    }

    void Update()
    {
        ActivateFirstComportement();

        ActivateFirstPart();

        ActivateSecondPart();
    }

    void ActivateFirstComportement()
    {
        if (plaqueDePression.GetComponent<PlaqueDePression>().activeTrap == true && stayActivate == false)
        {
            if (canRetracte == true)
            {
                StartCoroutine(RetractagePieu());
            }
        }
    } // Fonction qui gére le premier comportement de déplacement du pieu

    void ActivateFirstPart()
    {
        if (plaqueDePression.GetComponent<PlaqueDePression>().activeTrap == true && stayActivate == true)
        {
            if (canRetracte == true)
            {
                StartCoroutine(Retractage());
            }
        }
    } // Première fonction qui gére le deuxième type de déplacement du pieu

    void ActivateSecondPart()
    {
        if (plaqueDePression.GetComponent<PlaqueDePression>().activeTrap == false && stayActivate == true && canRetracte == false)
        {
            StartCoroutine(RemiseEnPlace());
        }
    }  // Deuxième fonction qui gére le deuxième type de déplacement du pieu

    IEnumerator RetractagePieu()
    {
        canRetracte = false;
        GetComponentInParent<Rigidbody2D>().velocity = (retractage.normalized * speedRetractage * Time.fixedDeltaTime);
        yield return new WaitForSeconds(tempsRetractage);
        GetComponentInParent<Rigidbody2D>().velocity = (remiseEnPlace.normalized * speedRemiseEnPlace * Time.fixedDeltaTime);
        yield return new WaitForSeconds(tempsRemiseEnPlace);
        GetComponentInParent<Rigidbody2D>().velocity = (Vector2.zero);
        canRetracte = true;
    } // Coroutine qui retracte et remet en place le pieu selon un temps et une vitesse donné (premier mouvement)
    IEnumerator Retractage()
    {
        canRetracte = false;
        GetComponentInParent<Rigidbody2D>().velocity = (retractage.normalized * speedRetractage * Time.fixedDeltaTime);
        yield return new WaitForSeconds(tempsRetractage);
        GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
    } // Coroutine qui retracte le pieu et le garde dans cette position tant que la plaque de pression est activé (deuxième mouvement)
    IEnumerator RemiseEnPlace()
    {
        GetComponentInParent<Rigidbody2D>().velocity = (remiseEnPlace.normalized * speedRemiseEnPlace * Time.fixedDeltaTime);
        yield return new WaitForSeconds(tempsRemiseEnPlace);
        GetComponentInParent<Rigidbody2D>().velocity = (Vector2.zero);
        canRetracte = true;
    } // Coroutine qui remet en place le pieu et le stop quand la plaque de pression est désactivé (deuxième mouvement)
}
