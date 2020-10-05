using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimedEvents : MonoBehaviour
{
    public float time;

    public GameObject background;

    public GameObject ground;
    public GameObject newGround;
    public GameObject creditsText;

    public GameObject menuButton;
    
    public bool stage5Music = false;

    [SerializeField]
    public MovableBlockBehavior[] blocks;

    void Start()
    {
        StartCoroutine(NarrationDelay(1.25f));

        if (SceneManager.GetActiveScene().buildIndex == 1)
            time = 20f;

        else if (SceneManager.GetActiveScene().buildIndex == 2)
            time = 0;

        else if (SceneManager.GetActiveScene().buildIndex == 3)
            time = 40;

        if (time != 0f)
            StartCoroutine(TriggerEventInSeconds());
    }

    IEnumerator TriggerEventInSeconds()
    {
        yield return new WaitForSeconds(time);
        Activate();
    }

    IEnumerator NarrationDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (SceneManager.GetActiveScene().name == "Ending")
            StartCoroutine(EndingSequence(44f));

        else
            FindObjectOfType<AudioManager>().Play("Stage_" + SceneManager.GetActiveScene().buildIndex.ToString());

        if(SceneManager.GetActiveScene().buildIndex > 5 && SceneManager.GetActiveScene().name != "Ending")
            FindObjectOfType<AudioManager>().Play("GameMusic");

        if (SceneManager.GetActiveScene().buildIndex == 5)
            StartCoroutine(StartMusic(23f));

        if (SceneManager.GetActiveScene().buildIndex == 6)
            StartCoroutine(ReduceMusicVolume(5.2f));

        if (SceneManager.GetActiveScene().buildIndex == 7)
            StartCoroutine(StartBackground(8.5f));

        if (SceneManager.GetActiveScene().buildIndex == 8)
            StartCoroutine(ReduceMusicVolume(10.1f));

        if (SceneManager.GetActiveScene().buildIndex == 9)
            StartCoroutine(ReduceMusicVolume(5.7f));

        if (SceneManager.GetActiveScene().buildIndex == 10)
            StartCoroutine(StartGround(9f));

        if (SceneManager.GetActiveScene().buildIndex == 11)
            StartCoroutine(ReduceMusicVolume(7.2f));

        if (SceneManager.GetActiveScene().buildIndex == 12)
            StartCoroutine(ReduceMusicVolume(8.6f));

        if (SceneManager.GetActiveScene().buildIndex == 13)
            StartCoroutine(ReduceMusicVolume(7.7f));
    }

    IEnumerator ReduceMusicVolume(float time)
    {
        FindObjectOfType<AudioManager>().SetVolume("GameMusic", 0.3f);
        yield return new WaitForSeconds(time);
        FindObjectOfType<AudioManager>().SetVolume("GameMusic", 1f);
    }

    IEnumerator StartMusic(float delay)
    {
        yield return new WaitForSeconds(delay);
        FindObjectOfType<AudioManager>().Play("GameMusic");
        stage5Music = true;
        FindObjectOfType<AudioManager>().SetVolume("GameMusic", 1f);
    }

    IEnumerator StartBackground(float delay)
    {
        FindObjectOfType<AudioManager>().SetVolume("GameMusic", 0.3f);
        yield return new WaitForSeconds(delay);
        FindObjectOfType<AudioManager>().SetVolume("GameMusic", 1f);
        background.SetActive(true);
    }

    IEnumerator StartGround(float delay)
    {
        FindObjectOfType<AudioManager>().SetVolume("GameMusic", 0.3f);
        yield return new WaitForSeconds(delay);
        FindObjectOfType<AudioManager>().SetVolume("GameMusic", 1f);
        newGround.SetActive(true);
        ground.SetActive(false);
    }

    IEnumerator EndingSequence(float delay)
    {
        FindObjectOfType<AudioManager>().Stop("GameMusic");
        FindObjectOfType<AudioManager>().Play("Ending");
        yield return new WaitForSeconds(delay);

        FindObjectOfType<PlayerController>().StartDeath();
        StartCoroutine(CreditsWait());
    }

    IEnumerator CreditsWait()
    {
        yield return new WaitForSeconds(1f);

        creditsText.GetComponent<CanvasGroup>().alpha = 1f;
        string text = "Created by:\n\nGabriel Barroso\nLucas Lima";
        StartCoroutine(CreditsSequence(3f, text, 4));
    }

    IEnumerator CreditsSequence(float delay, string text, int textRemaings)
    {
        creditsText.GetComponent<Text>().text = text;

        yield return new WaitForSeconds(delay);

        if (textRemaings == 4)
        {
            string newText = "Music by:\n\nVertex Studio";
            StartCoroutine(CreditsSequence(3f, newText, 3));
        }
        else if (textRemaings == 3)
        {
            string newText = "Sounds by:\n\nDustyroom";
            StartCoroutine(CreditsSequence(3f, newText, 2));
        }
        else if (textRemaings == 2)
        {
            string newText = "Background by:\n\nBayat Games";
            StartCoroutine(CreditsSequence(3f, newText, 1));
        }
        else if (textRemaings == 1)
        {
            string newText = "Narrator:\n\nSpik.AI";
            StartCoroutine(CreditsSequence(3f, newText, 0));
        }
        else
        {
            creditsText.SetActive(false);
            ActivateMenuButton();
        }
    }

    private void ActivateMenuButton()
    {
        menuButton.SetActive(true);
    }

    private void Activate()
    {
        foreach (var block in blocks)
        {
            block.Activate();
        }
    }
}
