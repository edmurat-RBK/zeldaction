using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleByWater : MonoBehaviour
{

    public bool khameoDetection;

    void Start()
    {
        khameoDetection = false;
    }

    void Update()
    {
        if (khameoDetection == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);
    }
}
