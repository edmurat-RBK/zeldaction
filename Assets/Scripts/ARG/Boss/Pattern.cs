using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class Pattern : MonoBehaviour
{
    #region Variable
    ////méthode Guillaume
    //[Header("Gestion du cône")]
    //public float step;
    //public float start;
    //public int bulNumber;

    //[Header("Projectile tiré")]
    //public GameObject bulletPrefab;

    //[Header("Vitesse des projectiles")]
    //public float speed;

    //Vector2 direction;
    //private Transform player;

    //méthode Samuel
    //[Range(0.1f, 20f)]
    //public int numberOfBullet;
    //public GameObject bullets;
    //[Range(0.1f, 100f)]
    //public int bulletsSpeed;
    //[Range(0.1f, 20f)]
    //public int bulletsDispersion;
    //public GameObject bulletParent;

    //méthode Guillaume 2
    public GameObject shotPoint;
    [Header("Onnde de choc")]
    public GameObject chocWave;
    private Transform player;
    private int numberOfWaves;
    private int numberOfTheWave;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.Instance.transform;
        fireball();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void fireball()
    {
        ////méthode Guillaume
        //var dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y); // Permet d'orienter le shotPoint vers le joueur
        //var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - start);

        //for (int i = 0; i < bulNumber; i++)
        //{
        //    transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + step);
        //    direction = transform.right;

        //    GameObject bul = Instantiate(bulletPrefab, transform.position, transform.rotation);
        //    bul.GetComponent<Rigidbody2D>().velocity = direction * speed * Time.fixedDeltaTime;

        //    Destroy(bul, 10f);
        //}

        ////samuel méthode
        //for (int i = 0; i < numberOfBullet; i++)
        //{
        //    float radius = numberOfBullet;
        //    float angle = i * -Mathf.PI / radius;
        //    Vector3 newPos = transform.position + (new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0)) / bulletsDispersion;
        //    GameObject bulletInstantiate = Instantiate(bullets, newPos, Quaternion.Euler(0, 0, 0));
        //    bulletInstantiate.transform.SetParent(bulletParent.transform);
        //    Vector2 distanceSpawns = new Vector2(newPos.x - transform.position.x,
        //                                         newPos.y - transform.position.y);
        //    Rigidbody2D bulletRigidbody = bulletInstantiate.GetComponent<Rigidbody2D>();
        //    bulletRigidbody.velocity = distanceSpawns * bulletsSpeed;
        //}

        //méthode Guillaume 2
        numberOfWaves = Random.Range(2, 5);
        StartCoroutine("createWave");
    }

    private IEnumerator createWave()
    {
        var dir = new Vector2(player.transform.position.x - shotPoint.transform.position.x, player.transform.position.y - shotPoint.transform.position.y); // Permet d'orienter le shotPoint vers le joueur
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        shotPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        GameObject wave = Instantiate(chocWave, shotPoint.transform.position, shotPoint.transform.rotation); // spawn l'onde de choc
        Destroy(wave, 0.5f);
        yield return new WaitForSeconds(2);
        numberOfTheWave += 1;
        if ( numberOfTheWave!= numberOfWaves)
        {
            StartCoroutine("createWave");
        }
        
    }

    private void slamPoint()
    {

    }

    private void slamPlayer()
    {

    }
}
