using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Game
{
    public class DirectionBehevior : MonoBehaviour
    {
        #region Variables
        public enum direction { down, downLeft, left, upLeft, up, upRight, right, downRight,}
        public direction dirPlayer = direction.down;
        
        #endregion
        
        void Awake()
        {
            
        }
        
        void Start()
        {
            
        }
        
        void Update()
        {
            PlayerDirection();
            Debug.Log(dirPlayer);
        }

        #region direction
        void PlayerDirection()
        {
            float horizontalDelta;
            float verticalDelta;

           // horizontalDelta = Input.GetAxisRaw("Horizontal");
           // verticalDelta = Input.GetAxisRaw("Vertical");

            horizontalDelta = PlayerManager.Instance.horizontal;
            verticalDelta = PlayerManager.Instance.vertical;

            float angleShoot;


            //input is used
            if (verticalDelta > 0.01 || horizontalDelta > 0.01 || verticalDelta < -0.01 || horizontalDelta < -0.01)
            {
                
                //find the angle with cos/sin
                angleShoot = Mathf.Atan2(horizontalDelta, verticalDelta);

                if (angleShoot > (Mathf.PI / 3) && angleShoot < ((2 * Mathf.PI) / 3))
                {//RIGHT

                    dirPlayer = direction.right;
                }

                else if (angleShoot > -((2 * Mathf.PI) / 3) && angleShoot < -(Mathf.PI / 3))
                {//LEFT

                    dirPlayer = direction.left;
                }

                else if (angleShoot > -(Mathf.PI / 6) && angleShoot < (Mathf.PI / 6))
                {//TOP

                    dirPlayer = direction.up;
                }

                else if (angleShoot > ((2 * Mathf.PI) / 3) && angleShoot < ((5 * Mathf.PI) / 6))
                {//DOWN RIGHT

                    dirPlayer = direction.downRight;
                }

                else if (angleShoot > (Mathf.PI / 6) && angleShoot < (Mathf.PI / 3))
                {//TOP RIGHT

                    dirPlayer = direction.upRight;
                }

                else if (angleShoot > -((5 * Mathf.PI) / 6) && angleShoot < -((2 * Mathf.PI) / 3))
                {//DOWN LEFT

                    dirPlayer = direction.downLeft;
                }

                else if (angleShoot > -(Mathf.PI / 3) && angleShoot < -(Mathf.PI / 6))
                {//TOP LEFT

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
