using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace movementPlayer
{
    public class PlayerMovement : MonoBehaviour
    {
        #region variable
        private float horizontal;
        private float vertical;
        private Vector2 direction;

        [SerializeField]
        [Range(100f, 1000f)]
        private float speed;
        #endregion

        private void Start()
        {
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

                direction = new Vector2(horizontal, vertical).normalized;
                PlayerManager.Instance.playerRigidBody.velocity = direction * speed * Time.deltaTime;
            }

        }
        #endregion
    }

}
