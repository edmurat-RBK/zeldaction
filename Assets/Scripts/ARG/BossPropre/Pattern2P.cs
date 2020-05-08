using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern2P : MonoBehaviour
{
    public Totem totem;
    public GameObject totemGO;
    public Transform[] pointsOfImpact;
    private float minWaiting;
    private float maxWaiting;
    public List<GameObject> allEnnemis;
    [SerializeField]
    private int timeBeforeLunchPattern;
    private bool ennemiDead = false;
    public bool l_isInAction;
    public bool hardStop;

    private Animator anim;
    private Animator animBoss;

    private void Start()
    {
        anim = totemGO.GetComponentInChildren<Animator>();
        animBoss = BossManagerP.instance.GetComponent<Animator>();
    }
    void Update()
    {
        if (hardStop == false)
        {

            allEnnemis.RemoveAll(list_item => list_item == null);

            if (allEnnemis.Count == 0 && ennemiDead == false)
            {
                BossManagerP.instance.ActivateClepsydre();
                ennemiDead = true;
            }

            if (!totem.isInAction && l_isInAction != totem.isInAction)
            {
                StartCoroutine(RepeatPhase2());
                l_isInAction = totem.isInAction;

            }
            else if (totem.isInAction && l_isInAction != totem.isInAction)
            {
                l_isInAction = totem.isInAction;
            }
        }
        else
        {
            anim.SetBool("IsShake", false);
            anim.SetBool("IsFall", false);
            animBoss.SetBool("TotemAttack", false);
        }

    }

    [ContextMenu("StartMovement Pattern 2")]
    public void StartTotemMovement()
    {
        totem.StartMovement(pointsOfImpact[Random.Range(0, pointsOfImpact.Length -1)].position);
    }

    public IEnumerator InitialisePattern2()
    {
        foreach (GameObject ennemi in allEnnemis)
        {
            ennemi.SetActive(true);
        }
        totem.enabled = true;
        yield return new WaitForSeconds(timeBeforeLunchPattern);
        StartTotemMovement();
    }

    public IEnumerator RepeatPhase2()
    {
        yield return new WaitForSeconds(Random.Range(minWaiting, maxWaiting));
        StartTotemMovement();
    }
}
