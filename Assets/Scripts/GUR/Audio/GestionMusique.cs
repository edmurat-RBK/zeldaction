using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionMusique : MonoBehaviour
{
    public bool start;
    public string startMusic;

    [Header ("Les musiques à swtich")]
    public string musique1;

    public string musique2;

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
            FindObjectOfType<AudioManager>().Stop(musique1);
            FindObjectOfType<AudioManager>().Play(musique2);
        }
    }
}
