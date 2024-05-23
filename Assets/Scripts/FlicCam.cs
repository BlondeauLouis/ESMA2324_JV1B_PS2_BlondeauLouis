using UnityEngine;

public class FlicCam : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rgbd;
    private Collider2D enemyCollider;
    private PlayerController playerController;

    private Transform playerTransform;
    private float initialY;

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        initialY = transform.position.y; // Sauvegarder la position initiale en Y
    }

    void Update()
    {
        // Ignorer les collisions avec le joueur si le joueur est invisible
        if (playerController.isInvisible)
        {
            Physics2D.IgnoreCollision(playerController.GetComponent<Collider2D>(), enemyCollider, true);
            rgbd.velocity = Vector2.zero; // Arrêter le mouvement si le joueur est invisible
        }
        else
        {
            Physics2D.IgnoreCollision(playerController.GetComponent<Collider2D>(), enemyCollider, false);

            if (playerTransform != null)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                direction.y = 0; // Contraindre le mouvement uniquement sur l'axe X
                rgbd.velocity = direction * speed;
            }
            else
            {
                Debug.Log("Player not found");
            }
        }

        // Verrouiller la position en Y pour éviter l'enfoncement
        Vector3 position = transform.position;
        position.y = initialY;
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.PerdPv();
        }
    }
}
