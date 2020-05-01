using System.Collections;
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

    private List<GameObject> isAlive = new List<GameObject>();

    public GameObject monster;
    private int numOfEnemy;
    private bool lockSpawn;
    private Vector2 spawnPosition;
    #endregion

    void Start()
    {
        lockSpawn = false;
    }

   
    void Update()
    {
        numOfEnemy = isAlive.Count;

        if (numOfEnemy < maxMob)
        {
            if (lockSpawn == false)
            {
                spawnPosition = Random.insideUnitCircle * radius;
                GameObject ennemy = Instantiate(monster, spawnPosition, transform.rotation);
                isAlive.Add(ennemy);
                StartCoroutine(SpawnCoolDown());
            }
        }

        foreach (GameObject gameObject in isAlive)
        {
            if (gameObject == null)
            {
                isAlive.Remove(gameObject);
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
