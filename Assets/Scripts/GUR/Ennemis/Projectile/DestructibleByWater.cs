﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleByWater : MonoBehaviour
{
    [HideInInspector]
    public bool khameoDetection;

    public bool particuleCanDestroy;
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
        if (particuleCanDestroy == true)
        {
            Destroy(gameObject);
        }
    }
}
