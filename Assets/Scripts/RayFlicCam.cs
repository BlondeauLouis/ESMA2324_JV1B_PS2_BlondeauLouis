using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayFlicCam : MonoBehaviour
{
    private FlicCam flicCam;
    private PlayerController playerController;
    private Collider2D enemyCollider;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        enemyCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isInvisible)
        {
            Physics2D.IgnoreCollision(playerController.GetComponent<Collider2D>(), enemyCollider, true);
        }
        else
        {
            Physics2D.IgnoreCollision(playerController.GetComponent<Collider2D>(), enemyCollider, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            flicCam.Actif();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            flicCam.Passif();
        }
    }
}