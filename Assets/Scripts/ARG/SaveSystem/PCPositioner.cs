using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Manager;

public class PCPositioner : MonoBehaviour
{

    NavigationPoint mySpawnpoint;

    void Start()
    {

        if (SceneManager.GetActiveScene().name == SvgManager.SvgData.currentSceneName && FindSpawnPoint())
        {
            Reposition();
        }
        else if (SceneManager.GetActiveScene().name != SvgManager.SvgData.currentSceneName)
        {
            StartCoroutine(Delay());
        }
    }

    public bool FindSpawnPoint()
    {
        int checkptID = SvgManager.SvgData.currCheckpointID;

        NavigationPoint[] npArray = FindObjectsOfType<NavigationPoint>();

        foreach(NavigationPoint np in npArray)
        {
            if (np.iD == checkptID)
            {
                mySpawnpoint = np;
                break;
            }
        }

        if (mySpawnpoint == null)
        {
            Debug.LogErrorFormat("<color=reds>ERROR: No entrey point with ID" + checkptID + "was found in the current scene(" + SvgManager.SvgData.currentSceneName + ")");
            return false;
        }

        return true;
    }

    public void Reposition()
    {

        Vector2 spawnPos = new Vector2(mySpawnpoint.transform.position.x, mySpawnpoint.transform.position.y);
        PlayerManager.Instance.transform.position = spawnPos;
        //PlayerManager.Instance.instantRepo(spawnPos, mySpawnpoint.mustPCFaceLeft);
    }

    private IEnumerator Delay()
    {
        SceneManager.LoadScene(SvgManager.SvgData.currentSceneName);
        yield return new WaitForSeconds(0.01f);
        FindSpawnPoint();
        Reposition();
    }
}
