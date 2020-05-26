﻿using Manager;
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

        if (cooldown != 0 && isActif == false)
        {
            isCooldown = true;
            cooldown -= Time.deltaTime;
            if (cooldown < 0)
            {
                isCooldown = false;
                cooldown = 0;
                anim.SetBool("IsShielding", false);
            }
        }

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
                isActif = true;
                cooldown += Time.fixedDeltaTime;
                effectTime -= Time.fixedDeltaTime;
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
                    isActif = false;
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
