using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class Pattern : MonoBehaviour
{
    #region Variable
    //waves of fire
    public GameObject shotPoint;
    [Header("Onnde de choc")]
    public GameObject chocWave;
    private Transform player;
    private int numberOfWaves;
    private int numberOfTheWave;
    public bool canFire;

    //slam pattern 2
    public GameObject[] spawnPointsForSlam = new GameObject[9];
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
    private GameObject shadow;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.Instance.transform;
        canFire = true;
        //lunchSlamPoint();
        //StartCoroutine("CreateWave");


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            WaveOfFlame();
        }

        #region Phase 2
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

    #region Phase 1
    private void WaveOfFlame()
    {
        if (canFire == true)
        {
            numberOfWaves = Random.Range(2, 5);
            numberOfTheWave = 0;
            StartCoroutine("CreateWave");
            Debug.Log(numberOfWaves);
        }

    }
    private IEnumerator CreateWave() //fire multiple waves of fire
    {
        
        canFire = false;
        var dir = new Vector2(player.transform.position.x - shotPoint.transform.position.x, player.transform.position.y - shotPoint.transform.position.y); // Permet d'orienter le shotPoint vers le joueur
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        shotPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject wave = Instantiate(chocWave, shotPoint.transform.position, shotPoint.transform.rotation); // spawn l'onde de choc
        Destroy(wave, 0.5f);
        yield return new WaitForSeconds(2);
        numberOfTheWave += 1;
        if (numberOfTheWave != numberOfWaves)
        {
            Debug.Log("je suis al");
            StartCoroutine("CreateWave");
        }
        else canFire = true;
        
    }
    #endregion

    #region Phase2
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


    public IEnumerator SlamInComing() //lunch the slam attack
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

    public IEnumerator SlamBack() //return the totem
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
        yield return new WaitForSeconds(2);
        GameObject meteor = Instantiate(projectil, (pointOfImpact + new Vector3(0, 2.5f)), transform.rotation);
    }

    #endregion

    private void SlamPlayer()
    {
        //suit le player
        //arret et tremble
        //slam sur le sol
    }
}
