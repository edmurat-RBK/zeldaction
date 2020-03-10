using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Attack
{
    public class Attaque : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private GameObject attackBox;
        #endregion
        
        void Awake()
        {
            
        }
        
        void Start()
        {
            
        }
        
        void Update()
        {
            AtatckPos();
        }

        private void AtatckPos()
        {

            switch (PlayerManager.Instance.dirPlayer)
            {
                case PlayerManager.direction.down:
                    attackBox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;

                case PlayerManager.direction.downRight:
                    attackBox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                    break;

                case PlayerManager.direction.right:
                    attackBox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    break;

                case PlayerManager.direction.upRight:
                    attackBox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 135));
                    break;

                case PlayerManager.direction.up:
                    attackBox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    break;

                case PlayerManager.direction.upLeft:
                    attackBox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 225));
                    break;

                case PlayerManager.direction.left:
                    attackBox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
                    break;

                case PlayerManager.direction.downLeft:
                    attackBox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 315));
                    break;
            }

        }
}
}