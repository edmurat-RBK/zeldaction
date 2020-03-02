using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace movementPlayer
{
    public class PlayerMovement : MonoBehaviour
    {
        #region variable
        public float horizontal;
        public float vertical;
        private Vector2 direction;
        public Rigidbody2D playerRB;

        [SerializeField]
        [Range(100f, 1000f)]
        private float speed;
        #endregion

        private void Start()
        {
            playerRB = GetComponent<Rigidbody2D>();

        }

        void Update()
        {
            //
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            PlayerMove();
        }

        #region movementScript
        /// <summary>
        /// player movement within 8 directions
        /// </summary>
        private void PlayerMove()
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
            playerRB.velocity = direction * speed * Time.deltaTime;
        }
        #endregion
    }

}
