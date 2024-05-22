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
    public float jump;
    public float health;

    public Image invisibilityGauge;
    public float maxInvisibilityDuration = 5f;
    public float currentInvisibilityDuration;
    public float invisibilityRechargeRate = 1f;
    public float glideForce = 10f;

    public Text livesText; // Assurez-vous que c'est public et de type UnityEngine.UI.Text

    private bool isInvincible = false;
    public float invincibilityTime = 2f;

    private Color baseColor;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded;
    public bool isInvisible;

    public Sprite sp;
    public Transform respawn;

    private bool isFollowingPlatform = false;
    private Transform platformToFollow;

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
        currentInvisibilityDuration = maxInvisibilityDuration;
        baseColor = spriteRenderer.color;

        // Mettre à jour le texte des vies au démarrage
        UpdateLivesText();
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

        if (!isGrounded && Input.GetKey(KeyCode.LeftShift))         //PLANER
        {
            rb.AddForce(Vector2.up * glideForce, ForceMode2D.Force);
        }

        currentInvisibilityDuration = Mathf.Clamp(currentInvisibilityDuration, 0, maxInvisibilityDuration);

        if (isInvisible)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
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

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
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
        UpdateLivesText(); // Mettre à jour le texte des vies lorsqu'on perd une vie
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
            livesText.text = "Lives: " + GameManager.Instance.playerLives.ToString();
        }
    }
}
