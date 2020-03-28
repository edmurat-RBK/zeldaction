using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Manager;

/// <summary>
/// Made by Arthur Galland
/// Script used for activate on a TriggerEnter the boat cinematique
/// </summary>
public class BoatCinematique : MonoBehaviour
{
    #region Variable
    public PlayableDirector boatcin;//reference to the playable cinematique
    [SerializeField]
    [Range(1f, 100f)]
    private float cinematiqueTimer;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayCinematique(); //start the cinematique
            StartCoroutine("MoveAgain"); //player can't move during this cinematique
            Destroy(this.gameObject); //destroy the collider so the cinematique can't be played again
        }
    }

    public void PlayCinematique()
    {
        boatcin.Play();
        PlayerManager.Instance.playerCanMove = false;
        PlayerManager.Instance.playerRigidBody.velocity = Vector2.zero;
    }

    IEnumerator MoveAgain()
    {
        yield return new WaitForSeconds(cinematiqueTimer);
        PlayerManager.Instance.playerCanMove = true;
    }
}
