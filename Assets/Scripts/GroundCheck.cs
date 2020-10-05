using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private GameObject player;
    public int playerNoCollisionLayer;
    public int playerCollisionLayer;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.y < transform.parent.transform.position.y &&
                transform.parent.GetComponent<Rigidbody2D>().velocity.y <
                collision.GetComponent<Rigidbody2D>().velocity.y &&
                FindObjectOfType<GameManager>().ghostColisions)
            {
                collision.gameObject.layer = playerCollisionLayer;
                collision.GetComponent<PlayerController>().SetWithPlayer(true, player.gameObject);
            }
        }

        if (collision.tag == "Button")
            collision.gameObject.GetComponent<ButtonBehavior>().ButtonDown(player.gameObject);     

        if (collision.tag == "MovableBlock")
            collision.GetComponent<MovableBlockBehavior>().AddPlayer(player.gameObject);   
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        player.GetComponent<PlayerController>().IsGrounded();

        if (collision.tag == "Button")
        {
            if (!collision.GetComponent<ButtonBehavior>().GetActivation())
            {
                collision.GetComponent<ButtonBehavior>().StayPressed();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.GetComponent<PlayerController>().isNotGrounded();

        if (collision.tag == "Player")
        {
            if (FindObjectOfType<GameManager>().ghostColisions)
            {
                collision.gameObject.layer = playerNoCollisionLayer;
                collision.GetComponent<PlayerController>().SetWithPlayer(false, null);
            }        }

        if (collision.tag == "Button")
        {
            collision.GetComponent<ButtonBehavior>().ButtonUp(player.gameObject);
        }

        if (collision.tag == "MovableBlock")
        {
            player.GetComponent<PlayerController>().IsGrounded();
            collision.GetComponent<MovableBlockBehavior>().RemovePlayer(player.gameObject);
        }
    }
}
