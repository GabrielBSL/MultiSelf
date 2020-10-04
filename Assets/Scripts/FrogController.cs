using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anime;
    public Transform rightColliding;
    public Transform leftColliding; 
    public Transform headPoint;
    public Transform spawnFrog;
    public LayerMask layer;
    public float speed;

    private int currentMovement;
    private bool collidingWall;
    private bool isDead;
    
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        isDead = false;
        
        currentMovement = 0;
    }

    void Update()
    {
        Movement();
     
    }

    void Movement(){
        rigid.velocity = new Vector2(speed, rigid.velocity.y);
        collidingWall = Physics2D.Linecast(rightColliding.position, leftColliding.position, layer);
        if(collidingWall){
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }
    }

    IEnumerator OnCollisionEnter2D(Collision2D collision){
        
        if(collision.gameObject.tag == "Player"){
            if(collision.contacts[0].point.y - headPoint.position.y > 0){
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                anime.SetBool("die", true);
                anime.SetBool("walk", false);
                yield return new WaitForSeconds(0.05f);
                StartDeath();
            }    
        }

    }

    public void ResetFrogObject(Transform spawnPosition){
        StartRespawn();
        currentMovement = 0;
        transform.position = spawnPosition.position;
    }

    private void StartRespawn(){
        isDead = false;
        anime.SetBool("die", false);
        anime.SetBool("walk", true);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        
    }

    public void StartDeath(){
        isDead = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        
        
    }

}
