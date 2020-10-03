using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlockBehavior : MonoBehaviour
{
    public Transform startPos;
    public Transform nextPos;

    public float moveSpeed;

    private bool activated;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        GameEvents.current.onTimeReset += onTimeReset;
    }

    private void onTimeReset()
    {
        transform.position = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(activated)
            transform.position = Vector2.MoveTowards(transform.position, nextPos.position, moveSpeed * Time.deltaTime);

        else
            transform.position = Vector2.MoveTowards(transform.position, startPos.position, moveSpeed * Time.deltaTime);
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
