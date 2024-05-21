using UnityEngine;

public class PlateformeTraversable : MonoBehaviour
{
    private Collider2D platformCollider;

    void Start()
    {
        // R�cup�rer le collider de la plateforme
        platformCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // V�rifier la position du joueur par rapport � la plateforme
        Vector2 playerPosition = PlayerController.Instance.transform.position;
        Vector2 platformPosition = transform.position;
        bool playerBelowPlatform = playerPosition.y < platformPosition.y;

        // Autoriser le passage si le joueur est en dessous de la plateforme
        if (playerBelowPlatform)
        {
            // Permettre au joueur de passer � travers la plateforme
            Physics2D.IgnoreCollision(PlayerController.Instance.GetComponent<Collider2D>(), platformCollider, true);
        }
        else
        {
            // Emp�cher le joueur de passer � travers la plateforme
            Physics2D.IgnoreCollision(PlayerController.Instance.GetComponent<Collider2D>(), platformCollider, false);
        }
    }
}
