using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpColor : MonoBehaviour
{
    private SpriteRenderer sprite;

    private float colorR = 0f;
    private float colorG = 0.5f;
    private float colorB = 1f;

    private float invR = 1f;
    private float invG = 1f;
    private float invB = 1f;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sprite.color = new Color(colorR, colorG, colorB, 1f);

        colorR += Time.deltaTime * invR * 2.5f;
        colorG += Time.deltaTime * invG * 1.5f;
        colorB += Time.deltaTime * invB;

        if (colorR > 1 || colorR < 0)
            invR *= -1f;

        if (colorG > 1 || colorG < 0)
            invG *= -1f;

        if (colorB > 1 || colorB < 0)
            invB *= -1f;
    }
}
