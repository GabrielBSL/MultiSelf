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
        if (collision.tag == "Ground")
            player.GetComponent<PlayerController>().IsGrounded();

        if (collision.tag == "Player")
        {
            player.GetComponent<PlayerController>().IsGrounded();

            if (collision.transform.position.y < transform.parent.gameObject.transform.position.y &&
                transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0 && 
                collision.gameObject.GetComponent<Rigidbody2D>().velocity.y >= 0)
            {
                collision.gameObject.layer = playerCollisionLayer;
                collision.gameObject.GetComponent<PlayerController>().SetWithPlayer(true, player.gameObject);
            }
        }

        if (collision.tag == "Button")
        {
            player.GetComponent<PlayerController>().IsGrounded();
            collision.gameObject.GetComponent<ButtonBehavior>().ButtonDown(player.gameObject);
        }
            
        //(playerObject.transform.position.y > gameObject.transform.position.y && playerObject.GetComponent<Rigidbody2D>().velocity.y < 0
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.layer = playerNoCollisionLayer;
            collision.gameObject.GetComponent<PlayerController>().SetWithPlayer(false, null);
        }

        if (collision.tag == "Button")
        {
            collision.gameObject.GetComponent<ButtonBehavior>().ButtonUp(player.gameObject);
        }
    }

}
