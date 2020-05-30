using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Créateur : Guillaume Rogé
/// Ce script permet de : 
/// - Faire spawn des caisses toute les x seconds
/// </summary>

public class SpawnerCaisse : MonoBehaviour
{
    #region Variable
    public float spawnRate;
    public GameObject caissePrefab;
    private bool canSpawn = true;
    #endregion
    private void Start()
    {
        //canSpawn = true;
    }

    private void OnEnable()
    {
        if (canSpawn == false)
        {
            StartCoroutine(Initialisation());
        }
    }

    void Update()
    {
        if (canSpawn == true)
        {   
            StartCoroutine(SpawnCaisse());
        }
    }

    IEnumerator SpawnCaisse()
    {
        canSpawn = false;
        Instantiate(caissePrefab, gameObject.transform.position, gameObject.transform.rotation);
        yield return new WaitForSeconds(spawnRate);
        canSpawn = true;
    } // Permet de faire spawn une caisse toute les x seconde

    IEnumerator Initialisation()
    {
        yield return new WaitForSeconds(1f);
        canSpawn = true;
    }
}
