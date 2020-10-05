using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlockBehavior : MonoBehaviour
{
    public Transform startPos;
    public Transform nextPos;

    private List<GameObject> players;

    float playerDirectionX = 1f;
    float playerDirectionY = 1f;

    public float moveSpeed;

    private bool activated;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        players = new List<GameObject>();
        GameEvents.current.onTimeReset += onTimeReset;
    }

    private void onTimeReset()
    {
        if (startPos == null || nextPos == null)
            return;

        transform.position = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (startPos == null || nextPos == null)
            return;

        GetDirection();

        foreach (var player in players)
        {
            player.transform.position = new Vector3(player.transform.position.x + (moveSpeed * Time.deltaTime * playerDirectionX),
                player.transform.position.y + (moveSpeed * Time.deltaTime * playerDirectionY),
                player.transform.position.z);
        }

        if (activated)
            transform.position = Vector2.MoveTowards(transform.position, nextPos.position, moveSpeed * Time.deltaTime);

        else
            transform.position = Vector2.MoveTowards(transform.position, startPos.position, moveSpeed * Time.deltaTime);
    }

    private void GetDirection()
    {
        if (players.Count > 0)
        {
            if (activated)
            {
                if (transform.position.y < nextPos.position.y)
                    playerDirectionY = 1f;

                else if (transform.position.y > nextPos.position.y)
                    playerDirectionY = -1f;

                else
                    playerDirectionY = 0f;

                if (transform.position.x < nextPos.position.x)
                    playerDirectionX = 1f;

                else if (transform.position.x > nextPos.position.x)
                    playerDirectionX = -1f;

                else
                    playerDirectionX = 0f;
            }

            else
            {
                if (transform.position.y < startPos.position.y)
                    playerDirectionY = 1f;

                else if (transform.position.y > startPos.position.y)
                    playerDirectionY = -1f;

                else
                    playerDirectionY = 0f;

                if (transform.position.x < startPos.position.x)
                    playerDirectionX = 1f;

                else if (transform.position.x > startPos.position.x)
                    playerDirectionX = -1f;

                else
                    playerDirectionX = 0f;
            }
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

    public void Activate()
    {
        activated = true;
    }

    public void Deactivate()
    {
        activated = false;
    }
}
