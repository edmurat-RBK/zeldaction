using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    #region Variable
    [Header("Gestion du cône")]
    public float step;
    public float start;
    public int bulNumber;

    [Header("Projectile tiré")]
    public GameObject bulletPrefab;

    [Header("Vitesse des projectiles")]
    public float speed;

    Vector2 direction;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        fireball();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void fireball()
    {
        //var dir = new Vector2(player.transform.position.x - shotPoint.transform.position.x, player.transform.position.y - shotPoint.transform.position.y)
        //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - start);

        for (int i = 0; i < bulNumber; i++)
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + step);
            direction = transform.right;

            GameObject bul = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bul.GetComponent<Rigidbody2D>().velocity = direction * speed * Time.fixedDeltaTime;

            Destroy(bul, 3f);
        }
    }

    private void slamPoint()
    {

    }

    private void slamPlayer()
    {

    }
}
