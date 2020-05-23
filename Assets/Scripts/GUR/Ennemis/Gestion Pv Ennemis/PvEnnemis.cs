using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemis;

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

    private Animator anim;

    private float stock;

    private bool lockMageVulné;
    private bool lockGolemVulné;
    #endregion

    private void Start()
    {
        lockGolemVulné = false;
        lockMageVulné = false;
        stock = waterForVulnerable;
        anim = GetComponent<Animator>();
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

        if (kameoHit == true)
        {
            kameoHit = false;
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
            StartCoroutine(CooldownOfVulnerable());            
        }
    } // Fonction qui rend vulnérable le mage quand il est touché par le khaméo

    void KhameoHitFlammeche()
    {
        if (flammeche == true && kameoHit == true)
        {
            pv = 0;
            anim.SetBool("IsDead", true);
        }
    }  // Fonction qui set les pv à 0 de la flammèche quand elle se fait toucher par le khaméo

    void KhameoHitGolemLave()
    {
        if (golemLave == true && kameoHit == true && gameObject.GetComponent<GolemLaveMouvement>().vunerableGolem == false)
        {
            StartCoroutine(CooldownOfVulnerable());
        }
    } // Fonction qui rend vulnérable le golem de lave quand il est touché par le khaméo

    void EnnemyDestruction()
    {
        if (pv <= 0)
        {
            if (flammeche == true)
            {
                anim.SetBool("IsDead", true);
                gameObject.GetComponent<FlammècheMouvement>().deathLockFlammeche = true;
                FindObjectOfType<AudioManager>().Play("DeathFlammeche");
                // Death de la flammèche
                Destroy(gameObject, 0.8f);
                gameObject.GetComponent<GestionDrop>().RamdomDrop();
            }

            if (mage == true)
            {
                anim.SetBool("IsDead", true); //YS MageDeath Timer pour laisser le temps a l'anim de mort de passer non?

                FindObjectOfType<AudioManager>().Play("DeathMage");

                gameObject.GetComponent<MageMovement>().deathLockMage = true;
                Destroy(gameObject, 0.8f);
                gameObject.GetComponent<GestionDrop>().RamdomDrop();
            }

            if (golemLave == true) 
            {
                anim.SetBool("IsDead",true);

                FindObjectOfType<AudioManager>().Play("DeathGolem");

                gameObject.GetComponent<GolemLaveMouvement>().deathLockGolem = true;
                Destroy(gameObject, 0.8f);
                gameObject.GetComponent<GestionDrop>().RamdomDrop();
            }
        }
    } // Fonction qui détruit les ennemis quand leur pv sont à 0 

    IEnumerator CooldownOfVulnerable()
    {
        if (mage == true && lockMageVulné == false)
        {
            lockMageVulné = true;
            anim.SetBool("IsEteint", true);

            gameObject.GetComponent<MageMovement>().vunerableMage = true;
            yield return new WaitForSeconds(timeOfVulnerability);
            gameObject.GetComponent<MageMovement>().vunerableMage = false;

            anim.SetBool("IsEteint", false);

            FindObjectOfType<AudioManager>().Play("MageAllumage");
            lockMageVulné = false;
        }

        if (golemLave == true && lockGolemVulné == false)
        {
            lockGolemVulné = true;

            anim.SetBool("IsSolidifying", true);

            gameObject.GetComponent<GolemLaveMouvement>().vunerableGolem = true;
            yield return new WaitForSeconds(timeOfVulnerability);

            anim.SetBool("IsSolidifying", false);

            yield return new WaitForSeconds(0.7f);
            gameObject.GetComponent<GolemLaveMouvement>().vunerableGolem = false;

            lockGolemVulné = false;
        }
    } // Coroutine qui rend vulnérable les ennemis pendants x temps


    public void EnnemiTakeDammage(int dammage)
    {
        if (golemLave == true)
        {
            if (gameObject.GetComponent<GolemLaveMouvement>().vunerableGolem == true)
            {
                anim.SetTrigger("IsStun");

                FindObjectOfType<AudioManager>().Play("GolemHit");
                pv -= dammage;
                
            }
        }

        if (mage == true)
        {         
            if (gameObject.GetComponent<MageMovement>().vunerableMage == true)
            {                
                anim.SetTrigger("IsDamaged");
                pv -= dammage;
            }           
        }
    }
}
