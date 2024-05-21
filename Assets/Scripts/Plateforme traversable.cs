using UnityEngine;

public class PlateformeTraversable : MonoBehaviour
{
    private Collider2D platformCollider;

    void Start()
    {
        // Récupérer le collider de la plateforme
        platformCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Vérifier la position du joueur par rapport à la plateforme
        Vector2 playerPosition = PlayerController.Instance.transform.position;
        Vector2 platformPosition = transform.position;
        bool playerBelowPlatform = playerPosition.y < platformPosition.y;

        // Autoriser le passage si le joueur est en dessous de la plateforme
        if (playerBelowPlatform)
        {
            // Permettre au joueur de passer à travers la plateforme
            Physics2D.IgnoreCollision(PlayerController.Instance.GetComponent<Collider2D>(), platformCollider, true);
        }
        else
        {
            // Empêcher le joueur de passer à travers la plateforme
            Physics2D.IgnoreCollision(PlayerController.Instance.GetComponent<Collider2D>(), platformCollider, false);
        }
    }
}
