using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Manager;

/// <summary>
/// Créateur : Guillaume Rogé
/// Ce script permet de : 
/// - Désactiver les hitbox des courants d'eau quand le joueur va sur une caisse
/// - Faire spawn d'activer des hitbox quand le joueur est sur une caisse pour l'empécher de tomber dans le courant
/// - Déplacer le joueur avec la caisse
/// </summary>

public class CubeBois : MonoBehaviour
{
    #region Variable
    [Header ("Glisser les hitbox de la caisse")]
    public GameObject hitboxVerticale;
    public GameObject hitboxHorizontale;

    [Header ("Mettre la vitesse de déplacement du courant")]
    public float speedOfWater;

    [HideInInspector]
    public string wichDirection;
    [HideInInspector]
    public bool canRedirect = true;
    [HideInInspector]
    public bool notStop;

    private Vector2 direction;
    private GameObject[] courantEau;
    #endregion

    private void Start()
    {
        notStop = true;
        courantEau = GameObject.FindGameObjectsWithTag("Courant"); // Permet de récuperer toutes les hitbox des courants d'eau et de les stocker dans un tableau
        hitboxVerticale.SetActive(false);
        hitboxHorizontale.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 31) // 29 = layer Player
        {
            switch (wichDirection) // Permet de récuperer la direction dans la quel va la caisse et d'activer les bonne hitbox
            {
                case ("Bas"):
                    hitboxVerticale.SetActive(true);
                    hitboxHorizontale.SetActive(false);
                    direction = Vector2.down;
                   
                break;

                case ("Haut"):
                    hitboxVerticale.SetActive(true);
                    hitboxHorizontale.SetActive(false);
                    direction = Vector2.up;
                    break;

                case ("Droite"):
                    hitboxVerticale.SetActive(false);
                    hitboxHorizontale.SetActive(true);
                    direction = Vector2.right;
                    break;

                case ("Gauche"):
                    hitboxVerticale.SetActive(false);
                    hitboxHorizontale.SetActive(true);
                    direction = Vector2.left;
                    break;
            }

            for (int i = 0; i < courantEau.Length; i++) // Permet de désactiver toute les hitbox des courants d'eau 
            {
                courantEau[i].SetActive(false);
            }

            if (notStop == true)
            {
                PlayerManager.Instance.playerRigidBody.AddForce(direction * speedOfWater); // Permet de transporter le joueur sur la caisse
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 31) // 29 = layer Player
        {
            for (int i = 0; i < courantEau.Length; i++) // Permet d'activer toute les hitbox des courants d'eau
            {
                courantEau[i].SetActive(true);
            }

            hitboxVerticale.SetActive(false);
            hitboxHorizontale.SetActive(false);
        }
    }
}
