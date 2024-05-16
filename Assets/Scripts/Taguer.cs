using UnityEngine;

public class Taguer : MonoBehaviour
{
    private bool isTouchingWall = false;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    void Update()
    {   if (playerController.currentInvisibilityDuration == playerController.maxInvisibilityDuration)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isTouchingWall)
                {
                    InteractWithWall();
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            playerController.isInvisible = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            isTouchingWall = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            isTouchingWall = false;
            playerController.isInvisible = false;
        }
    }

    void InteractWithWall()
    {
        playerController.isInvisible = true;
    }
}
