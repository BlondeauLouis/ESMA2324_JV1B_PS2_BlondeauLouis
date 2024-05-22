using UnityEngine;
using System.Collections;

public class Taguer : MonoBehaviour
{
    private bool isTouchingWall = false;
    private bool isTouchingPlatform = false;
    private bool isTouchingHeart = false;
    private PlayerController playerController;
    private Transform platformTransform;

    public float tagTime = 1f;

    private GameObject heart;

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
                    StartCoroutine(TaguerHeart());
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
            heart = other.gameObject;
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
            playerController.isInvisible = false;
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
        playerController.isInvisible = true;
        playerController.GainPv();
    }

    IEnumerator TaguerHeart()
    {
        yield return new WaitForSeconds(tagTime);

        // Attendre tant que le joueur reste appuyé sur la touche E
        while (Input.GetKey(KeyCode.E))
        {
            yield return null;
        }

        // Si le joueur ne reste plus appuyé, récupérer le coeur
        InteractWithHeart();

        // Détruire le GameObject du coeur après avoir récupéré le coeur
        if (heart != null)
        {
            Destroy(heart);
        }
    }

}