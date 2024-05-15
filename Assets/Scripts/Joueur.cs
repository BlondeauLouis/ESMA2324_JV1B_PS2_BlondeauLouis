using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jump;
    public float health;

    private Rigidbody2D rb;
    private SpriteRenderer SpriteRenderer;

    private bool isGrounded;
    public bool isInvisible;

    public Sprite sp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector2(moveHorizontal, 0);

        transform.Translate(direction * speed * Time.deltaTime);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }

        if (isInvisible)
        {
            SpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            SpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}