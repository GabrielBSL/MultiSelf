using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public GameObject stanAnimation;
    public float transitionTime;

    private void Start()
    {
        if (!stanAnimation.activeSelf)
            stanAnimation.SetActive(true);
    }

    public void StartSceneTransition(int levelIndex)
    {
        StartCoroutine(LoadLevel(stanAnimation.GetComponent<Animator>(), levelIndex));
    }

    IEnumerator LoadLevel(Animator anim, int levelIndex)
    {
        anim.SetTrigger("SceneTransition");

        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
