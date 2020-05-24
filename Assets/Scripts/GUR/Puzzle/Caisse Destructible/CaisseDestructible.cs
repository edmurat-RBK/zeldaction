using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaisseDestructible : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Destruction()
    {
        FindObjectOfType<AudioManager>().Play("Totem death");
        anim.SetBool("IsDead", true);
        Destroy(gameObject, 1.20f);
    }
}
