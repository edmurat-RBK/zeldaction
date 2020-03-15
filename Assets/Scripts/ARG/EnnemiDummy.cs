using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemis
{
    /// <summary>
    /// Made by Arthur Galland
    /// Test for ennemis dammages
    /// </summary>
    public class EnnemiDummy : MonoBehaviour
    {
        #region Variables
        public int hitPoint;
        
        #endregion
        
        void Awake()
        {
            
        }
        
        void Start()
        {
            hitPoint = 5;
        }
        
        void Update()
        {
            Die();
        }
        
        public void EnnemiTakeDammage(int dammage)
        //fonction used to apply dammages to ennemis use GetComponent<EnnemiDummy>().EnnemiTakeDammage(variable for dammage)
        {
            hitPoint -= dammage;
        }

        private void Die()
        //fonction used for destroy the ennemis when his hitpoint hit 0
        {
            if (hitPoint <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        
    }
}