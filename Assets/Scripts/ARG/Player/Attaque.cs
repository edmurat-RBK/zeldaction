using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using Ennemis;

namespace Attack
{
    /// <summary>
    /// Made by Arthur Galland
    /// Use for the attack behevior and for the dammage application to the ennemis
    /// </summary>
    public class Attaque : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private GameObject attackBox;
        private bool alreadyInList;
        private bool alreadyInCrateList;
        public List<GameObject> ennemisInRange = new List<GameObject>(); //list of all the ennemis in range
        public List<GameObject> destructibleElement = new List<GameObject>();
        public int dammage; //dammages of the player

        private Animator anim;
        //bool attack1;
        float timer;
        [SerializeField]
        private float timeBetweenAttack;
        private int attackCount;
        private float timestamp;
        private float timestampAuto;
        [SerializeField]
        private float timeReset;
        #endregion

        void Awake()
        {
            
        }
        
        void Start()
        {
            anim = GetComponent<Animator>();
        }
       
        void Update()
        {
            AtatckPos();
            ennemisInRange.RemoveAll(list_item => list_item == null); //remove

            if (Input.GetButtonDown("X") && timestamp < Time.time) //get l'input
            {
                ApplyDammage();
                CrateDestruction();
             }

            if (timestampAuto < Time.time)
            {
                attackCount = 0;
            }

         
        }

        public void Attack1Done()  //cree event dans animation pour ramener meleeattack a false apres 1 cp
        {
                anim.SetBool("IsMeleeAttacking", false);
        }

        public void Attack2Done() //event trigger lors de la deuxième attack
        {
            anim.SetBool("IsMeleeAttacking", false);
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

        #region DetectTheEnnemis
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Ennemi")
            {
                foreach (GameObject ennemi in ennemisInRange)
                {
                    if (collision.gameObject == ennemi)
                    {
                        alreadyInList = true;
                    }
                }
                if (!alreadyInList)
                {
                    ennemisInRange.Add(collision.gameObject);
                }
                alreadyInList = false;
            }

            if (collision.gameObject.tag == "Caisse Destructible")
            {
                foreach (GameObject caisseDestruc in destructibleElement)
                {
                    if (collision.gameObject == caisseDestruc)
                    {
                        alreadyInCrateList = true;
                    }
                }
                if (alreadyInCrateList == false)
                {
                    destructibleElement.Add(collision.gameObject);
                }
                alreadyInCrateList = false;
            }

            if (collision.gameObject.tag == "Boss")
            {
                foreach (GameObject ennemi in ennemisInRange)
                {
                    if (collision.gameObject == ennemi)
                    {
                        alreadyInList = true;
                    }
                }
                if (!alreadyInList)
                {
                    ennemisInRange.Add(collision.gameObject);
                }
                alreadyInList = false;
            }

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Ennemi")
            {
                ennemisInRange.Remove(collision.gameObject);
            }

            if (collision.gameObject.tag == "Caisse Destructible")
            {
                destructibleElement.Remove(collision.gameObject);
            }

            if (collision.gameObject.tag == "Boss")
            {
                ennemisInRange.Remove(collision.gameObject);
            }

        }
        #endregion

        private void ApplyDammage()
        {
            timestamp = Time.time + timeBetweenAttack;

            if (attackCount == 0 || attackCount == 2)
            {
                attackCount = 1;
                anim.SetInteger("AttackCount", 1);
                anim.SetBool("IsMeleeAttacking", true);
                ResetAuto();
            }
            else if (attackCount == 1)
            {
                attackCount = 2;
                anim.SetInteger("AttackCount", 2);
                anim.SetBool("IsMeleeAttacking", true);
                ResetAuto();
            }

            foreach (GameObject ennemi in ennemisInRange)
            {
                if (ennemi.tag == ("Boss"))
                {
                    ennemi.GetComponent<BossManager>().DammageBoss();
                }
                else ennemi.GetComponent<PvEnnemis>().EnnemiTakeDammage(dammage);

            }


        } //apply damage to the ennemis within the colldier of the attack

        private void ResetAuto()
        {
            timestampAuto = Time.time;
            timestampAuto = Time.time + timeReset;
        }

        private void CrateDestruction()
        {
            foreach (GameObject caisseDestruc in destructibleElement)
            {
                caisseDestruc.GetComponent<CaisseDestructible>().Destruction();
            }
        }
    }
}