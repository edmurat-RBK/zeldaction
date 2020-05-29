using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAnimator : MonoBehaviour
{
    // Start is called before the first frame update

    
    public Animator BossCinematique;
    public Animator BossManager;
    public GameObject boss;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Boss rire");
        FindObjectOfType<AudioManager>().Play("Boss musique");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopAnimator()
    {
        //FindObjectOfType<AudioManager>().Play("Boss rire");
        BossCinematique.enabled=false;
        BossManager.enabled=true;
        boss.GetComponent<BossManagerP>().LunchPhase1();
    }
}
