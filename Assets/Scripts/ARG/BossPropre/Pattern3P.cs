using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern3P : MonoBehaviour
{
    public Totem2 totem2;
    public List<GameObject> allEnnemis;
    [SerializeField]
    private int timeBeforeLunchPattern;
    private bool ennemiDead = false;
    public bool hardStop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        }
    }

    public IEnumerator InitialisePattern3()
    {
        foreach (GameObject ennemi in allEnnemis)
        {
            ennemi.SetActive(true);
        }
        totem2.enabled = true;
        yield return new WaitForSeconds(timeBeforeLunchPattern);
        totem2.LaunchMovement();
    }
}
