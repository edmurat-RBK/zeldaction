using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

/// <summary>
/// Créateur : Guillaume Rogé
/// Ce script permet de : 
/// - Detecter quand le raycast kameheaumeheau touche une caisse
/// - Bougé la caisse dans la direction du kameheaumeheau 
/// - Choisir entre deux comportement de déplacement de la caisse
/// </summary>

public class CaisseEnPierre : MonoBehaviour
{
    #region Variable
    [Header ("Choix du comportement")]
    public bool withOutInertie;
    public bool withInertie;

    [Header ("Vitesse de déplacement")]
    public float speed;

    [Header ("Pour le mouvement sans inertie")]
    public float tempsMouvement;

    [Header ("Pour le mouvement avec inertie")]
    public float freinage;

    private GameObject player;
    private Rigidbody2D rbPierre;

    private float actualSpeed;
    private bool canMove;

    [HideInInspector]
    public bool move;
    #endregion

    private void Start()
    {
        rbPierre = gameObject.GetComponent<Rigidbody2D>();
        rbPierre.isKinematic = true;
        player = GameObject.FindWithTag("Player");
        actualSpeed = speed;

        move = false;
        canMove = true;
    }

    void Update()
    {
        MouvementInertie(); // Fonction qui gére le mouvement avec inertie

        Mouvement(); // Fonction qui gére le mouvement sans inertie (freinement instantané)
    }

    void MouvementInertie()
    {
        if (withInertie == true)
        {
            if (move == true) // La bool pas en true si le raycast a touche (dans le script du kameheaumeheau)
            {
                canMove = false; 
            }

            if (canMove == false) // lance le mouvement
            {
                speed -= (Time.fixedDeltaTime * freinage); // reduit la speed au cour du temps 
                rbPierre.velocity = player.GetComponent<Kameheaumeheau>().beamDir * speed * Time.fixedDeltaTime;
            }

            if (speed <= 0) // quand la vitesse atteind 0 arréte le mouvement et reset les valeurs
            {
                canMove = true;
                move = false;
                speed = actualSpeed;
            }
        }
    }

    void Mouvement()
    {
        if (withOutInertie == true)
        {
            if (move == true) // La bool pas en true si le raycast a touche (dans le script du kameheaumeheau)
            {
                move = false;
                if (canMove == true) // lance le mouvement
                {
                    StartCoroutine(TempsGlissement());
                }
            }
        }
    }

    IEnumerator TempsGlissement() // Coroutine qui gère le mouvement sans inertie
    {
        FindObjectOfType<AudioManager>().Play("CaisseP poussé");

        canMove = false;
        rbPierre.isKinematic = false;
        rbPierre.velocity = player.GetComponent<Kameheaumeheau>().beamDir * speed * Time.fixedDeltaTime;
        yield return new WaitForSeconds(tempsMouvement);
        rbPierre.velocity = Vector2.zero;
        rbPierre.isKinematic = true;
        canMove = true;
        FindObjectOfType<AudioManager>().Stop("CaisseP poussé");
    }
}
