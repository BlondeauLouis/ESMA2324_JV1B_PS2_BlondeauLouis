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
    public Transform respawn;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        currentInvisibilityDuration = maxInvisibilityDuration;
    }

    void Update()
    {
        if (!isInvisible)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");       //MOUVEMENT
            Vector3 direction = new Vector2(moveHorizontal, 0);
            transform.Translate(direction * speed * Time.deltaTime);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && !isInvisible)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);     //SAUT
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
            float fillAmount = Mathf.Clamp(currentInvisibilityDuration / maxInvisibilityDuration, 0f, 1f);
            invisibilityGauge.rectTransform.localScale = new Vector3(fillAmount, 1f, 1f);
        }

        if (health <= 0)
        {
            perdu();
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

    public void perdPv()
    {
        health -= 1;
    }

    public void perdu()
    {
        //Destroy(gameObject);
        //SceneManager.LoadScene("Scene0");
        transform.position = respawn.position;
        health = 3;
    }

}