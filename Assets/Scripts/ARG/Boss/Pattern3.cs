using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class Pattern3 : MonoBehaviour
{
    #region Variable

    //slam pattern 3
    [SerializeField]
    private GameObject totem;
    [SerializeField]
    private int hightOfTotem;
    [SerializeField]
    private float speedOfTotem;
    private Vector3 pointOfImpact;
    [SerializeField]
    private GameObject totemShadow;
    [SerializeField]
    private BoxCollider2D totemCollider;
    [SerializeField]
    private GameObject projectil;
    private bool canLunchCoRoutine;
    private bool canSlam;
    private bool canMoveOnThePlayer = true;
    //public bool canReturn;
    private bool meteorCanStrike;
    [SerializeField]
    private GameObject totemShadowForPoint;
    private GameObject shadow;


    private PlayerManager player;
    private bool canSlamThePlayer;
    private Vector3 pointOfPlayer;
    [SerializeField]
    private int timeBeforeSlamPlayer;
    [SerializeField]
    private int timeOnGround;
    private bool canRepeat = true;

    //pour les meteors
    public GameObject[] spawnPointsForMeteor;
    private Vector3 pointOfMeteorImpact;
    [SerializeField]
    private int numberOfMeteor;
    private int actualMeteor;

    public bool vulnerable;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.Instance;
        pointOfPlayer = new Vector2(player.transform.position.x, player.transform.position.y + hightOfTotem);
        //vulnerable = GetComponent<BossManager>().vulnerable;
        StartCoroutine("WaitForSlam");
    }

    // Update is called once per frame
    void Update()
    {

        if (vulnerable == true)
        {
            Debug.Log("je rentre dedans");
            StopAllCoroutines();
            totem.SetActive(false);
        }

        pointOfPlayer = new Vector2(player.transform.position.x, player.transform.position.y + hightOfTotem);

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //}

        #region Phase
        if (totem.transform.position != pointOfPlayer && canMoveOnThePlayer && vulnerable == false) //le totem bouge jusqu"à la position au dessus du joueur
        {
            totem.transform.position = Vector2.MoveTowards(totem.transform.position, pointOfPlayer, speedOfTotem * Time.fixedDeltaTime);
            canLunchCoRoutine = true;
            totemShadow.SetActive(true);
        }
        else if (totem.transform.position == pointOfPlayer && canSlamThePlayer && vulnerable == false)//si au dessus du joueur et que la coroutine cooldown  à fini
        {
            StartCoroutine("SlamInComing");
        }

        if (totem.transform.position != pointOfImpact && canSlam == true && vulnerable == false) //le totem slam le sol
        {
            totem.transform.position = Vector2.MoveTowards(totem.transform.position, pointOfImpact, speedOfTotem * Time.fixedDeltaTime);
        }
        else if (totem.transform.position == pointOfImpact && vulnerable == false)
        {
            totemCollider.enabled = (true);
            canSlam = false;

            if (Vector2.Distance(totem.transform.position, player.transform.position) < 0.5f && vulnerable == false)
            {
                player.GetComponent<PlayerHealth>().TakeHit(1);
            }

            if (meteorCanStrike == true && vulnerable == false)
            {
                StartCoroutine("MeteorStrike");
            }

            if (canRepeat && vulnerable == false)
            {
                StartCoroutine("Repeat");
            }
            
        }
        #endregion
    }

    #region Phase


    public IEnumerator SlamInComing() //lunch the slam attack
    {
        if (canLunchCoRoutine == true)
        {
            Debug.Log("SlamInComing");
            canLunchCoRoutine = false;
            canMoveOnThePlayer = false; 
            pointOfImpact = new Vector3(pointOfPlayer.x,pointOfPlayer.y - hightOfTotem, 0);// le fait que une fois
            Debug.Log(pointOfImpact);
            //le totem tremble 
            totem.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(2);
            meteorCanStrike = true; //reset the meteor
            canSlam = true;
            totemShadow.SetActive(false);
            shadow = Instantiate(totemShadowForPoint, pointOfImpact, transform.rotation);
            //le totem slam vers le point
            totem.GetComponent<SpriteRenderer>().color = Color.white;
        }

    }

    private IEnumerator Repeat() //return the totem
    {
        Debug.Log("Repeat");
        canRepeat = false;
        yield return new WaitForSeconds(timeOnGround);
        totemShadow.SetActive(true);
        Destroy(shadow);
        totemCollider.enabled = (false);
        StartCoroutine("WaitForSlam");
        meteorCanStrike = false;
    }

    private IEnumerator MeteorStrike()
    {
        meteorCanStrike = false;
        int i = Random.Range(0, 9);
        Transform destinationOfMeteor = spawnPointsForMeteor[i].transform;
        pointOfMeteorImpact = new Vector3(destinationOfMeteor.position.x, destinationOfMeteor.position.y);
        yield return new WaitForSeconds(2);
        GameObject meteor = Instantiate(projectil, (pointOfMeteorImpact), transform.rotation);
        actualMeteor += 1;
        if (actualMeteor != numberOfMeteor)
        {
            StartCoroutine("MeteorStrike");
        }
        else actualMeteor = 0;
    }

    private IEnumerator WaitForSlam()
    {
        Debug.Log("Wait");
        canMoveOnThePlayer = true;
        yield return new WaitForSeconds(timeBeforeSlamPlayer);
        canLunchCoRoutine = true;
        canSlamThePlayer = true;
        canRepeat = true;
    }

    #endregion
}
