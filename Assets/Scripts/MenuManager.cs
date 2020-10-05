using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject ControlPanel;
    public GameObject CreditsPanel;

    public void StartGame()
    {
        FindObjectOfType<SceneTransition>().StartSceneTransition(1);
        FindObjectOfType<AudioManager>().Play("PlayerNextLevel");
    }

    public void Controls()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        mainPanel.SetActive(false);
        ControlPanel.SetActive(true);
    }

    public void BackControls()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        mainPanel.SetActive(true);
        ControlPanel.SetActive(false);
    }

    public void Credits()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        mainPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }

    public void BackCredits()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        mainPanel.SetActive(true);
        CreditsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
