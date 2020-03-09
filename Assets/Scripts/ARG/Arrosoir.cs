using System.Collections;
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
        private ParticleSystem water;


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
            horizontalRot = PlayerManager.Instance.horizontal;
            verticalRot = PlayerManager.Instance.vertical;
            //retrieve the position of the joystick

            if (Input.GetButton("B"))
            {
                WateringPos();
            }
            else
            {
             PlayerManager.Instance.playerCanMove = true; 
             water.gameObject.SetActive(false); //set off the particule system
            }

        }

        #region Watering
        private void WateringPos()
        {
            Debug.Log("je me désactive");
            PlayerManager.Instance.playerCanMove = false; //stop the player from moving
            PlayerManager.Instance.playerRigidBody.velocity = Vector2.zero; //stop the player movement
            water.gameObject.SetActive (true); //set on the particule system

            if (horizontalRot != 0 || verticalRot != 0)
            {
                if (verticalRot >= 0.01)
                {
                    verticalRot = 1;
                    //space for animator
                }
                else if (verticalRot <= -0.01)
                {
                    verticalRot = -1;
                    //space for animator
                }
                else
                {
                    verticalRot = 0;
                    //space for animator
                }

                if (horizontalRot >= 0.01)
                {
                    horizontalRot = 1;
                    //space for animator
                }
                else if (horizontalRot <= -0.01)
                {
                    horizontalRot = -1;
                    //space for animator
                }
                else
                {
                    horizontalRot = 0;
                    //space for animator
                }

                water.transform.rotation = Quaternion.Euler(new Vector3(-verticalRot, horizontalRot,0).normalized*90);
                //vertical and horizontal is swap, i don't know why but it work
            }
        }

        #endregion
    }
}