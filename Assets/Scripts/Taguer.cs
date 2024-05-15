using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool isTouchingWall = false; // Indique si le personnage touche un mur

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && isTouchingWall)
        {
            InteractWithWall();
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
        }
    }

    void InteractWithWall()
    {
        Debug.Log("Interacting with the wall!");
    }
}
