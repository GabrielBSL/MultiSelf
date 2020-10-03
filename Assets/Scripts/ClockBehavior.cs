using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockBehavior : MonoBehaviour
{
    public float bonusTime;
    private bool playerGet = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!collision.GetComponent<PlayerController>().isGhost && !playerGet)
            {
                playerGet = true;
                FindObjectOfType<GameManager>().AddTime(bonusTime);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    public void ResetClock()
    {
        playerGet = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
