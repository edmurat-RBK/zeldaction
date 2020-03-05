using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Game
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        #region Variables

        public float horizontal;
        public float vertical;
        public bool playerCanMove;
        public Rigidbody2D playerRigidBody;

        #endregion

        void Awake()
        {
            MakeSingleton(true);
        }
        
        void Start()
        {
            playerCanMove = true;
            playerRigidBody = GetComponent<Rigidbody2D>();
        }
        
        void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        
        
        
    }
}