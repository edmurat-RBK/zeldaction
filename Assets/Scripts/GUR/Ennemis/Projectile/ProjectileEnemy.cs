using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    public int damage;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //infliger les dégats au joueur
            Destroy(gameObject);
        }
        anim.SetBool("IsDead", true);
        Destroy(gameObject, 0.8f);
    }
}
