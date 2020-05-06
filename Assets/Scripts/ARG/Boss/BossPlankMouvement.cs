using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlankMouvement : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }

    #region Variable

    [Header("Dans quel direction ce retracte le pieu")]
    // public string inWichDirection;
    public Direction inWichDirection;


    [Header("Stats premier mouvement")]
    public float speedRetractage;
    public float speedRemiseEnPlace;
    [Space] public float tempsRetractage;
    public float tempsRemiseEnPlace;

    [Header("Stats deuxième mouvement")]
    public float speed;
    [Space] public float startTime = 0f;
    public float maxDist;

    [Header("activer si le pieu doit rester activé")]
    public bool stayActivate = false;

    private Vector3 startPosition;

    private bool lockStop;

    public float actualDist;
    private bool canRetracte = true;
    Vector2 retractage;
    Vector2 remiseEnPlace;
    public bool vulnerable;
    public GameObject boss;
    #endregion

    private void Start()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        actualDist = startTime;

        switch (inWichDirection)
        {
            case Direction.Right:
                retractage = Vector2.right;
                break;

            case Direction.Left:
                retractage = Vector2.left;
                break;

            case Direction.Up:
                retractage = Vector2.up;
                break;

            case Direction.Down:
                retractage = Vector2.down;
                break;
        } // Detecte la direction dans la quelle bouge le pieu et change les vecteur en conséquence

        remiseEnPlace = -retractage;
        vulnerable = boss.GetComponent<BossManager>().vulnerable;
    }

    void Update()
    {
        if (stayActivate == false)
        {
            ActivateFirstComportement();
        }
        else
        {
            ActivateSecondComportement();
        }

        //pour le boss
        if (actualDist >= maxDist && boss.GetComponent<BossManager>().plankMouvement == true)
        {
            boss.GetComponent<BossManager>().vulnerable = true;
            boss.GetComponent<BossManager>().IsVulnerable();
        }
        else
        {
            boss.GetComponent<BossManager>().vulnerable = false;
            boss.GetComponent<BossManager>().IsVulnerable();
        }
    }

    void ActivateFirstComportement()
    {
        if (gameObject.GetComponent<GestionActivateur>().canActive == true && canRetracte == true)
        {
            StartCoroutine(RetractagePieu());
        }
    } // Fonction qui gére le premier comportement de déplacement du pieu

    void ActivateSecondComportement()
    {
        actualDist = (transform.position - startPosition).magnitude;

        if (gameObject.GetComponent<GestionActivateur>().canActive == true)
        {
            Debug.Log("je rentre");
            if (actualDist < maxDist)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = retractage.normalized * speed * Time.fixedDeltaTime;
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
        else
        {
            if (actualDist > 0.1f)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = remiseEnPlace.normalized * speed * Time.fixedDeltaTime;
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    IEnumerator RetractagePieu()
    {
        canRetracte = false;
        GetComponent<Rigidbody2D>().velocity = (retractage.normalized * speedRetractage * Time.fixedDeltaTime);
        yield return new WaitForSeconds(tempsRetractage);
        GetComponent<Rigidbody2D>().velocity = (remiseEnPlace.normalized * speedRemiseEnPlace * Time.fixedDeltaTime);
        yield return new WaitForSeconds(tempsRemiseEnPlace);
        GetComponent<Rigidbody2D>().velocity = (Vector2.zero);
        canRetracte = true;
    } // Coroutine qui retracte et remet en place le pieu selon un temps et une vitesse donné (premier mouvement)
}
