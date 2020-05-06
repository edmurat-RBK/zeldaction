using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

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

    private void Start()
    {
        player = PlayerManager.Instance.transform;
    }

    private void Update()
    {
        distanceToPlayer = (player.position - transform.position).magnitude;
        //go sur le player
        if (canMove && distanceToPlayer > minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, totemSpeedOfMovement * Time.deltaTime);
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
        }
        else if (totemRenderer.transform.localPosition.y == 0 && canFall)
        {
            totemRenderer.GetComponent<Collider2D>().enabled = true;
            canFall = false;
            Instantiate(meteorite, pointOfMeteor[Random.Range(0, pointOfMeteor.Length - 1)].position, Quaternion.identity);
            StartCoroutine(FallCD());

        }
        
        //remonter
        if (canUp && totemRenderer.transform.localPosition.y != totemHight)
        {
            totemRenderer.transform.position = Vector2.MoveTowards(totemRenderer.transform.position, new Vector2(transform.position.x, transform.position.y + totemHight), totemSpeedOfFall * Time.deltaTime); //possible de changer totemSpeedOffall si le totem doit changer de vitesse quand il remonte
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
        yield return new WaitForSeconds(timeOnGround);
        canUp = true;
        totemRenderer.GetComponent<Collider2D>().enabled = false;
        fallOnCD = true;
        yield return new WaitForSeconds(fallCD);
        fallOnCD = false;
    }

    public IEnumerator StartFall()
    {
        //lancer l'animation de l'objet sur le totemrenderer
        yield return new WaitForSeconds(timeBeforeFall);
        canFall = true;
    }
}
