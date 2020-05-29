using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{

    //totem = ombre
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
    private float totemSpeedReturn;
    public Transform pointOfReturn;
    private Vector3 aimPosition;
    [SerializeField]
    private bool canMove;
    [SerializeField]
    private bool canFall;
    [SerializeField]
    private bool canReturn;
    [SerializeField]
    private bool canUp;
    [SerializeField]
    private GameObject meteorite;
    [SerializeField]
    private Transform[] pointOfMeteor;
    public bool isInAction;

    private Animator anim;
    private Animator animBoss;

    private void Start()
    {
        transform.position = pointOfReturn.position;
        totemRenderer.transform.localPosition = new Vector3(0, totemHight, 0);

        anim = GetComponentInChildren<Animator>();
        animBoss = BossManagerP.instance.GetComponent<Animator>();
        anim.SetBool("Phase2", true);
    }

    private void Update()
    {


        //go to point
        if (canMove && transform.position != aimPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, aimPosition, totemSpeedOfMovement * Time.deltaTime);
            totemRenderer.GetComponent<SpriteRenderer>().sortingOrder = 11;
        }
        else if (transform.position == aimPosition && canMove)
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
            canFall = false;
            Instantiate(meteorite, pointOfMeteor[Random.Range(0, pointOfMeteor.Length -1)].position, Quaternion.identity); 
            StartCoroutine(ReturnAtPoint());

        }

        //relève
        if (canUp && totemRenderer.transform.localPosition.y != totemHight)
        {
            totemRenderer.transform.position = Vector2.MoveTowards(totemRenderer.transform.position, new Vector2(transform.position.x,transform.position.y + totemHight), totemSpeedOfFall * Time.deltaTime); //possible de changer totemSpeedOffall si le totem doit changer de vitesse quand il remonte
            totemRenderer.GetComponent<SpriteRenderer>().sortingOrder = 11;
        }

        else if (totemRenderer.transform.localPosition.y == totemHight && canUp)
        {
            canUp = false;
            canReturn = true;
        }


        //retour
        if (canReturn && transform.position != aimPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, aimPosition, totemSpeedReturn * Time.deltaTime);
            anim.SetBool("IsShake", false);
            anim.SetBool("IsFall", false);
        }
        else if (transform.position == aimPosition && canReturn)
        {
            anim.SetBool("IsShake", false);
            anim.SetBool("IsFall", false);
            canReturn = false;
            isInAction = false;
        }


    }

    public void StartMovement(Vector3 aimPos)
    {
        aimPosition = aimPos;
        canMove = true;
        isInAction = true;
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

    public IEnumerator ReturnAtPoint()
    {
        FindObjectOfType<AudioManager>().Play("Boss totem");
        totemRenderer.GetComponent<SpriteRenderer>().sortingOrder = 10;
        yield return new WaitForSeconds(timeOnGround);
        canUp = true;
        aimPosition = pointOfReturn.position;
        totemRenderer.GetComponent<Collider2D>().enabled = false;
    }

    public void ForceReturn()
    {
        StopAllCoroutines();
        canFall = false;
        canMove = false;
        canReturn = true;
        canUp = false;
        aimPosition = pointOfReturn.position;
        totemRenderer.GetComponent<Collider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeHit(1);
        }
    }

}
