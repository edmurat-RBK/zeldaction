using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Créateur : Guillaume Rogé 
/// Ce script permet de :
/// - Faire bouger la flammèche dans 4 directions et ceci de façon aléatoire. 
/// - Lors d'une collision la flammèche par dans la direction opposé.
/// - Génerer les flammes derière le passage de la flammèche.
/// </summary>

public class FlammècheMouvement : MonoBehaviour
{
    #region Variable
    [Header("Stat de base")]
    [Space]
    public float speed;
    public int damage;

    [Header ("Comportement de la flamèche")]
    [Space]
    public float timeBtwDirection;

    [Header ("Prefab de la trainé de flame")]
    [Space]
    public GameObject flamePrefab;

    private Animator anim;

    Vector2 movement;

    private int direction;
    public float flameSpawnRate;

    public bool deathLockFlammeche;

    private bool lockDirection = true;
    private bool lockGeneration = true;

    private Rigidbody2D rbFlammeche;
    #endregion

    private void Start()
    {
        deathLockFlammeche = false;
        anim = GetComponent<Animator>();
        rbFlammeche = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (deathLockFlammeche == true)
        {
            rbFlammeche.velocity = Vector2.zero;
        }

        if (deathLockFlammeche == false)
        {
            if (GetComponentInChildren<ZoneAggro>().canAggro == true)
            {
                EnnemyMoving();
                Direction();
                FlameGeneration();
            } // Rentre si le joueur rentre dans la zone d'aggro

        }

        if (GetComponentInChildren<ZoneAggro>().canAggro == false) 
        {
            rbFlammeche.velocity = Vector2.zero;   
        } // Arrete la flammèche si le joueur sort da la zone d'aggro 
    }

    #region Partie sur la gestion du mouvement 
    void Direction()
    {
        if (lockDirection == true)
        {
            StartCoroutine(ChangementDirection());
        }
    }  // Fonction qui lance la coroutine de changement de direction

    IEnumerator ChangementDirection()
    {
        lockDirection = false;
        direction = Random.Range(1, 5);


        yield return new WaitForSeconds(timeBtwDirection);
        lockDirection = true;
    } // Coroutine qui génére aléatoirement une direction selon un temps donné
    
    void EnnemyMoving()
    {
        if (direction == 1)
        {
            movement = Vector2.up;
        }

        if (direction == 2)
        {
            movement = Vector2.down;
        }

        if (direction == 3)
        {
            movement = Vector2.right;
        }

        if (direction == 4)
        {
            movement = Vector2.left;
        }

        rbFlammeche.velocity = (movement.normalized * speed * Time.fixedDeltaTime);

        //youmna a ecrit ca


        anim.SetFloat("Horizontal", (GetComponent<Rigidbody2D>().velocity.x));

        anim.SetFloat("Vertical", (GetComponent<Rigidbody2D>().velocity.y));

    } // Fonction qui applique le changement de direction
    void OnCollisionEnter2D(Collision2D collision)
    {
       if (direction == 1)
       {
            direction = 2;
       }

       else if (direction == 2)
       {
            direction = 1;
       }

       else if (direction == 3)
       {
            direction = 4;
       }

       else if (direction == 4)
       {
            direction = 3;
       }
    } // Detecte la collision et fait partir la flammèche dans le sens opposé
    #endregion

    #region Partie sur la géneration des flammes
    void FlameGeneration()
    {
        if (lockGeneration == true)
        {
            StartCoroutine(FlameCoolDown());
        }
    }      // Fonction qui lance la coroutine de génération de flamme

    IEnumerator FlameCoolDown()
    {
        lockGeneration = false;
        GameObject flame = Instantiate(flamePrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(flameSpawnRate);
        lockGeneration = true;
    } // Coroutine qui génére des flammes selon un temps donné
    #endregion

}
