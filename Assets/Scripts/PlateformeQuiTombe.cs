using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 2f; // D�lai avant la chute
    public float respawnDelay = 5f; // D�lai avant le respawn
    public float fallSpeed = 10f; // Vitesse de la chute
    public Transform spawn;

    private Rigidbody2D rb;
    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            Invoke("Fall", fallDelay);
            isFalling = true;
        }
    }

    void Fall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = fallSpeed;
        // Optionnel : D�sactivez le collider pour �viter les collisions pendant la chute
        GetComponent<Collider2D>().enabled = false;

        // R�activer la plateforme apr�s un certain temps
        Invoke("Respawn", respawnDelay);
    }

    void Respawn()
    {
        rb.bodyType = RigidbodyType2D.Static;
        rb.gravityScale = 0f;
        GetComponent<Collider2D>().enabled = true;
        transform.position = spawn.position;
        // Si vous avez besoin de r�initialiser d'autres propri�t�s, faites-le ici
        isFalling = false; // R�initialisez le drapeau de chute
    }
}
