using UnityEngine;
using System.Collections;

public class Rat : MonoBehaviour
{
    public bool toleft = true;
    public float speed;
    private Rigidbody2D rgbd;
    private Collider2D enemyCollider;
    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>(); // Obtenez la r�f�rence au script du joueur
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (toleft)
        {
            if (!spriteRenderer.flipX)
            {
                spriteRenderer.flipX = true;
            }
            rgbd.velocity = new Vector2(-speed, rgbd.velocity.y);
        }
        else
        {
            if (spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false;
            }
            rgbd.velocity = new Vector2(speed, rgbd.velocity.y);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LeftLimit"))
        {
            toleft = false;
        }

        if (other.CompareTag("RightLimit"))
        {
            toleft = true;
        }

        if (other.CompareTag("Player"))
        {
            playerController.PerdPv();
        }
    }
}