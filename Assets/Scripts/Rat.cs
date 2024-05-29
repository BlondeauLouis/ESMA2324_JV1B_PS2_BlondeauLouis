using UnityEngine;
using System.Collections;

public class Rat : MonoBehaviour
{
    public bool toleft = true;
    public float speed;
    private Rigidbody2D rgbd;
    private Collider2D enemyCollider;
    private PlayerController playerController;

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>(); // Obtenez la r�f�rence au script du joueur
    }

    void Update()
    {
        if (toleft)
        {
            rgbd.velocity = new Vector2(-speed, rgbd.velocity.y);
        }
        else
        {
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
            Debug.Log("LeftLimit touch�e");
            toleft = false;
        }

        if (other.CompareTag("RightLimit"))
        {
            Debug.Log("RightLimit touch�e");
            toleft = true;
        }

        if (other.CompareTag("Player"))
        {
            playerController.PerdPv();
        }
    }
}