using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Taguer : MonoBehaviour
{
    private bool isTouchingWall = false;
    private bool isTouchingPlatform = false;
    private PlayerController playerController;
    private Transform platformTransform;

    private Dictionary<string, KeyCode> keyMappings;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        
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
        if (playerController.currentInvisibilityDuration == playerController.maxInvisibilityDuration)
        {
            if (Input.GetKeyDown(keyMappings["Tag"]))
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

        if (Input.GetKeyUp(keyMappings["Tag"]))
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