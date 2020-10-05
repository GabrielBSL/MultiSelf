using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerBlockBehavior : MonoBehaviour
{
    public bool killGhost;

    private void Start()
    {
        if (killGhost)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 0.75f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 1f, 0.75f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.GetComponent<PlayerController>().isGhost == killGhost)
            {
                if (collision.name == "Player")
                    collision.GetComponent<PlayerController>().SetDeath();

                else
                    collision.GetComponent<PlayerController>().StartDeath();
            }
        }
    }
}
