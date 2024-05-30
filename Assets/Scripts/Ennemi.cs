using UnityEngine;
using System.Collections;

public class Ennemi : MonoBehaviour
{
    public bool toleft = true;
    public float speed;
    private Rigidbody2D rgbd;
    private Collider2D enemyCollider;
    private PlayerController playerController; // Référence au script du joueur
    private SpriteRenderer spriteRenderer;

    public float pauseDuration = 1.0f; // Durée de la pause en secondes
    private bool isPaused = false;

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>(); // Obtenez la référence au script du joueur
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isPaused)
        {
            if (toleft)
            {
                rgbd.velocity = new Vector2(-speed, rgbd.velocity.y);
                if (!spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = true;
                }
            }
            else
            {
                rgbd.velocity = new Vector2(speed, rgbd.velocity.y);
                if (spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = false;
                }
            }

            // Ignorer les collisions avec le joueur si le joueur est invisible
            if (playerController.isInvisible)
            {
                Physics2D.IgnoreCollision(playerController.GetComponent<Collider2D>(), enemyCollider, true);
            }
            else
            {
                Physics2D.IgnoreCollision(playerController.GetComponent<Collider2D>(), enemyCollider, false);
            }
        }
        else
        {
            rgbd.velocity = new Vector2(0f, rgbd.velocity.y);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LeftLimit"))
        {
            StartCoroutine(PauseAndChangeDirection());
        }

        if (other.CompareTag("RightLimit"))
        {
            StartCoroutine(PauseAndChangeDirection());
        }

        if (other.CompareTag("Player"))
        {
            playerController.PerdPv();
        }
    }

    private IEnumerator PauseAndChangeDirection()
    {
        isPaused = true; // Activer la pause
        yield return new WaitForSeconds(pauseDuration); // Attendre pendant la durée de la pause
        toleft = !toleft; // Inverser la direction
        isPaused = false; // Désactiver la pause
    }
}

