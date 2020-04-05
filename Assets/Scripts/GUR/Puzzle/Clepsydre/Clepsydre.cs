using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clepsydre : MonoBehaviour
{
    public bool actifClepsydre;

    public int valeurParticule;
    public int maxStockage;

    public float remplissage;

    void Start()
    {
        actifClepsydre = false;
    }

    void Update()
    {
        if (remplissage > 0)
        {
            actifClepsydre = true;
            remplissage -= Time.fixedDeltaTime;
        }

        else if (remplissage <= 0)
        {
            actifClepsydre = false;
        }    
    }

    private void OnParticleCollision(GameObject other)
    {
        if (remplissage < maxStockage)
        {
            Debug.Log("detection trigger");
            remplissage += valeurParticule;
        }
        
    }
}
