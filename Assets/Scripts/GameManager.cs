using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Panels")]

    public GameObject pausePanel;
    public GameObject controlsPanel;

    [Header("Rest")]
    public Text timeText;
    public Text MaxCopiesText;
    public GameObject player;
    public GameObject ghosts;
    public GameObject clocks;
    public Transform spawnPlayer;

    public float timeToRespawn;
    public float respawnDelay;
    public int maxGhosts;
    public bool ghostColisions = true;
    private float timer;
    private bool isRestarting;
    private bool isSceneTransitioning;

    public bool isCounting;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        Time.timeScale = 1f;
        isRestarting = false;
        isSceneTransitioning = false;

        if (!isCounting)
        {
            MaxCopiesText.gameObject.SetActive(false);
            timeText.gameObject.SetActive(false);
        }

        if(MaxCopiesText.gameObject.activeSelf == true)
        {
            MaxCopiesText.text = "Max: " + maxGhosts.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSceneTransitioning)
            return;

        if (isCounting && !isRestarting) {
            if (timer < timeToRespawn)
            {
                timer += Time.deltaTime;
                timeText.text = Mathf.Round(timeToRespawn - timer).ToString();
            }
            else
            {
                StartRestart();
            }
        }

        CheckKeys();
    }

    private void CheckKeys()
    {
        if (SceneManager.GetActiveScene().name == "Ending" || !player.GetComponent<PlayerController>().isDead || SceneManager.GetActiveScene().buildIndex < 4)
            return;

        if (Input.GetButtonDown("Cancel"))
        {
            PauseCanvasAction();
        }
        if (Input.GetKeyDown("o"))
        {
            if(!player.GetComponent<PlayerController>().isDead)
                ResetStage();
        }
        if (Input.GetKeyDown("p"))
        {
            StartRestart();
        }
    }

    public void PauseCanvasAction()
    {
        FindObjectOfType<AudioManager>().Play("Button");

        if (pausePanel.active == false)
        {
            Time.timeScale = 0;
            FindObjectOfType<AudioManager>().Pause("Stage_" + SceneManager.GetActiveScene().buildIndex.ToString());

            if (SceneManager.GetActiveScene().buildIndex >= 6 || 
                (SceneManager.GetActiveScene().buildIndex == 5 && FindObjectOfType<TimedEvents>().stage5Music))
            {
                FindObjectOfType<AudioManager>().Pause("GameMusic");
            }

            pausePanel.SetActive(true);
            controlsPanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            FindObjectOfType<AudioManager>().UnPause("Stage_" + SceneManager.GetActiveScene().buildIndex.ToString());

            if (SceneManager.GetActiveScene().buildIndex >= 6 ||
                (SceneManager.GetActiveScene().buildIndex == 5 && FindObjectOfType<TimedEvents>().stage5Music))
            {
                FindObjectOfType<AudioManager>().UnPause("GameMusic");
            }

            pausePanel.SetActive(false);
            controlsPanel.SetActive(false);
        }
    }

    public void StartRestart()
    {
        if (isRestarting)
            return;

        isRestarting = true;
        timer = 0f;
        StartCoroutine(RespawnCall());
    }

    IEnumerator RespawnCall()
    {
        player.GetComponent<PlayerController>().StartDeath();
        yield return new WaitForSeconds(respawnDelay);
        ResetPositions();
    }

    public void ResetStage()
    {
        timer = 0f;

        FindObjectOfType<AudioManager>().Play("PlayerReset");

        player.GetComponent<PlayerController>().ResetPlayerObject(spawnPlayer.transform, true);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        ghosts.GetComponent<GhostsManager>().DeleteGhosts();
        clocks.GetComponent<ClocksManager>().ResetClocks();

        GameEvents.current.TimeReset();
        isRestarting = false;
    }
    
    private void ResetPositions()
    {
        player.GetComponent<PlayerController>().CreateNewGhost(spawnPlayer.transform);
        ghosts.GetComponent<GhostsManager>().ResetGhosts(spawnPlayer.transform, maxGhosts);
        clocks.GetComponent<ClocksManager>().ResetClocks();

        GameEvents.current.TimeReset();
        isRestarting = false;
    }

    public void LoadNextScene()
    {
        Time.timeScale = 0;
        FindObjectOfType<AudioManager>().Play("PlayerNextLevel");
        isSceneTransitioning = true;
        FindObjectOfType<SceneTransition>().StartSceneTransition(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMainMenu()
    {
        isSceneTransitioning = true;
        FindObjectOfType<AudioManager>().ResetAll();
        FindObjectOfType<SceneTransition>().StartSceneTransition(0);
    }

    public void ShowControls()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        pausePanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void HideControls()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        pausePanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void AddTime(float bonusTime)
    {
        timer -= bonusTime;
    }
}
