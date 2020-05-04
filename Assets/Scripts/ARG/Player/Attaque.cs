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
        bool attack1;
        float timer;
        public float timeBetweenAttack;       
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

            if (Input.GetButtonDown("X")) //get l'input
            {
                if (!anim.GetBool("IsMeleeAttacking")) //si la booleen d'attaque est false
                {
                    anim.SetBool("IsMeleeAttacking", true); //alors elle devient true

                    if (!attack1) //si attack est faux
                    {
                        attack1 = true; //alors il devient true
                    }
                    else //sinon
                    {
                        anim.SetBool("DoubleAttack", true); //la booleen double attack devient true
                        attack1 = false; //attack  devient faux
                        timer = 0; //timer reset
                    }
                }
                ApplyDammage();
                CrateDestruction();
             }


            //youmna a eu de l'aide de sam

            if (attack1) //attack est true
            {
                timer += Time.deltaTime;//le timer s'incrémente

                if (timer >= timeBetweenAttack) //si le timer est supérieur ou égale au cd d'attaque
                {
                    attack1 = false;//alors attack est false
                    timer = 0; //le timer est reset
                }
            }

         
        }

        public void Attack1Done()  //cree event dans animation pour ramener meleeattack a false apres 1 cp
        {
            if (!anim.GetBool("DoubleAttack"))
            {
                anim.SetBool("IsMeleeAttacking", false);

            }
        }

        public void Attack2Done() //event trigger lors de la deuxième attack
        {
            anim.SetBool("DoubleAttack", false);
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
        }
        #endregion

        private void ApplyDammage()
        {
            foreach (GameObject ennemi in ennemisInRange)
            {
                ennemi.GetComponent<PvEnnemis>().EnnemiTakeDammage(dammage);
            }
        } //apply damage to the ennemis within the colldier of the attack

        private void CrateDestruction()
        {
            foreach (GameObject caisseDestruc in destructibleElement)
            {
                caisseDestruc.GetComponent<CaisseDestructible>().Destruction();
            }
        }
    }
}