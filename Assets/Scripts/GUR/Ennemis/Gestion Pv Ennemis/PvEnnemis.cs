using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Créateur : Guillaume Rogé 
/// Ce script permet de :
/// - Detecter la collision du khameo ou des particule d'eau
/// - En fonction du type d'ennemi soit infliger des dégat ou les rendre vulnérables
/// - Detruire les ennemis une fois leurs pv à 0
/// </summary>

public class PvEnnemis : MonoBehaviour
{
    #region Variable
    [Header ("Quel ennemi")]
    public bool flammeche;
    public bool mage;
    public bool golemLave;

    [Header ("Stats")]
    public float pv;
    public float waterForVulnerable;
    public float timeOfVulnerability;
    public float damageParticule;

   [HideInInspector]
    public bool kameoHit;

    private float stock;
    #endregion

    private void Start()
    {
        stock = waterForVulnerable;
    }

    void Update()
    {
        KhameoHitFlammeche();
        KhameoHitMage();
        KhameoHitGolemLave();
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

        if (mage == true && gameObject.GetComponent<MageMovement>().vunerableMage == false)
        {
            waterForVulnerable -= damageParticule;
        }

        if (golemLave == true && gameObject.GetComponent<GolemLaveMouvement>().vunerableGolem == false)
        {
            waterForVulnerable -= damageParticule;
        }
    }

    void KhameoHitMage()
    {
        if (mage == true && kameoHit == true && gameObject.GetComponent<MageMovement>().vunerableMage == false)
        {
            kameoHit = false;
            StartCoroutine(CooldownOfVulnerable());
        }
    } // Fonction qui rend vulnérable le mage quand il est touché par le khaméo

    void KhameoHitFlammeche()
    {
        if (flammeche == true && kameoHit == true)
        {
            pv = 0;
        }
    }  // Fonction qui set les pv à 0 de la flammèche quand elle se fait toucher par le khaméo

    void KhameoHitGolemLave()
    {
        if (golemLave == true && kameoHit == true && gameObject.GetComponent<GolemLaveMouvement>().vunerableGolem == false)
        {
            kameoHit = false;
            StartCoroutine(CooldownOfVulnerable());
        }
    } // Fonction qui rend vulnérable le golem de lave quand il est touché par le khaméo

    void EnnemyDestruction()
    {
        if (pv <= 0)
        {
            // Anim de mort
            Destroy(gameObject);
        }
    } // Fonction qui détruit les ennemis quand leur pv sont à 0 

    IEnumerator CooldownOfVulnerable()
    {
        if (mage == true)
        {
            gameObject.GetComponent<MageMovement>().vunerableMage = true;
            yield return new WaitForSeconds(timeOfVulnerability);
            gameObject.GetComponent<MageMovement>().vunerableMage = false;
        }

        if (golemLave == true)
        {
            gameObject.GetComponent<GolemLaveMouvement>().vunerableGolem = true;
            yield return new WaitForSeconds(timeOfVulnerability);
            gameObject.GetComponent<GolemLaveMouvement>().vunerableGolem = false;
        }
    } // Coroutine qui rend vulnérable les ennemis pendants x temps
}
