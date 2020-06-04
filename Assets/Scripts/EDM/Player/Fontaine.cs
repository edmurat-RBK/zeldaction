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

    private bool isActif;
    private bool isCooldown;

    private bool lockSon;

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
    void FixedUpdate()
    {
        PlayerManager.Instance.cooldownF = cooldown;

        if (cooldown != 0 && isActif == false)
        {
            isCooldown = true;
            cooldown -= Time.fixedDeltaTime;
            if (cooldown < 0)
            {
                isCooldown = false;
                cooldown = 0;
                anim.SetBool("IsShielding", false);
            }
        }

        if (PlayerManager.Instance.lockcUseBucket == false)
        {
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
                    }
                }


                else if (isCooldown == false)
                {
                    if (lockSon == false)
                    {
                        lockSon = true;
                        FindObjectOfType<AudioManager>().Play("Fontaine");
                    }

                    isActif = true;
                    cooldown += Time.fixedDeltaTime;
                    effectTime -= Time.fixedDeltaTime;
                    if (effectTime < 0)
                    {
                        FindObjectOfType<AudioManager>().Stop("Fontaine");
                        cooldown = maxCooldown;
                        effectTime = 0;
                        active = false;
                        manager.playerInvulnerable = false;
                        anim.SetBool("IsShielding", false);
                        fontaineShootPoint.SetActive(false);
                        PlayerManager.Instance.isFontaine = false;
                        PlayerManager.Instance.playerCanMove = true;
                        isActif = false;
                        lockSon = false;
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

}
