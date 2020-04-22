using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Créateur : Guillaume Rogé 
/// Ce script permet de : 
/// - Détruire les flammes génere par la flammèche selon un temps donné
/// </summary>

public class FlameduFlammèche : MonoBehaviour
{
    [Header ("Temps avant que la flamme s'auto détruise")]
    public float timesBeforeDestruction;
    private float timeAnimation;

    private Animator anim;
    void Start()
    {
        timeAnimation = timesBeforeDestruction - 0.3f;
        anim = GetComponent<Animator>();
        StartCoroutine(TimeBeforeDestruction());

        Destroy(gameObject, timesBeforeDestruction);
    }
    IEnumerator TimeBeforeDestruction() // Coroutine qui permet de détruire la flamme une fois géneré selon un temps donné 
    {
        yield return new WaitForSeconds(timeAnimation);

        anim.SetBool("IsDead", true);

    }
}
