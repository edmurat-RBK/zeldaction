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
    [Header ("Dans quel direction ce retracte le pieu")]
    public string inWichDirection;

    [Header ("Stats premier mouvement")]
    public float speedRetractage;
    public float speedRemiseEnPlace;
    [Space]
    public float tempsRetractage;
    public float tempsRemiseEnPlace;

    [Header ("Stats deuxième mouvement")]
    public float speed;
    [Space]
    public float startTime = 0f;
    public float maxTime;

    [Header ("activer si le pieu doit rester activé")]
    public bool stayActivate = false;

    private float actualTime;
    private bool canRetracte = true;
    Vector2 retractage;
    Vector2 remiseEnPlace;
    #endregion

    private void Start()
    {
        actualTime = startTime;
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
        ActivateSecondComportement();
    }

    void ActivateFirstComportement()
    {
        if (stayActivate == false)
        {
            if (gameObject.GetComponent<GestionActivateur>().canActive == true && canRetracte == true)
            {
                StartCoroutine(RetractagePieu());
            }
        }
    } // Fonction qui gére le premier comportement de déplacement du pieu

    void ActivateSecondComportement()
    {
        if (stayActivate == true)
        {
           if (gameObject.GetComponent<GestionActivateur>().canActive == true && actualTime <= maxTime)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = retractage.normalized * speed * Time.fixedDeltaTime;
                actualTime += Time.fixedDeltaTime;
            }


           if (gameObject.GetComponent<GestionActivateur>().canActive == false && actualTime > startTime)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = remiseEnPlace.normalized * speed * Time.fixedDeltaTime;
                actualTime -= Time.fixedDeltaTime;
            }

           if (actualTime <= startTime)
            {
                actualTime = startTime;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }

           if (actualTime >= maxTime)
           {
                actualTime = maxTime;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
           }
        }
    } // Première fonction qui gére le deuxième type de déplacement du pieu


    IEnumerator RetractagePieu()
    {
        canRetracte = false;
        GetComponent<Rigidbody2D>().velocity = (retractage.normalized * speedRetractage * Time.fixedDeltaTime);
        yield return new WaitForSeconds(tempsRetractage);
        GetComponent<Rigidbody2D>().velocity = (remiseEnPlace.normalized * speedRemiseEnPlace * Time.fixedDeltaTime);
        yield return new WaitForSeconds(tempsRemiseEnPlace);
        GetComponent<Rigidbody2D>().velocity = (Vector2.zero);
        canRetracte = true;
    } // Coroutine qui retracte et remet en place le pieu selon un temps et une vitesse donné (premier mouvement)
}
