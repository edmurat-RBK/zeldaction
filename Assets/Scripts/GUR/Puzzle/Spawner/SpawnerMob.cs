﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Créateur : Guillaume Rogé
/// Ce script permet de : 
/// - Faire spawn un ennemi toute les x seconde
/// - De caper le nombre d'ennemis pour pas en avoir trop
/// </summary>

public class SpawnerMob : MonoBehaviour
{
    #region Variable
    public int maxMob;
    public float timeBtwSpawn;
    public float radius;

    [HideInInspector]
    public List<GameObject> isAlive = new List<GameObject>();

    public bool canSpawn;

    public GameObject monster;
    private int numOfEnemy;
    private bool lockSpawn;
    private Vector3 spawnPosition;
    #endregion

    void Start()
    {
        lockSpawn = false;
    }

   
    void Update()
    {
        //isAlive.RemoveAll(list_item => list_item == null);
        numOfEnemy = isAlive.Count;

        if (numOfEnemy < maxMob)
        {
            if (lockSpawn == false && canSpawn == true)
            {
                spawnPosition = Random.insideUnitCircle * radius;
                GameObject ennemy = Instantiate(monster, (transform.position + spawnPosition), transform.rotation);
                isAlive.Add(ennemy);
                StartCoroutine(SpawnCoolDown());
            }
        }

        foreach (GameObject gameObject in isAlive)
        {
            if (gameObject == null)
            {
                isAlive.Remove(gameObject);
                StartCoroutine(SpawnCoolDown());
                isAlive.RemoveAll(list_item => list_item == null);
            }
        }
    }

   
    IEnumerator SpawnCoolDown()
    {
        lockSpawn = true;
        yield return new WaitForSeconds(timeBtwSpawn);
        lockSpawn = false;
    }
}
