using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fontaine : MonoBehaviour
{
    public float cooldown = 0;
    public float maxCooldown = 3;
    public float effectTime = 0;
    public float maxEffectTime = 0.5f;
    public bool active = false;
    //public float vagueTrigger = 0.2f;

    [SerializeField]
    private GameObject fontaineShootPoint;

    private Animator anim;

    private PlayerManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<PlayerManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!active && cooldown == 0)
        {

            if(Input.GetButton("A"))
            {
                anim.SetBool("IsShielding",true);
                fontaineShootPoint.SetActive (true);
                effectTime = maxEffectTime;
                active = true;
                manager.playerInvulnerable = true;                
                manager.playerCanMove = false;
                manager.playerRigidBody.velocity = Vector2.zero;

                Debug.Log("Fontaine !");

            }
        }


        else if(!active && cooldown != 0 && !Input.GetButton("A"))
        {
            
            cooldown -= Time.deltaTime;
            if(cooldown < 0)
            {
                cooldown = 0;
                anim.SetBool("IsShielding", false);
                Debug.Log("Pas Fontaine !");
                fontaineShootPoint.SetActive (false);
             }
        }
        else
        {
            //Vague();

            effectTime -= Time.deltaTime; 
            if(effectTime < 0)
            {
                cooldown = maxCooldown;
                effectTime = 0;
                active = false;
                manager.playerInvulnerable = false;
                anim.SetBool("IsShielding", false);
            }
        }

        if (Input.GetButtonUp("A"))
        {
            anim.SetBool("IsShielding", false);
            PlayerManager.Instance.playerCanMove = true;           
        }        
    }

    //private void Vague()
    //{
    //    if(0 < effectTime && effectTime < vagueTrigger)
    //    {
    //        if(Input.GetButtonDown("A"))
    //        {
    //            manager.playerCanMove = false;
    //            manager.playerRigidBody.velocity = Vector2.zero;
    //            Debug.Log("Vague !");
    //        }
    //    }
    //}
}
