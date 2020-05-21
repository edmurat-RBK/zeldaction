using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCaissePlaque : MonoBehaviour
{
    public GameObject caisseBoisPrefab;

    public bool desactivate;

    [HideInInspector]
    public GameObject caisse;

    void Update()
    {
        if (desactivate == true)
        {
            if (UpgradesManager.List["finishShaman"] == false)
            {
                if (gameObject.GetComponent<GestionActivateur>().canActive == true && caisse == null)
                {
                    caisse = Instantiate(caisseBoisPrefab, transform.position, transform.rotation);
                }
            }
        }
        else
        {
            if (gameObject.GetComponent<GestionActivateur>().canActive == true && caisse == null)
            {
                caisse = Instantiate(caisseBoisPrefab, transform.position, transform.rotation);
            }
        }
        
    }
}
