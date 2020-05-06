using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

public class BassinDestructible : Singleton<CowManager>
{
    public List<GameObject> caisseDestructible = new List<GameObject>();

    public GameObject bassin;

    void Start()
    {
        
    }

    
    void Update()
    {
        caisseDestructible.RemoveAll(list_item => list_item == null);

        if (caisseDestructible.Count == 0)
        {
            bassin.GetComponent<Bassin>().fullDestroy = true;
        }
    }
}
