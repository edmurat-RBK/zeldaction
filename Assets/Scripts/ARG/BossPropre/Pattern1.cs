using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class Pattern1 : MonoBehaviour
{
    #region Variable
    //waves of fire
    public GameObject shotPoint;
    [Header("Onde de choc")]
    public GameObject chocWave;
    private Transform player;
    private int numberOfWaves;
    private int numberOfTheWave;
    public bool canFire;
    public List<GameObject> allEnnemis;
    [SerializeField]
    private float timeBeforeLunchPattern = 0;
    private bool ennemiDead = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.Instance.transform;
        canFire = true;

    }

    private void Update()
    {
        allEnnemis.RemoveAll(list_item => list_item == null);
       
        if (allEnnemis.Count == 0 && ennemiDead == false)
        {
            BossManagerP.instance.ActivateClepsydre();
            ennemiDead = true;
        }
    }

    #region Phase
    public void WaveOfFlame()
    {
        if (canFire == true)
        {
            numberOfWaves = Random.Range(2, 5);
            Debug.Log(numberOfWaves);
            numberOfTheWave = 0;
            StartCoroutine(CreateWave());
        }

    }
    private IEnumerator CreateWave() //fire multiple waves of fire
    {
        
        canFire = false;
        var dir = new Vector2(player.transform.position.x - shotPoint.transform.position.x, player.transform.position.y - shotPoint.transform.position.y); // Permet d'orienter le shotPoint vers le joueur
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        shotPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //aniamtion de tire
        GameObject wave = Instantiate(chocWave, shotPoint.transform.position, shotPoint.transform.rotation); // spawn l'onde de choc
        Destroy(wave, 1f);
        yield return new WaitForSeconds(2);
        numberOfTheWave += 1;
        if (numberOfTheWave != numberOfWaves)
        {
            StartCoroutine(CreateWave());
        }
        else
        {
            canFire = true;
            int time = Random.Range(5, 9);
            yield return new WaitForSeconds(time);
            WaveOfFlame(); //repeat itself
        }

    }
    #endregion

    public IEnumerator InitialisePattern1()
    {
        foreach (GameObject ennemi in allEnnemis)
        {
            ennemi.SetActive(true);
        }
        yield return new WaitForSeconds(timeBeforeLunchPattern);
        WaveOfFlame();
    }
}
