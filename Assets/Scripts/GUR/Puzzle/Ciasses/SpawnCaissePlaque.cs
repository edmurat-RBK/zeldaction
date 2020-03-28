using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCaissePlaque : MonoBehaviour
{
    public GameObject wichPlaque;
    public GameObject caisseBoisPrefab;

    private bool lockSpawn;

    void Start()
    {
        lockSpawn = true;
    }

    void Update()
    {
        if (wichPlaque.GetComponent<PlaqueDePression>().activeTrap == true && lockSpawn == true)
        {
            lockSpawn = false;
            Instantiate(caisseBoisPrefab, transform.position, transform.rotation);
        }

        if (wichPlaque.GetComponent<PlaqueDePression>().activeTrap == false && lockSpawn == false)
        {
            lockSpawn = true;
        }
    }
}
