using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Attack;
using WateringCan;
using UnityEngine.SceneManagement;

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

        public int healthPlayer;
        public int healthMax;

        public float horizontal;
        public float vertical;
        public bool playerCanMove;
        public bool playerInvulnerable = false;
        public bool getBucket;
        public Rigidbody2D playerRigidBody;
        public Transform playerTransform;
        public enum direction { down, downLeft, left, upLeft, up, upRight, right, downRight, } //enulm for the player direction
        public direction dirPlayer = direction.down;
        [Range(100f, 1000f)]
        public float speed;
        public bool playerCanRotate;
        public bool canTakeDammage = true;
        public bool deathParalise;
        private Animator anim;
        //booleen for the different actions of the player


        // Bool pour détecter les différentes ations du joueur
        public bool lockcUseBucket;

        public bool isAttacking;
        public bool isArroisoir;
        public bool isKhameau;
        public bool isFontaine;

        //4 scripts to disable when the player don't have the bucket
        private Attaque attack;
        private Arrosoir water;
        private Fontaine fontaine;
        private Kameheaumeheau kameo;
        public bool canChangeSprite; //use when obtain the bucket

        public float cooldownF;
        public float maxCooldownF;


        // Gestion du son
        public bool onSand;
        public bool onDirt;
        public bool onConcrete;
        private float angleDir;

        #endregion

        void Awake()
        {
            playerTransform = GetComponent<Transform>();
            MakeSingleton(true);
            water = GetComponent<Arrosoir>();
            attack = GetComponent<Attaque>(); 
            kameo = GetComponent<Kameheaumeheau>(); 
            fontaine = GetComponent<Fontaine>();
            canChangeSprite = false;

        }
        
        void Start()
        {
            anim = GetComponent<Animator>();
            if (UpgradesManager.List["as bucket"] == true)
            {
                getBucket = true;
                obtainBucket();
                anim.SetBool("HasBucket", true);
            }
            else
            {
                getBucket = false;
                obtainBucket();
                anim.SetBool("HasBucket", false);
            }
            playerCanMove = true;
            playerRigidBody = GetComponent<Rigidbody2D>();
            //getBucket = false;
           

        }
        
        void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            PlayerDirection();

                //if (Input.GetKeyDown(KeyCode.N))
                //{
                //    getBucket = false;
                //    obtainBucket();
                //    anim.SetBool("HasBucket", false);

                //}

                if (Input.GetKeyDown(KeyCode.B))
                {
                    getBucket = true;
                    obtainBucket();
                    GetComponent<UpgradeObject>().Gotcha();
                    anim.SetBool("HasBucket", true);
                }

        }

        //function to lucnh when you want to desactivate or activate all the abilities of the player with the boolen
        public void obtainBucket()
        {            
            //anim.SetBool("HasBucket", false);
            if (getBucket == true)
            {
                anim.SetBool("HasBucket", true);
                water.enabled = true;
                attack.enabled = true;
                kameo.enabled = true;
                fontaine.enabled = true;
                
                
            }
            else if (getBucket == false)
            {
                water.enabled = false;
                attack.enabled = false;
                kameo.enabled = false;
                fontaine.enabled = false;
                if (canChangeSprite == true)
                {
                    //change the animator without bucket
                    canChangeSprite = false;
                }
            }
        }

        public void deathParalisy()
        {
            if (deathParalise == false)
            {
                water.enabled = true;
                attack.enabled = true;
                kameo.enabled = true;
                fontaine.enabled = true;

            }
            else
            {
                water.enabled = false;
                attack.enabled = false;
                kameo.enabled = false;
                fontaine.enabled = false;
            }
        }



        #region direction
        /// <summary>
        /// find the direction of the player with the input of the joystick
        /// we use the dirPlayer variable for retrieve the direction in others scripts
        /// </summary>
        void PlayerDirection()
        {
            //float angleDir;
            if (playerCanRotate)
            {
                //if input is used
                if (vertical > 0.01 || horizontal > 0.01 || vertical < -0.01 || horizontal < -0.01)
                {

                    angleDir = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;

                    if (angleDir < 0)
                    {
                        angleDir += 360;
                    }

                    if (angleDir > 22.5f && angleDir <67.5f)
                    {//UP RIGHT

                        dirPlayer = direction.upRight;
                    }

                    else if (angleDir > 67.5f && angleDir < 112.5f)
                    {//RIGHT

                        dirPlayer = direction.up;
                    }

                    else if (angleDir > 112.5f && angleDir < 157.5f)
                    {//DOWN RIGHT

                        dirPlayer = direction.upLeft;
                    }

                    else if (angleDir > 157.5f && angleDir < 202.5f)
                    {//DONW

                        dirPlayer = direction.left;
                    }

                    else if (angleDir > 202.5f && angleDir < 247.5f)
                    {//DOWN LEFT

                        dirPlayer = direction.downLeft;
                    }

                    else if (angleDir > 247.5F && angleDir < 295.5F)
                    {//LEFT

                        dirPlayer = direction.down;
                    }

                    else if (angleDir > 292.5f && angleDir < 337.5f)
                    {//UP LEFT

                        dirPlayer = direction.downRight;
                    }

                    else
                    {//UP

                        dirPlayer = direction.right;
                    }
                }
           
            }
            #endregion

        }
    }
}