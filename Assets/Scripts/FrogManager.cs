using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogManager : MonoBehaviour
{
    public void ResetFrog(Transform spawnPosition){
        foreach(Transform child in transform){
            child.gameObject.GetComponent<FrogController>().ResetFrogObject(spawnPosition);
            
        }
    }
}
