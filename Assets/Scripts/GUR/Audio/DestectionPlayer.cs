using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestectionPlayer : MonoBehaviour
{
    public AudioSource source;
    public AudioClip fireDeath;

    public bool isFire;

    public DestructibleByWater scrpit;



    private void Update()
    {
        if (isFire == true)
        {
            if (scrpit.canDeathSong == true)
            {
                isFire = false;
                source.Stop();
                source.volume = 1f;
                source.clip = fireDeath;
                source.Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            source.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            source.Stop();
        }
    }
}
