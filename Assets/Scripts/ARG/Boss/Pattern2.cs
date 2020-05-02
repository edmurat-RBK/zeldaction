using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class Pattern2 : MonoBehaviour
{
    #region Variable

    //slam pattern 2
    public GameObject[] spawnPointsForSlam;
    [SerializeField]
    private GameObject totem;
    [SerializeField]
    private int hightOfTotem;
    [SerializeField]
    private float speedOfTotem;
    private Vector3 pointBeforeImpact;
    private Vector3 pointOfImpact;
    [SerializeField]
    private GameObject totemShadow;
    [SerializeField]
    private Transform slamDestination;
    [SerializeField]
    private BoxCollider2D totemCollider;
    [SerializeField]
    private GameObject totemStase;
    [SerializeField]
    private GameObject projectil;
    private bool canLunchCoRoutine;
    private bool canSlam;
    private bool canSearchForPoint;
    private bool canReturn;
    private bool meteorCanStrike;
    [SerializeField]
    private GameObject totemShadowForPoint;
    private PlayerManager player;
    private GameObject shadow;

    public GameObject[] spawnPointsForMeteor;
    private Vector3 pointOfMeteorImpact;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //lunchSlamPoint();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
        }

        #region Phase
        if (totem.transform.position != pointBeforeImpact && canSearchForPoint == true) //le totem bouge jusqu"à la position au dessus de sa cible
        {
            totem.transform.position = Vector2.MoveTowards(totem.transform.position, pointBeforeImpact, speedOfTotem);
            canLunchCoRoutine = true;
        }
        else if (totem.transform.position == pointBeforeImpact )//si à la bonne position et qu'il ne peux plus chercher de point
        {
            canSearchForPoint = false;
            StartCoroutine("SlamInComing"); //lance la coroutine à l'infini
        }

        if (totem.transform.position != pointOfImpact && canSlam == true) //le totem slam le sol
        {
            totem.transform.position = Vector2.MoveTowards(totem.transform.position, pointOfImpact, speedOfTotem);
        }
        else if (totem.transform.position == pointOfImpact)
        {
            totemCollider.enabled = (true);
            canSlam = false;

            if (Vector2.Distance(totem.transform.position, player.transform.position) < 0.5f)
            {
                player.GetComponent<PlayerHealth>().TakeHit(1);
            }


            if (meteorCanStrike == true)
            {
                StartCoroutine("MeteorStrike");
            }

            StartCoroutine("SlamBack");
        }

        if (totem.transform.position != totemStase.transform.position && canReturn == true) //le totem retounrne à sa position initial
        {
            totem.transform.position = Vector2.MoveTowards(totem.transform.position, totemStase.transform.position, speedOfTotem);
        }
        else if (totem.transform.position == totemStase.transform.position)
        {
            canReturn = false;
            totemShadow.SetActive(false);
        }
        #endregion

    }


    #region Phase
    private void SlamPoint() //decide of the slam point position
    {
            //choix du spawn point
            canSearchForPoint = true;
            int i = Random.Range(0, 9);
            Transform slamDestination = spawnPointsForSlam[i].transform;
            pointBeforeImpact = new Vector3(slamDestination.position.x, slamDestination.position.y + hightOfTotem, 0);
            //activation de l'ombre du totem
            totemShadow.SetActive(true);

            //retour à l'upddate
            meteorCanStrike = true; //reset the meteor
    }


    private IEnumerator SlamInComing() //lunch the slam attack
    {
        if (canLunchCoRoutine == true)
        {
            canLunchCoRoutine = false;
            pointOfImpact = new Vector3(pointBeforeImpact.x, pointBeforeImpact.y - hightOfTotem, 0);
            //le totem tremble 
            totem.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(2);
            canSlam = true;
            totemShadow.SetActive(false);
            shadow = Instantiate(totemShadowForPoint, pointOfImpact,transform.rotation);
            //le totem slam vers le point
            totem.GetComponent<SpriteRenderer>().color = Color.white;
        }

    }

    private IEnumerator SlamBack() //return the totem
    {
            Destroy(shadow);
            totemCollider.enabled = (false);
            yield return new WaitForSeconds(1);
            canReturn = true;
            canLunchCoRoutine = false;
    }

    private IEnumerator MeteorStrike()
    {
        meteorCanStrike = false;
        int i = Random.Range(0, 9);
        Transform destinationOfMeteor = spawnPointsForMeteor[i].transform;
        pointOfMeteorImpact = new Vector3(destinationOfMeteor.position.x, destinationOfMeteor.position.y);
        yield return new WaitForSeconds(2);
        GameObject meteor = Instantiate(projectil, (pointOfImpact + new Vector3(0, 2.5f)), transform.rotation);
    }

    #endregion
}
