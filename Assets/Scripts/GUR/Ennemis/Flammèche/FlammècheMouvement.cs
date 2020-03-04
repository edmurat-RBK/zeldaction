﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Créateur : Guillaume Rogé 
/// Ce script permet de :
/// - faire bouger la flammèche dans 4 directions et ceci de façon aléatoire. 
/// - Lors d'une collision la flammèche par dans la direction opposé.
/// - Génerer les flammes derière le passage de la flammèche.
/// </summary>

public class FlammècheMouvement : MonoBehaviour
{
    #region Variable
    [Header("Stat de base")]
    [Space]
    public float pv;
    public float speed;
    public float damage;

    [Header ("Comportement de la flamèche")]
    [Space]
    public float timeBtwDirection;

    [Header ("Prefab de la trainé de flame")]
    [Space]
    public GameObject flamePrefab;

    Vector2 movement;

    private int direction;
    private float flameSpawnRate = 0.5f;

    private bool lockDirection = true;
    private bool lockGeneration = true;
    #endregion

    void Update()
    {
        EnnemyMoving();
        Direction();
        FlameGeneration();
    }

    void Direction()
    {
        if (lockDirection == true)
        {
            StartCoroutine(ChangementDirection());
        }
    }  // Fonction qui lance la coroutine de changement de direction
    
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

        GetComponent<Rigidbody2D>().velocity = (movement.normalized * speed * Time.fixedDeltaTime);
    } // Fonction qui applique le changement de direction
    void FlameGeneration()
    {
        if (lockGeneration == true)
        {
            StartCoroutine(FlameCoolDown());
        }
    }      // Fonction qui lance la coroutine de génération de flamme

    IEnumerator ChangementDirection()
    {
        lockDirection = false;
        direction = Random.Range(1, 5);
        yield return new WaitForSeconds(timeBtwDirection);
        lockDirection = true;
    } // Coroutine qui génére aléatoirement une direction selon un temps donné
    IEnumerator FlameCoolDown()
    {
        lockGeneration = false;
        GameObject flame = Instantiate(flamePrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(flameSpawnRate);
        lockGeneration = true;
    } // Coroutine qui génére des flammes selon un temps donné

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
}
