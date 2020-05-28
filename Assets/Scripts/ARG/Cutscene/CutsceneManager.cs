using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Manager;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    private float timeOfCutscene;
    [SerializeField]
    private GameObject videoPlayer;
    [SerializeField]
    private int numberOfScene;
    [SerializeField]
    private bool isOutro;

    // Start is called before the first frame update
    void Start()
    {
        if (isOutro)
        {
            PlayerManager.Instance.gameObject.SetActive(false);
        }

        videoPlayer.SetActive(true);
        StartCoroutine(DelayStartScene());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("X"))
        {
            SceneManager.LoadScene(numberOfScene);
        }
    }

    private IEnumerator DelayStartScene()
    {
        yield return new WaitForSecondsRealtime(timeOfCutscene);
        videoPlayer.SetActive(false);
        SceneManager.LoadScene(numberOfScene);
    }
}
