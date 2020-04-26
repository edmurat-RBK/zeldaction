using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCaissePlaque : MonoBehaviour
{
    public GameObject wichPlaque;
    public GameObject caisseBoisPrefab;

    private GameObject caisse;

    private bool lockSpawn;
    private int counter = 0;

    void Start()
    {
        lockSpawn = true;
    }

    void Update()
    {
        if (gameObject.GetComponent<GestionActivateur>().canActive == true && caisse == null)
        {
            caisse = Instantiate(caisseBoisPrefab, transform.position, transform.rotation);
        }

        
    }
}
