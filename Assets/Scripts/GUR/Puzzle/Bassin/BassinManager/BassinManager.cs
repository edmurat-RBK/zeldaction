using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

public class BassinManager : Singleton<CowManager>
{
    public List<GameObject> mobSpawner = new List<GameObject>();

    public GameObject bassin;
    public Transform spawnCaissePoint;
    public GameObject caisseBois;

    public GameObject plaquePression;

    private List<GameObject> caiseStock = new List<GameObject>();

    private bool active = false;

    private bool lockGet;

    void Start()
    {
        lockGet = false;
    }

    
    void Update()
    {
        caiseStock.RemoveAll(list_item => list_item == null);

        if (lockGet == false)
        {
            if (plaquePression.GetComponent<PlaqueDePressionPierre>().activePlaquePierre == true)
            {
                lockGet = true;
                bassin.GetComponent<Bassin>().lockBassin = true;
            }

        }

        if (lockGet == true)
        {
            foreach (GameObject spawner in mobSpawner)
            {
                spawner.GetComponent<SpawnerMob>().canSpawn = false;
            }
        }

        if (lockGet == false)
        {
            Gestion();
        }
    }

    void Gestion()
    {
        if (bassin.GetComponent<Bassin>().remplissage > bassin.GetComponent<Bassin>().maxStockage)
        {
            active = true;

            if (caiseStock.Count <= 0)
            {
                GameObject caisse = Instantiate(caisseBois, spawnCaissePoint.transform.position, spawnCaissePoint.transform.rotation);
                caiseStock.Add(caisse.gameObject);
            }

            foreach (GameObject spawner in mobSpawner)
            {
                spawner.GetComponent<SpawnerMob>().canSpawn = true;
            }
        }

        else if (bassin.GetComponent<Bassin>().remplissage <= 0 && active == true)
        {
            active = false;

            foreach (GameObject gameObject in caiseStock)
            {
                Destroy(gameObject);
            }
            
            foreach (GameObject spawner in mobSpawner)
            {
                spawner.GetComponent<SpawnerMob>().canSpawn = false;
            }
        }
    }
}
