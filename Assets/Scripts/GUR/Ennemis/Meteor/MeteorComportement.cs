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
    public int damage;
    private CapsuleCollider2D hitBox;
    #endregion

    void Start()
    {
        hitBox = GetComponent<CapsuleCollider2D>();
        hitBox.enabled = false;

        FindObjectOfType<AudioManager>().Play("Meteor");

        StartCoroutine(ActivationDomage());
        Destroy(gameObject, 4f);
    }


    IEnumerator ActivationDomage()
    {
        yield return new WaitForSeconds(2.57f);
        hitBox.enabled = true;
        FindObjectOfType<AudioManager>().Play("Meteor impact");
        yield return new WaitForSeconds(0.05f);
        hitBox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeHit(damage);
        }
    }
}
