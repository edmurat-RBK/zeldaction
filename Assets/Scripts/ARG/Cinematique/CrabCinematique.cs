using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Manager;

/// <summary>
/// Made by Arthur Galland
/// Script used for activate on a TriggerEnter the crabe cinematique
/// </summary>
public class CrabCinematique : MonoBehaviour
{
    #region Variable
    public PlayableDirector crabcin; //reference to the playable cinematique
    [SerializeField]
    [Range(0f, 50f)]
    private float cinematicTimer;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayCinematique(); //start the cinematique
            StartCoroutine("MoveAgain"); //player can't move during this cinematique
        }
    }

    public void PlayCinematique()
    {
        crabcin.Play();
        PlayerManager.Instance.playerCanMove = false;
        PlayerManager.Instance.playerRigidBody.velocity = Vector2.zero;
    }

    IEnumerator MoveAgain()
    {
        yield return new WaitForSeconds(cinematicTimer);
        PlayerManager.Instance.playerCanMove = true;
        Destroy(this.gameObject); //destroy the collider so the cinematique can't be played again
    }
}
