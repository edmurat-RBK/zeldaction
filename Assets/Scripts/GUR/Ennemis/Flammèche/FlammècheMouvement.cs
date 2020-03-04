using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammècheMouvement : MonoBehaviour
{
    [Header("Stat de base")]
    public float speed;
    public float damage;
    public float timeBtwDirection;

    public GameObject flamePrefab;

    Vector2 movement;

    private int direction;
    private float flameSpawnRate = 0.5f;
    private bool lockDirection = true;
    private bool lockGeneration = true;
   
    void Update()
    {
        Direction();
        EnnemyMoving();
        FlameGeneration();
    }

    void Direction()
    {
        if (lockDirection == true)
        {
            StartCoroutine(ChangementDirection());
        }
    }
    void EnnemyMoving()
    {
        if (direction == 1)
        {
            movement = Vector2.up;
        }

        if (direction == 2)
        {
            movement = Vector2.down;
        }

        if (direction == 3)
        {
            movement = Vector2.right;
        }

        if (direction == 4)
        {
            movement = Vector2.left;
        }

        GetComponent<Rigidbody2D>().velocity = (movement.normalized * speed * Time.fixedDeltaTime);
    }
    void FlameGeneration()
    {
        if (lockGeneration == true)
        {
            StartCoroutine(FlameCoolDown());
        }
    }

    IEnumerator ChangementDirection()
    {
        lockDirection = false;
        direction = Random.Range(1, 5);
        yield return new WaitForSeconds(timeBtwDirection);
        lockDirection = true;
    }
    IEnumerator FlameCoolDown()
    {
        lockGeneration = false;
        GameObject flame = Instantiate(flamePrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(flameSpawnRate);
        lockGeneration = true;
    }
}
