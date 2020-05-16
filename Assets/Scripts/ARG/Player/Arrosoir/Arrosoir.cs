﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace WateringCan
{
    /// <summary>
    /// Made by Arthur Galland
    /// Script used by the bucket for the watering can
    /// </summary>
    public class Arrosoir : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private GameObject water; //reference to the particule system attach to a object
        public bool useWaterCan = false;

        private Animator anim;
        #endregion

        void Awake()
        {
            
        }
        
        void Start()
        {
            water.gameObject.SetActive(false);
            anim = GetComponent<Animator>();
        }
        
        void Update()
        {

            if (Input.GetButton("B"))
            {
                PlayerManager.Instance.isArroisoir = true;

                Watering();
                anim.SetBool("IsWatering", true);

            }
            else
            {
                water.gameObject.SetActive(false); //set off the particule system
                anim.SetBool("IsWatering", false);
            }

            if (Input.GetButtonUp("B"))
            {
                PlayerManager.Instance.isArroisoir = false;
                PlayerManager.Instance.playerCanMove = true;
            }

        }

        #region BucketWatering
        private void Watering()
        {
            PlayerManager.Instance.playerCanMove = true; // the player can move_YS
            /*PlayerManager.Instance.playerRigidBody.velocity = Vector2.zero; //stop the player movement*/ //il doit pouvoir bouger normalement_YS
            water.gameObject.SetActive(true); //set on the particule system

            switch (PlayerManager.Instance.dirPlayer) //use the direction in the player manager to retrieve the angle for the particule systeme
            {
                case PlayerManager.direction.down:
                    water.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;

                case PlayerManager.direction.downRight:
                    water.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                    break;

                case PlayerManager.direction.right:
                    water.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    break;

                case PlayerManager.direction.upRight:
                    water.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 135));
                    break;

                case PlayerManager.direction.up:
                    water.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    break;

                case PlayerManager.direction.upLeft:
                    water.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 225));
                    break;

                case PlayerManager.direction.left:
                    water.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
                    break;

                case PlayerManager.direction.downLeft:
                    water.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 315));
                    break;

                default:
                water.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            }
        }
        #endregion

    }
}