using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kameheaumeheau : MonoBehaviour
{
    public float beamRange;
    public float inputMaxHoldTime;
    public float inputHoldTime;
    public float beamMaxTime;
    public float beamTime;
    public bool loaded = false;
    public bool kameheaumeheau = false;

    [HideInInspector]
    public Vector2 beamDir;

    public PlayerManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLoading();
        LaunchKameheaumeheau();
    }

    private void CheckLoading()
    {
        //If input hasnt been pressed enough time
        if (!loaded)
        {
            //If inupt is still pressed
            if (Input.GetButton("Y"))
            {
                inputHoldTime += Time.deltaTime;
                //When input has been pressed enough
                if (inputHoldTime >= inputMaxHoldTime)
                {
                    loaded = true;
                }
            }
            //If input is not pressed / realsed
            else
            {
                inputHoldTime = 0;
            }
        }
        // If Kameheaumeheau is loaded
        else
        {
            //When input released
            if (Input.GetButtonUp("Y"))
            {
                //Start Kameheau
                kameheaumeheau = true;
                manager.playerCanMove = false;
                loaded = false;
                inputHoldTime = 0;
            }
        }
    }

    private void LaunchKameheaumeheau()
    {
        //If kameheau launched
        if(kameheaumeheau)
        {
            //Check time
            beamTime += Time.deltaTime;
            manager.playerRigidBody.velocity = Vector2.zero;
            if(beamTime >= beamMaxTime)
            {
                beamTime = 0;
                kameheaumeheau = false;
                manager.playerCanMove = true;
                
            }

            //Check direction
            Vector2 beamDirection = Vector2.zero;
            switch(manager.dirPlayer)
            {
                case PlayerManager.direction.up:
                    beamDirection = Vector2.up;
                    break;

                case PlayerManager.direction.upRight:
                    beamDirection = Vector2.up + Vector2.right;
                    break;

                case PlayerManager.direction.right:
                    beamDirection = Vector2.right;
                    break;

                case PlayerManager.direction.downRight:
                    beamDirection = Vector2.down + Vector2.right;
                    break;

                case PlayerManager.direction.down:
                    beamDirection = Vector2.down;
                    break;

                case PlayerManager.direction.downLeft:
                    beamDirection = Vector2.down + Vector2.left;
                    break;

                case PlayerManager.direction.left:
                    beamDirection = Vector2.left;
                    break;

                case PlayerManager.direction.upLeft:
                    beamDirection = Vector2.up + Vector2.left;
                    break;
            }

            //Check collision
            LayerMask mask = LayerMask.GetMask("KamehoHit");
            RaycastHit2D ray = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), beamDirection, beamRange, mask);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), beamDirection, Color.blue);
            
            if (ray.rigidbody != null)
            {
                beamDir = beamDirection;
                if (ray.transform.gameObject.tag == "Moulin")
                {
                    ray.transform.gameObject.GetComponent<MoulinHitDetection>().getHit = true;
                }

                if (ray.transform.gameObject.tag == "CaissePierre")
                {
                    ray.transform.gameObject.GetComponent<CaisseEnPierre>().move = true;
                }
                if (ray.transform.gameObject.tag == "Clepsydre")
                {
                    ray.transform.gameObject.GetComponent<Clepsydre>().clepsydreHit = true;
                }


                if (ray.transform.gameObject.tag == "Ennemi")
                {
                    ray.transform.gameObject.GetComponent<PvEnnemis>().kameoHit = true;
                }

                if (ray.transform.gameObject.tag == "Destructible")
                {
                    ray.transform.gameObject.GetComponent<DestructibleByWater>().khameoDetection = true;
                }
            }
        }
    }
}

