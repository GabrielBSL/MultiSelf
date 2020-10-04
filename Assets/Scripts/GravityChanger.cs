using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChanger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (collision.GetComponent<PlayerController>().gravityChanged)
                return;

            collision.GetComponent<PlayerController>().GravityHasChanged();

            collision.GetComponent<Rigidbody2D>().gravityScale *= -1f;
            collision.GetComponent<PlayerController>().gravityRotation *= -1f;
        }
    }
}
