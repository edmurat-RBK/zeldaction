using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using System.Linq;

public class Totem2 : MonoBehaviour
{
    //totem ombre
    [SerializeField]
    private GameObject totemRenderer;
    [SerializeField]
    private float totemHight;
    [SerializeField]
    private float totemSpeedOfFall;
    [SerializeField]
    private float totemSpeedOfMovement;
    [SerializeField]
    private float timeOnGround;
    [SerializeField]
    private float timeBeforeFall;
    [SerializeField]
    private bool canMove;
    [SerializeField]
    private bool canFall;
    [SerializeField]
    private bool canUp;
    [SerializeField]
    private GameObject meteorite;
    [SerializeField]
    private Transform[] pointOfMeteor;
    private Transform player;
    private bool fallOnCD;
    [SerializeField]
    private float fallCD;
    private float distanceToPlayer;
    [SerializeField]
    private float minDistance;
    [SerializeField]
    private float minNumberOfMeteor;
    private float maxNumberOfMeteor;

    private Animator anim;
    private Animator animBoss;

    private void Start()
    {
        player = PlayerManager.Instance.transform;
        anim = GetComponentInChildren<Animator>();
        animBoss = BossManagerP.instance.GetComponent<Animator>();
        anim.SetBool("Phase2", false);
    }

    private void Update()
    {
        distanceToPlayer = (player.position - transform.position).magnitude;
        //go sur le player
        if (canMove && distanceToPlayer > minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, totemSpeedOfMovement * Time.deltaTime);
            totemRenderer.GetComponent<SpriteRenderer>().sortingOrder = 11;
        }
        else if (canMove && distanceToPlayer <= minDistance && fallOnCD == false)
        {
            canMove = false;
            StartCoroutine(StartFall());
        }

        //tomber
        if (canFall && totemRenderer.transform.localPosition.y != 0)
        {
            totemRenderer.transform.position = Vector2.MoveTowards(totemRenderer.transform.position, transform.position, totemSpeedOfFall * Time.deltaTime);
            anim.SetBool("IsFall", true);
        }
        else if (totemRenderer.transform.localPosition.y == 0 && canFall)
        {
            anim.SetBool("IsFall", false);
            totemRenderer.GetComponent<Collider2D>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
            canFall = false;
            List<Transform> listPointOfMeteor = pointOfMeteor.ToList();
            for (int i = 0; i < Random.Range(minNumberOfMeteor,maxNumberOfMeteor); i++)
            {
                Transform thisPoint = listPointOfMeteor[Random.Range(0, listPointOfMeteor.Count - 1)];
                Instantiate(meteorite, thisPoint.position, Quaternion.identity);
                listPointOfMeteor.Remove(thisPoint);
            }
            StartCoroutine(FallCD());

        }
        
        //remonter
        if (canUp && totemRenderer.transform.localPosition.y != totemHight)
        {
            totemRenderer.transform.position = Vector2.MoveTowards(totemRenderer.transform.position, new Vector2(transform.position.x, transform.position.y + totemHight), totemSpeedOfFall * Time.deltaTime); //possible de changer totemSpeedOffall si le totem doit changer de vitesse quand il remonte
            totemRenderer.GetComponent<SpriteRenderer>().sortingOrder = 11;
        }

        else if (totemRenderer.transform.localPosition.y == totemHight && canUp)
        {
            canUp = false;
            canMove = true;
        }
    }

    public void LaunchMovement()
    {
        canMove = true;
    }

    public IEnumerator FallCD()
    {
        FindObjectOfType<AudioManager>().Play("Boss totem");
        totemRenderer.GetComponent<SpriteRenderer>().sortingOrder = 10;
        yield return new WaitForSeconds(timeOnGround);
        canUp = true;
        totemRenderer.GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        fallOnCD = true;
        yield return new WaitForSeconds(fallCD);
        fallOnCD = false;
    }

    public IEnumerator StartFall()
    {
        anim.SetBool("IsShake", true);
        yield return new WaitForSeconds(timeBeforeFall);
        anim.SetBool("IsShake", false);
        animBoss.SetBool("TotemAttack", true);
        canFall = true;
        yield return new WaitForSeconds(0.483f);
        animBoss.SetBool("TotemAttack", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeHit(1);
        }
    }
}
