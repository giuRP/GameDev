using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingParallaxEffect : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public BoxCollider2D bCollider;

    private float width;

    [Range(-10, 10)]
    public float scrollSpeed = -0.15f;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        width = bCollider.size.x;
        bCollider.enabled = false;

        rb2d.velocity = new Vector2(scrollSpeed, 0);
    }

    private void FixedUpdate()
    {
        if (transform.position.x < -width)
        {
            Vector2 resetPosition = new Vector2(width * 2f, 0);

            transform.position = (Vector2)transform.position + resetPosition;
        }
    }
}
