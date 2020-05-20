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
    private bool lockCooldown;
    //public float vagueTrigger = 0.2f;

    [SerializeField]
    private GameObject fontaineShootPoint;

    private Animator anim;

    private PlayerManager manager;

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.maxCooldownF = maxCooldown;
        PlayerManager.Instance.cooldownF = cooldown;
        lockCooldown = false;
        active = false;
        manager = GetComponent<PlayerManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerManager.Instance.cooldownF = cooldown;

        if (PlayerManager.Instance.isAttacking == false && PlayerManager.Instance.isArroisoir == false && PlayerManager.Instance.isKhameau == false)
        {
            if (!active && cooldown == 0)
            {
                if (Input.GetButton("A"))
                {
                    PlayerManager.Instance.isFontaine = true;
                    anim.SetBool("IsShielding", true);
                    fontaineShootPoint.SetActive(true);
                    effectTime = maxEffectTime;
                    active = true;
                    manager.playerInvulnerable = true;
                    manager.playerCanMove = false;
                    manager.playerRigidBody.velocity = Vector2.zero;

                    Debug.Log("Fontaine !");

                }
            }

            else if (!active && cooldown != 0 && !Input.GetButton("A"))
            {

                cooldown -= Time.deltaTime;
                if (cooldown < 0)
                {
                    cooldown = 0;
                    anim.SetBool("IsShielding", false);
                    Debug.Log("Pas Fontaine !");
                }
            }
            else if (cooldown == 0)
            {
                //Vague();
                effectTime -= Time.deltaTime;
                if (effectTime < 0)
                {
                    cooldown = maxCooldown;
                    effectTime = 0;
                    active = false;
                    manager.playerInvulnerable = false;
                    anim.SetBool("IsShielding", false);
                    fontaineShootPoint.SetActive(false);
                    PlayerManager.Instance.isFontaine = false;
                    PlayerManager.Instance.playerCanMove = true;
                }
            }

            if (Input.GetButtonUp("A"))
            {

                if (effectTime == 0)
                {
                    PlayerManager.Instance.isFontaine = false;
                }
            }
        }
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
