﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace WateringCan
{
    public class Arrosoir : MonoBehaviour
    {
        #region Variables
        private float horizontalRot;
        private float verticalRot;
        [SerializeField]
        private GameObject water;
        private int bucketAngle;
        #endregion

        void Awake()
        {
            
        }
        
        void Start()
        {
            water.gameObject.SetActive(false);
        }
        
        void Update()
        {

            if (Input.GetButton("B"))
            {
                Watering();
            }
            else
            {
             PlayerManager.Instance.playerCanMove = true; 
             water.gameObject.SetActive(false); //set off the particule system
            }

        }

        #region BucketWatering
        private void Watering()
        {
            PlayerManager.Instance.playerCanMove = false; //stop the player from moving
            PlayerManager.Instance.playerRigidBody.velocity = Vector2.zero; //stop the player movement
            water.gameObject.SetActive(true); //set on the particule system

            switch (PlayerManager.Instance.dirPlayer)
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
            }
        }
        #endregion
    }
}