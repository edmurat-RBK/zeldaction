using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionMusique : MonoBehaviour
{
    public bool start;
    public string startMusic;

    [Header ("La musique")]
    public string musique1;

    void Start()
    {
        if (start == true)
        {
            FindObjectOfType<AudioManager>().Play(startMusic);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play(musique1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Stop(musique1);
        }
    }
}
