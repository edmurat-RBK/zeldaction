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
        if (!loaded)
        {
            if (Input.GetButton("Y"))
            {
                inputHoldTime += Time.deltaTime;
                if (inputHoldTime >= inputMaxHoldTime)
                {
                    loaded = true;
                }
            }
            else
            {
                inputHoldTime = 0;
            }
        }
        else
        {
            if (Input.GetButtonUp("Y"))
            {
                kameheaumeheau = true;
                manager.playerCanMove = false;
                loaded = false;
                inputHoldTime = 0;
            }
        }
    }

    private void LaunchKameheaumeheau()
    {
        if(kameheaumeheau)
        {
            beamTime += Time.deltaTime;
            if(beamTime >= beamMaxTime)
            {
                beamTime = 0;
                kameheaumeheau = false;
                manager.playerCanMove = true;
            }

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

            Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), beamDirection, beamRange);
        }
    }
}
