using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PvEnnemis : MonoBehaviour
{
    public float pv;
    public float waterForVulnerable;
    public float timeOfVulnerability;

    public bool flammeche;
    public bool mage;
    public bool golemLave;

    public float damageParticule;

    public bool kameoHit;

    private float stock;
    private bool vulnerablePv;

    private void Start()
    {
        stock = waterForVulnerable;

        if (mage == true)
        {
            vulnerablePv = gameObject.GetComponent<MageMovement>().vunerable;
        }

        if (golemLave == true)
        {
            vulnerablePv = gameObject.GetComponent<GolemLaveMouvement>();
        }
    }

    void Update()
    {
        KhameoHitFlammeche();
        KhameoHitMage();
        EnnemyDestruction();

        if (waterForVulnerable <= 0)
        {
            StartCoroutine(CooldownOfVulnerable());
            waterForVulnerable = stock;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (flammeche == true)
        {
            pv -= damageParticule;
        }

        if (mage == true && vulnerablePv == false)
        {
            waterForVulnerable -= damageParticule;
        }
    }

    void KhameoHitMage()
    {
        if (mage == true && kameoHit == true && vulnerablePv == false)
        {
            kameoHit = false;
            StartCoroutine(CooldownOfVulnerable());
        }
    }

    void KhameoHitFlammeche()
    {
        if (flammeche == true && kameoHit == true)
        {
            pv = 0;
        }
    }

    void EnnemyDestruction()
    {
        if (pv <= 0)
        {
            // Anim de mort
            Destroy(gameObject);
        }
    }

    IEnumerator CooldownOfVulnerable()
    {
        vulnerablePv = true;
        yield return new WaitForSeconds(timeOfVulnerability);
        vulnerablePv = false;
      
    }
}
