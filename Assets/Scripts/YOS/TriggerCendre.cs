using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCendre : MonoBehaviour
{
    // Start is called before the first frame update
    public  ParticleSystem cendre;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D colid)
    {
        if (colid.gameObject.tag == "Player")
        {
            cendre.Play();
        }
    }
}
    
