using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class Pattern1 : MonoBehaviour
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
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.Instance.transform;
        canFire = true;
        //StartCoroutine("CreateWave");


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            WaveOfFlame();
        }

        

    }

    #region Phase
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

  
}
