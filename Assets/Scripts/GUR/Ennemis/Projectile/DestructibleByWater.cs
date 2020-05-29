using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleByWater : MonoBehaviour
{
    public enum whatObject
    {
        flamme,
        ennemiObject,
        totem,
    }

    public whatObject element;


    public float timeAnimation;
    private Animator anim;

    [HideInInspector]
    public bool canDeathSong;

    [HideInInspector]
    public bool khameoDetection;

    public bool particuleCanDestroy;

    private Collider2D boxDetection;
    private Collider2D boxDomage;
    private Collider2D bodyBlock;

    void Start()
    {
        if (element == whatObject.ennemiObject)
        {
            boxDetection = GetComponent<Collider2D>();
            boxDomage = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
        }

        if (element == whatObject.flamme)
        {

            boxDetection = GetComponent<Collider2D>();
            boxDomage = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
            bodyBlock = gameObject.transform.GetChild(1).GetComponent<Collider2D>();
        }

        anim = GetComponent<Animator>();  

        khameoDetection = false;
    }

    void Update()
    {
        if (khameoDetection == true)
        {
            if (element == whatObject.ennemiObject)
            {
                boxDetection.enabled = false;
                boxDomage.enabled = false;
            }

            if (element == whatObject.flamme)
            {
                boxDetection.enabled = false;
                boxDomage.enabled = false;
                bodyBlock.enabled = false;
            }

                canDeathSong = true;
            anim.SetBool("IsDead", true);
            Destroy(gameObject, timeAnimation);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (particuleCanDestroy == true)
        {
            if (element == whatObject.ennemiObject)
            {
                boxDetection.enabled = false;
                boxDomage.enabled = false;
            }

            if (element == whatObject.flamme)
            {
                boxDetection.enabled = false;
                boxDomage.enabled = false;
                bodyBlock.enabled = false;
            }

            canDeathSong = true;
            anim.SetBool("IsDead", true);
            Destroy(gameObject, timeAnimation);
        }
    }
}
