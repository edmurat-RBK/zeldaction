﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Manager
{
    /// <summary>
    /// Made by Arthur Galland
    /// Script use for manage all the importante variables for the player
    /// It's a Singleton, we can acces all thoses variables with "PlayerManager.Instance.Variable"
    /// </summary>
    public class PlayerManager : Singleton<PlayerManager>
    {
        #region Variables
        public float horizontal;
        public float vertical;
        public bool playerCanMove;
        public Rigidbody2D playerRigidBody;
        public enum direction { down, downLeft, left, upLeft, up, upRight, right, downRight, } //enulm for the player direction
        public direction dirPlayer = direction.down;
        #endregion

        void Awake()
        {
            MakeSingleton(true);
        }
        
        void Start()
        {
            playerCanMove = true;
            playerRigidBody = GetComponent<Rigidbody2D>();
        }
        
        void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            PlayerDirection();
        }


        #region direction
        /// <summary>
        /// find the direction of the player with the input of the joystick
        /// we use the dirPlayer variable for retrieve the direction in others scripts
        /// </summary>
        void PlayerDirection()
        {
            float angleDir;

            //if input is used
            if (vertical > 0.2 || horizontal > 0.2 || vertical < -0.2 || horizontal < -0.2)
            {

                //find the angle with cos/sin
                angleDir = Mathf.Atan2(horizontal, vertical);

                if (angleDir > (Mathf.PI / 3) && angleDir < ((2 * Mathf.PI) / 3))
                {//RIGHT

                    dirPlayer = direction.right;
                }

                else if (angleDir > -((2 * Mathf.PI) / 3) && angleDir < -(Mathf.PI / 3))
                {//LEFT

                    dirPlayer = direction.left;
                }

                else if (angleDir > -(Mathf.PI / 6) && angleDir < (Mathf.PI / 6))
                {//UP

                    dirPlayer = direction.up;
                }

                else if (angleDir > ((2 * Mathf.PI) / 3) && angleDir < ((5 * Mathf.PI) / 6))
                {//DOWN RIGHT

                    dirPlayer = direction.downRight;
                }

                else if (angleDir > (Mathf.PI / 6) && angleDir < (Mathf.PI / 3))
                {//UP RIGHT

                    dirPlayer = direction.upRight;
                }

                else if (angleDir > -((5 * Mathf.PI) / 6) && angleDir < -((2 * Mathf.PI) / 3))
                {//DOWN LEFT

                    dirPlayer = direction.downLeft;
                }

                else if (angleDir > -(Mathf.PI / 3) && angleDir < -(Mathf.PI / 6))
                {//UP LEFT

                    dirPlayer = direction.upLeft;
                }

                else
                {//DOWN

                    dirPlayer = direction.down;
                }

            }
            #endregion

        }
    }
}