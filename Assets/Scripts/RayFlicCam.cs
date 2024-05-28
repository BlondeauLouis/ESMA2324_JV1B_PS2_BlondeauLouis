using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayFlicCam : MonoBehaviour
{
    public FlicCam flicCam;

    private PlayerController playerController;
    private Collider2D enemyCollider;

    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        enemyCollider = GetComponent<Collider2D>();

        if (flicCam == null)
        {
            Debug.LogError("FlicCam reference is not assigned in RayFlicCam script.");
        }
    }

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
            flicCam.isActif = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            flicCam.isActif = false;
        }
    }
}
