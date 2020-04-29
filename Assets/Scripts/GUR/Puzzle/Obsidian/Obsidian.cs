using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsidian : MonoBehaviour
{
    public float timeBeforeDestrcution;

    private SpriteRenderer obsiSprite;
    private BoxCollider2D obsiHitBox;


    void Start()
    {


        obsiHitBox = GetComponent<BoxCollider2D>();
        obsiSprite = GetComponent<SpriteRenderer>();
        obsiSprite.enabled = false;
        obsiHitBox.enabled = true;
    }

    
    private void OnParticleCollision(GameObject other)
    {
        obsiHitBox.enabled = false;
        obsiSprite.enabled = true;

        StartCoroutine(RespawnObsi());
    }

    IEnumerator RespawnObsi()
    {
        yield return new WaitForSeconds(timeBeforeDestrcution);
        obsiHitBox.enabled = true;
        obsiSprite.enabled = false;
    }
}
