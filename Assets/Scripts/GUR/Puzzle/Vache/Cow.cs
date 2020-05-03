using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    public float timeBtwBroutage;

    private bool canActivate;
    private bool lockCow;

    private CircleCollider2D circle;
    private BoxCollider2D box;

    private Animator anim;

    private bool lockBroutage;
    void Start()
    {
        circle = GetComponent<CircleCollider2D>();
        box = GetComponent<BoxCollider2D>();

        box.enabled = box.enabled = false;

        anim = GetComponent<Animator>();
        canActivate = false;
        lockCow = false;
    }

    
    void Update()
    {
        if (canActivate == true && lockCow == false)
        {
            if (Input.GetButton("B"))
            {
                lockCow = true;
                anim.SetBool("Water", true);

                gameObject.GetComponent<SpriteRenderer>().sortingOrder -= 1;

                box.enabled = box.enabled = true;
                circle.enabled = circle.enabled = false;

                gameObject.transform.position = CowManager.Instance.spawnTpCow.position;
                transform.GetChild(0).gameObject.SetActive(false);
                CowManager.Instance.SwitchHitBox();
            }
        }

        if (lockBroutage == false)
        {
            StartCoroutine(Broutage());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 31)
        {
            anim.SetBool("Player", true);
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 31)
        {
            anim.SetBool("Player", false);
            canActivate = false;
        }
    }


    IEnumerator Broutage()
    {
        lockBroutage = true;
        anim.SetBool("Broutage", true);
        yield return new WaitForSeconds(2.15f);
        anim.SetBool("Broutage", false);
        yield return new WaitForSeconds(timeBtwBroutage);
        lockBroutage = false;
    }
}
