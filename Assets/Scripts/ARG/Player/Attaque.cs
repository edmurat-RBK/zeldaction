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
        public List<GameObject> ennemisInRange = new List<GameObject>(); //list of all the ennemis in range
        public int dammage; //dammages of the player

        private Animator anim;
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

            if (Input.GetButtonDown("X"))
            {
                ApplyDammage();
                anim.SetBool("IsMeleeAttacking", true);

            }
            else
            {
                anim.SetBool("IsMeleeAttacking", false);
            }

            //Essayer de mettre une ligne de code qui dit qu'un double click de x ramene a une deuxieme anim
            


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
            if (collision.gameObject.tag == "Ennemis")
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
            if (collision.gameObject.tag == "Ennemis")
            {
                ennemisInRange.Remove(collision.gameObject);
            }
        }
        #endregion

        private void ApplyDammage()
        {
            foreach (GameObject ennemi in ennemisInRange)
            {
             ennemi.GetComponent<EnnemiDummy>().EnnemiTakeDammage(dammage);
            }
        } //apply damage to the ennemis within the colldier of the attack
    }
}