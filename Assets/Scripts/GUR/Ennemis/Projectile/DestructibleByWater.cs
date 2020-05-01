using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleByWater : MonoBehaviour
{
    private Animator anim;

    [HideInInspector]
    public bool khameoDetection;

    public bool particuleCanDestroy;
    void Start()
    {
        anim = GetComponent<Animator>();  

        khameoDetection = false;
    }

    void Update()
    {
        if (khameoDetection == true)
        {
            anim.SetBool("IsDead", true);

            Destroy(gameObject, 0.8f);
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
