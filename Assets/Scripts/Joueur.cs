using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public float speed;
    private float baseSpeed;
    private float glideSpeed;
    private float mudSpeed;

    public float jump;
    public float health;

    public Image invisibilityGauge;
    public float maxInvisibilityDuration = 5f;
    public float currentInvisibilityDuration;
    public float invisibilityRechargeRate = 1f;
    public float glideForce = 10f;

    public Text livesText;

    private bool isInvincible = false;
    public float invincibilityTime = 2f;

    private Color baseColor;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isGrounded;
    private bool isInMud = false;
    public bool isInvisible;

    public Sprite walk1, walk2, jumpSprite;
    public Transform respawn;

    private bool isFollowingPlatform = false;
    private Transform platformToFollow;

    private Dictionary<string, KeyCode> keyMappings;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Il y a déjà une instance de PlayerController dans la scène.");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentInvisibilityDuration = maxInvisibilityDuration;
        baseColor = spriteRenderer.color;
        baseSpeed = speed;
        glideSpeed = speed * 0.75f;
        mudSpeed = speed * 0.5f;

        UpdateLivesText();

        keyMappings = new Dictionary<string, KeyCode>
        {
            { "MoveLeft", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveLeft", "A")) },
            { "MoveRight", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MoveRight", "D")) },
            { "Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")) },
            { "Tag", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Tag", "E")) },
            { "Glide", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Glide", "LeftShift")) }
        };
    }

    void Update()
    {

        if (!isInvisible)
        {
            float moveHorizontal = 0f;

            if (Input.GetKey(keyMappings["MoveLeft"]))  //BOUGER A GAUCHE
            {
                moveHorizontal = -1f;
                if (!spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = true;
                }
            }
            else if (Input.GetKey(keyMappings["MoveRight"]))  //BOUGER A DROITE
            {
                moveHorizontal = 1f;
                if (spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = false;
                }
            }

            if (moveHorizontal != 0 && isGrounded)
            {
                animator.Play("Walk");
            }
            else
            {
                animator.Play("Stay");
            }

            Vector3 direction = new Vector2(moveHorizontal, 0);
            transform.Translate(direction * speed * Time.deltaTime);
        }

        if (isGrounded && Input.GetKeyDown(keyMappings["Jump"]) && !isInvisible)    //SAUTER
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            spriteRenderer.sprite = jumpSprite;
        }

        if (!isGrounded && Input.GetKey(keyMappings["Glide"]) && rb.velocity.y < 0)     //PLANER
        {
            speed = glideSpeed;
            rb.gravityScale = 0.4f;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -5f));
            animator.Play("Glide");
        }
        else if (!isInMud)
        { 
            rb.gravityScale = 5f;
            speed = baseSpeed;
        }

        currentInvisibilityDuration = Mathf.Clamp(currentInvisibilityDuration, 0, maxInvisibilityDuration);

        if (isInvisible)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
            animator.Play("Tag");
            currentInvisibilityDuration -= Time.deltaTime;
            if (currentInvisibilityDuration <= 0)
            {
                isInvisible = false;
                StopFollowingPlatform();
            }
        }
        else
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
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
            Perdu();
        }

        if (isFollowingPlatform)
        {
            FollowPlatform();
        }

        if (GameManager.Instance.playerLives <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Mud"))
        {
            isInMud = true;
            speed = mudSpeed;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Mud"))
        {
            isInMud = false;
            speed = baseSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Heart"))
        {
            Destroy(collision.gameObject);
            GainPv();
        }

        if (collision.gameObject.CompareTag("Life"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.GainLife();
            UpdateLivesText();
        }
    }

    public void PerdPv()
    {
        if (!isInvincible)
        {
            health--;
            StartCoroutine(InvincibilityRoutine());
        }
    }

    public void GainPv()
    {
        if (health < 3)
        {
            health++;
        }
    }

    public void Perdu()
    {
        GameManager.Instance.LoseLife();
        transform.position = respawn.position;
        health = 3;
        UpdateLivesText();
    }

    IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    public void StartFollowingPlatform(Transform platform)
    {
        isFollowingPlatform = true;
        platformToFollow = platform;
    }

    public void StopFollowingPlatform()
    {
        isFollowingPlatform = false;
        platformToFollow = null;
    }

    private void FollowPlatform()
    {
        if (platformToFollow != null)
        {
            transform.position = new Vector2(transform.position.x, platformToFollow.position.y);
        }
    }

    private void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = GameManager.Instance.playerLives.ToString();
        }
    }
}