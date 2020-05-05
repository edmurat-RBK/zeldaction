using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsidian : MonoBehaviour
{
    public float timeBeforeDestrcution;

    public Sprite[] sprite;

    private SpriteRenderer obsiSprite;
    private BoxCollider2D hitDetection;
    public BoxCollider2D obsiHitBox;

    private float state;


    void Start()
    {
        state = timeBeforeDestrcution / sprite.Length;
        hitDetection = GetComponent<BoxCollider2D>();
        obsiSprite = GetComponent<SpriteRenderer>();
        obsiSprite.enabled = false;
        obsiHitBox.enabled = true;
        hitDetection.enabled = true;
    }

    
    private void OnParticleCollision(GameObject other)
    {
        obsiHitBox.enabled = false;
        hitDetection.enabled = false;
        obsiSprite.enabled = true;

        StartCoroutine(RespawnObsi());
    }

    IEnumerator RespawnObsi()
    {
        for (int i = 0; i < sprite.Length; i++)
        {
            obsiSprite.sprite = sprite[i];
            yield return new WaitForSeconds(state);
        }

        obsiHitBox.enabled = true;
        hitDetection.enabled = true;
        obsiSprite.enabled = false;
    }
}
