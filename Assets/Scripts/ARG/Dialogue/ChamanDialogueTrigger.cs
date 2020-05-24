using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class ChamanDialogueTrigger : MonoBehaviour
{
    public GameObject chamanCine;
    public float timeOfCine;
    public Collider2D ChamanCollider;
    public GameObject chamanSave;

    private void Start()
    {
        if (UpgradesManager.List["chaman"] == true)
        {
            chamanCine.SetActive(false);
            chamanSave.SetActive(true);
        }
        else
        {
            ChamanCollider.enabled = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (UpgradesManager.List["chaman"] == false && collision.gameObject.tag == "Player")
        {
            chamanCine.SetActive(true);
            PlayerManager.Instance.playerCanMove = false;
            PlayerManager.Instance.playerRigidBody.velocity = Vector2.zero;
            collision.GetComponent<Animator>().SetBool("IsWalking", false);
            StartCoroutine(Imo());
            GetComponent<UpgradeObject>().Gotcha();
        }
    }

    private IEnumerator Imo()
    {

        yield return new WaitForSeconds(timeOfCine);
        PlayerManager.Instance.playerCanMove = true;
        ChamanCollider.enabled = true;

    }
}