using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public float activationSpeed;
    public float moveTime;
    public bool isInverted;
    private float inversion;
    private float timer;
    private bool activated;

    private Vector3 initialPosition;

    private List<GameObject> players;
    
    [SerializeField]
    public MovableBlockBehavior[] blocks;

    private void Start()
    {
        if (isInverted)
            inversion = -1f;

        else
            inversion = 1f;

        activated = false;
        players = new List<GameObject>();
        timer = 0f;
        initialPosition = transform.position;

        GameEvents.current.onTimeReset += onTimeReset;
    }

    private void onTimeReset()
    {
        transform.position = initialPosition;
        Deactivate();
        timer = 0f;
    }

    private void Update()
    {
        if (activated && timer < moveTime)
        {
            timer += Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y - (activationSpeed * Time.deltaTime * inversion), transform.position.z);

            foreach (var player in players)
            {
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - (activationSpeed * Time.deltaTime * inversion), player.transform.position.z);
            }
        }
        else if (!activated && timer > 0)
        {
            timer -= Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y + (activationSpeed * Time.deltaTime * inversion), transform.position.z);
        }
    }

    public void AddPlayer(GameObject newPLayer)
    {
        players.Add(newPLayer);
    }
    public void RemovePlayer(GameObject removePlayer)
    {
        players.Remove(removePlayer);
    }

    public void ButtonDown(GameObject newPLayer)
    {
        activated = true;
        AddPlayer(newPLayer);
        Activate();
    }

    public void ButtonUp(GameObject removePlayer)
    {
        activated = false;
        RemovePlayer(removePlayer);
        Deactivate();
    }

    private void Activate()
    {
        foreach (var block in blocks)
        {
            block.Activate();
        }
    }

    private void Deactivate()
    {
        foreach (var block in blocks)
        {
            block.Deactivate();
        }
    }

    public void StayPressed()
    {
        activated = true;
        Activate();
    }

    public bool GetActivation()
    {
        return activated;
    }
}
