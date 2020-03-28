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
    public float vagueTrigger = 0.2f;

    private PlayerManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!active && cooldown == 0)
        {
            if(Input.GetButton("X"))
            {
                effectTime = maxEffectTime;
                active = true;
                manager.playerInvulnerable = true;
            }
        }
        else if(!active && cooldown != 0)
        {
            cooldown -= Time.deltaTime;
            if(cooldown < 0)
            {
                cooldown = 0;
            }
        }
        else
        {
            Vague();

            effectTime -= Time.deltaTime;
            if(effectTime < 0)
            {
                cooldown = maxCooldown;
                effectTime = 0;
                active = false;
                manager.playerInvulnerable = false;
            }
        }
    }

    private void Vague()
    {
        if(0 < effectTime && effectTime < vagueTrigger)
        {
            if(Input.GetButtonDown("X"))
            {
                Debug.Log("Vague !");
            }
        }
    }
}
