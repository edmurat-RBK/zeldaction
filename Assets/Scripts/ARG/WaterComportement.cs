using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Made by Arthur Galland
    /// Use by the particule system for retrieve the collition object and act on it
    /// </summary>
    public class WaterComportement : MonoBehaviour
    {
        #region Variables
        
        
        
        #endregion
        
        void Awake()
        {
            
        }
        
        void Start()
        {
            
        }
        
        void Update()
        {
            
        }

        private void OnParticleCollision(GameObject other)
        {

            if (other.tag == "Fire")
            {
                Destroy(other.gameObject);
            }
        }

    }
}