using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonMainMenu : MonoBehaviour
{
    private bool controlActif;

    public GameObject controlSprite;

    public Button start;
    public Button option;
    public Button reset;
    public Button quit;

    private void Update()
    {
        if (controlActif == true)
        {
            if (Input.GetButtonDown("B"))
            {
                controlActif = false;
                controlSprite.SetActive(false);

                start.enabled = true;
                option.enabled = true;
                reset.enabled = true;
                quit.enabled = true;
            }
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }


    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();
    }


    public void OptionButton()
    {
        controlActif = true;
        controlSprite.SetActive(true);

        start.enabled = false;
        option.enabled = false;
        reset.enabled = false;
        quit.enabled = false;
    }
}
