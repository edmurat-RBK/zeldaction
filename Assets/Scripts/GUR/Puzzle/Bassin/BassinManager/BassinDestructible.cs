using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

public class BassinDestructible : Singleton<CowManager>
{
    public List<GameObject> caisseDestructible = new List<GameObject>();

    public GameObject waterParticule;
    public GameObject bassin;

    void Start()
    {
        waterParticule.SetActive(false);
    }

    
    void Update()
    {
        caisseDestructible.RemoveAll(list_item => list_item == null);

        if (caisseDestructible.Count == 0)
        {
            waterParticule.SetActive(true);
            bassin.GetComponent<Bassin>().fullDestroy = true;
        }
    }
}
