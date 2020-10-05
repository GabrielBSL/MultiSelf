using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("PauseCanvas")]
    public GameObject pauseCanvas;

    [Header("Rest")]
    public Text timeText;
    public GameObject player;
    public GameObject ghosts;
    public GameObject clocks;
    public Transform spawnPlayer;

    public float timeToRespawn;
    public float respawnDelay;
    public int maxGhosts;
    private float timer;
    private bool isRestarting;

    public bool isCounting;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        isRestarting = false;

        if (!isCounting)
            timeText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
        if (pauseCanvas.active == false)
        {
            Time.timeScale = 0;
            pauseCanvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseCanvas.SetActive(false);
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

    public void AddTime(float bonusTime)
    {
        timer -= bonusTime;
    }
}
