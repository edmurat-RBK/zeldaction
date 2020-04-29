using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEditor;

namespace MovementPlayer
{
    /// <summary>
    /// Made by Arthur Galland
    /// Script use by the player for moving
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        #region variable
        private float horizontal;
        private float vertical;
       
        public Vector2 direction;
        private PlayerManager manager;
        private Animator anim;
        #endregion

        private void Start()
        {
           anim = GetComponent<Animator>();
           manager = GetComponent<PlayerManager>();
            
        }

        void Update()
        {
            horizontal = PlayerManager.Instance.horizontal;
            vertical = PlayerManager.Instance.vertical;
           
            PlayerMove();
        }

        #region movementScript
        /// <summary>
        /// player movement within 8 directions
        /// </summary>
        private void PlayerMove()
        { 
            if (PlayerManager.Instance.playerCanMove == true)
            {


                if (vertical >= 0.01)
                {
                    vertical = 1;
                    //space for animator
                }
                else if (vertical <= -0.01)
                {
                    vertical = -1;
                    //space for animator
                }
                else
                {
                    vertical = 0;
                    //space for animator
                }

                if (horizontal >= 0.01)
                {
                    horizontal = 1;
                    //space for animator
                }
                else if (horizontal <= -0.01)
                {
                    horizontal = -1;
                    //space for animator
                }
                else
                {
                    horizontal = 0;
                    //space for animator
                }

                //youmna was helped

            if (vertical==0 && horizontal == 0) 
                {
                    anim.SetBool("IsWalking", false);
                 
                }
                else 
                {
                    anim.SetBool("IsWalking", true);
                anim.SetFloat("Horizontal", horizontal);
                anim.SetFloat("Vertical", vertical);

                }
                direction = new Vector2(horizontal, vertical).normalized;
                PlayerManager.Instance.playerRigidBody.velocity = direction * manager.speed * Time.deltaTime;
            }
            
        }
        #endregion
    }

}
