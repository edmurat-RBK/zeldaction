using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class Obsidian : MonoBehaviour
{
    public float timeBeforeDestrcution;

    public Sprite[] sprite;

    private SpriteRenderer obsiSprite;
    private BoxCollider2D hitDetection;
    public BoxCollider2D obsiHitBox;

    public bool playerOn;

    private float state;
    [SerializeField]
    private bool isObsi;

    public GameObject respawnPoint;

    void Start()
    {
        Debug.Log("OBSI");
        state = timeBeforeDestrcution / sprite.Length;
        hitDetection = GetComponent<BoxCollider2D>();
        obsiSprite = GetComponent<SpriteRenderer>();
        obsiSprite.enabled = false;
        obsiHitBox.enabled = true;
        hitDetection.enabled = true;
    }

    private void Update()
    {
        if (playerOn == true &&  isObsi == false)
        {
            PlayerManager.Instance.transform.position = respawnPoint.transform.position;
        }
    }


    private void OnParticleCollision(GameObject other)
    {
        obsiHitBox.enabled = false;
        hitDetection.enabled = false;
        obsiSprite.enabled = true;
        isObsi = true;

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
        isObsi = false;
    }
}
