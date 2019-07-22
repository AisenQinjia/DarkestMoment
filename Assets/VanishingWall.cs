using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingWall : MonoBehaviour
{
    public float fadeTime;
    public float resetTime;
    bool canFade = false;
    bool startReset = false;
    float timer = 0;
    float fadeSpeed;

    SpriteRenderer spriteRenderer;
    BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        fadeSpeed = 1 / fadeTime;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        collider = gameObject.GetComponent<BoxCollider2D>();
        if (spriteRenderer == null || collider == null) Debug.Log("Null");

    }

    // Update is called once per frame
    void Update()
    {
        if (canFade)
        {
            Color c = spriteRenderer.color;
            c.a = (c.a - fadeSpeed * Time.deltaTime);
            if(c.a <= 0)
            {
                c.a = 0;
                TransparentLowerThanZero();
                spriteRenderer.color = c;
            }
            else spriteRenderer.color = c;

        }
        else if (startReset)
        {
            timer += Time.deltaTime;
            if (timer > resetTime) ShowWall();
        }
    }

    void TransparentLowerThanZero()
    {
        canFade = false;
        collider.enabled = false;
        startReset = true;
    }

    void ShowWall()
    {
        startReset = false;
        canFade = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        collider.enabled = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            canFade = true;
        }
    }
}
