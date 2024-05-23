using UnityEngine;
using System.Collections;

public class Taguer : MonoBehaviour
{
    private bool isTouchingWall = false;
    private bool isTouchingPlatform = false;
    private PlayerController playerController;
    private Transform platformTransform;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerController.currentInvisibilityDuration == playerController.maxInvisibilityDuration)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isTouchingWall)
                {
                    InteractWithWall();
                }
                else if (isTouchingPlatform)
                {
                    InteractWithPlatform();
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            playerController.isInvisible = false;
            playerController.StopFollowingPlatform();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            isTouchingWall = true;
        }
        else if (other.CompareTag("Ascenseur"))
        {
            isTouchingPlatform = true;
            platformTransform = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            isTouchingWall = false;
            playerController.isInvisible = false;
        }
        else if (other.CompareTag("Ascenseur"))
        {
            isTouchingPlatform = false;
            playerController.isInvisible = false;
            playerController.StopFollowingPlatform();
        }
    }

    void InteractWithWall()
    {
        playerController.isInvisible = true;
    }

    void InteractWithPlatform()
    {
        playerController.isInvisible = true;
        playerController.StartFollowingPlatform(platformTransform);
    }


}