using UnityEngine;

public class Taguer : MonoBehaviour
{
    private bool isTouchingWall = false;
    private bool isTouchingPlatform = false;
    private bool isTouchingHeart = false;
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
                else if (isTouchingHeart)
                {
                    InteractWithHeart();
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
        else if (other.CompareTag("Heart"))
        {
            isTouchingHeart = true;
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
        else if (other.CompareTag("Heart"))
        {
            isTouchingHeart = false;
            Destroy(other);
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

    void InteractWithHeart()
    {
        playerController.GainPv();
    }
}

