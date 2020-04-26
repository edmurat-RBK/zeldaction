using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCaissePlaque : MonoBehaviour
{
    public GameObject caisseBoisPrefab;

    [HideInInspector]
    public GameObject caisse;

    void Update()
    {
        if (gameObject.GetComponent<GestionActivateur>().canActive == true && caisse == null)
        {
            caisse = Instantiate(caisseBoisPrefab, transform.position, transform.rotation);
        }

        
    }
}
