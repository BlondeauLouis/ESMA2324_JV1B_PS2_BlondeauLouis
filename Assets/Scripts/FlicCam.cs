using UnityEngine;

public class FlicCam : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rgbd;
    private Collider2D enemyCollider;
    private PlayerController playerController;

    private Transform playerTransform;

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {

        // Ignorer les collisions avec le joueur si le joueur est invisible
        if (playerController.isInvisible)
        {
            Physics2D.IgnoreCollision(playerController.GetComponent<Collider2D>(), enemyCollider, true);
        }
        else
        {
            Physics2D.IgnoreCollision(playerController.GetComponent<Collider2D>(), enemyCollider, false);
        }

        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            Debug.Log("samarchpas");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.PerdPv();
        }
    }
}