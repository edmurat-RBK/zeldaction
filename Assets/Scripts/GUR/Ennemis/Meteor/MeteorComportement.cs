using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Créateur : Guillaume Rogé
/// Ce script permet de :
///  - activer et désactivé la hitbox qui inflige les dégats
///  - DETRUIRE la meteor après la fin de l'animation :)
/// </summary>

public class MeteorComportement : MonoBehaviour
{
    #region Variable
    [Header ("Object qui contient l'hitbox dmg meteor")]
    public GameObject damageHitBox;
   
    [HideInInspector]
    public bool damageActive;

    private bool canDestroy;
    private bool lockDamage;
    #endregion

    void Start()
    {
        damageHitBox.SetActive(false);
        damageActive = false;
        canDestroy = true;
        lockDamage = true;
    }

    void Update()
    {
        if (damageActive == true && lockDamage == true)
        {
            StartCoroutine(ActiveDamage());
        }

        if (canDestroy == true)
        {
            StartCoroutine(DestroyMeteor());
        }
    }

    IEnumerator ActiveDamage()
    {
        damageActive = false;
        lockDamage = false;
        damageHitBox.SetActive(true);
        yield return new WaitForEndOfFrame();
        damageHitBox.SetActive(false);
    } // Permet d'activé puis de désactivé la hitbox qui inflige des dégats 

    IEnumerator DestroyMeteor()
    {
        canDestroy = false;
        yield return new WaitForSeconds(4f);
        GameObject.Destroy(gameObject);
    } // Permet de détruire la méteor après un temps donné 
}
