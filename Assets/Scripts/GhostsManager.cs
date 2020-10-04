using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostsManager : MonoBehaviour
{
    public int ghostLayer;

    public void ResetGhosts(Transform spawnPosition, int maxGhosts)
    {
        int currentGhost = transform.childCount;

        foreach (Transform child in transform)
        {
            if (currentGhost > maxGhosts)
            {
                child.gameObject.GetComponent<PlayerController>().SelfDestroy();
            }

            else
            {
                child.gameObject.GetComponent<PlayerController>().ResetPlayerObject(spawnPosition, false);
            }

            currentGhost--;
        }
    }

    public void DeleteGhosts()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<PlayerController>().SelfDestroy();
        }
    }
}
