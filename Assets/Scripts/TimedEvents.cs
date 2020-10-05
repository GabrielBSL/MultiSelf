using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEvents : MonoBehaviour
{
    public float time;

    [SerializeField]
    public MovableBlockBehavior[] blocks;

    void Start()
    {
        if(time != 0f)
            StartCoroutine(TriggerEventInSeconds());
    }

    IEnumerator TriggerEventInSeconds()
    {
        yield return new WaitForSeconds(time);
        Activate();
    }

    private void Activate()
    {
        foreach (var block in blocks)
        {
            block.Activate();
        }
    }
}
