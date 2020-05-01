using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMob : MonoBehaviour
{
    public enum Ennemy
    {
        flammeche,
        mage,
        golem
    }
    
    public int maxMob;
    public float timeBtwSpawn;
    public float radius;

    public GameObject enemy;

    private GameObject monster;
    public int numOfEnemy;
    private bool lockSpawn;
    private Vector2 spawnPosition;

    void Start()
    {
        lockSpawn = false;
    }

   
    void Update()
    {
        if (numOfEnemy < maxMob)
        {
            if (lockSpawn == false)
            {
                numOfEnemy += 1;
                spawnPosition = Random.insideUnitCircle * radius;
                monster = Instantiate(enemy, spawnPosition, transform.rotation);
                StartCoroutine(SpawnCoolDown());
  
            }
        }

        if (monster.activeSelf == false)
        {
            numOfEnemy -= 1;
        }
     
    }

    


    IEnumerator SpawnCoolDown()
    {
        lockSpawn = true;
        yield return new WaitForSeconds(timeBtwSpawn);
        lockSpawn = false;
    }
}
