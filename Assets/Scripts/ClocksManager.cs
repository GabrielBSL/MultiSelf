using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClocksManager : MonoBehaviour
{
    public void ResetClocks()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<ClockBehavior>().ResetClock();
        }
    }
}
