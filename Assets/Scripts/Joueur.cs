using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jump;
    public float health;

    public Image invisibilityGauge;
    public float maxInvisibilityDuration = 5f;
    public float currentInvisibilityDuration;
    public float invisibilityRechargeRate = 1f;

    private Rigidbody2D rb;
    private SpriteRenderer SpriteRenderer;

    private bool isGrounded;
    public bool isInvisible;

    public Sprite sp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        currentInvisibilityDuration = maxInvisibilityDuration;
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

        currentInvisibilityDuration = Mathf.Clamp(currentInvisibilityDuration, 0, maxInvisibilityDuration);

        if (isInvisible)
        {
            SpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
            currentInvisibilityDuration -= Time.deltaTime;
            if (currentInvisibilityDuration <= 0)
            {
                isInvisible = false;
            }
        }
        else
        {
            SpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            if (currentInvisibilityDuration < maxInvisibilityDuration)
            {
                currentInvisibilityDuration += invisibilityRechargeRate * Time.deltaTime;
            }
        }

        if (invisibilityGauge != null)
        {
            invisibilityGauge.fillAmount = currentInvisibilityDuration / maxInvisibilityDuration;
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