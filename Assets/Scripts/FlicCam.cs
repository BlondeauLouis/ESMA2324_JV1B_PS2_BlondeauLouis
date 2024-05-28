using UnityEngine;
using System.Collections;

public class FlicCam : MonoBehaviour
{
    public float speed;
    private float regularSpeed;
    private float superSpeed;
    private Rigidbody2D rgbd;
    private Collider2D enemyCollider;
    private PlayerController playerController;

    private Transform playerTransform;
    private float initialY;

    public bool toleft = true;
    public bool isActif = false;

    public float pauseDuration = 1.0f; // Durée de la pause en secondes
    private bool isPaused = false;

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        initialY = transform.position.y;
        regularSpeed = speed;
        superSpeed = speed * 1.5f;
    }

    void Update()
    {
        if (isActif)
        {
            Actif();
        }
        else
        {
            if (!isPaused)
            {
                Passif();
            }
            else
            {
                rgbd.velocity = new Vector2(0f, rgbd.velocity.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActif && other.CompareTag("LeftLimit"))
        {
            StartCoroutine(PauseAndChangeDirection());
        }

        if (!isActif && other.CompareTag("RightLimit"))
        {
            StartCoroutine(PauseAndChangeDirection());
        }

        if (other.CompareTag("Player"))
        {
            playerController.PerdPv();
        }
    }

    public void Actif()
    {
        if (playerController.isInvisible)
        {
            Physics2D.IgnoreCollision(playerController.GetComponent<Collider2D>(), enemyCollider, true);
            rgbd.velocity = Vector2.zero;
        }
        else
        {
            speed = superSpeed;
            Physics2D.IgnoreCollision(playerController.GetComponent<Collider2D>(), enemyCollider, false);

            if (playerTransform != null)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                direction.y = 0;
                rgbd.velocity = direction * speed;
            }
            else
            {
                Debug.Log("Player not found");
            }
        }

        Vector3 position = transform.position;
        position.y = initialY;
        transform.position = position;
    }

    public void Passif()
    {
        speed = regularSpeed;
        if (toleft)
        {
            rgbd.velocity = new Vector2(-speed, rgbd.velocity.y);
        }
        else
        {
            rgbd.velocity = new Vector2(speed, rgbd.velocity.y);
        }
    }

    private IEnumerator PauseAndChangeDirection()
    {
        isPaused = true; // Activer la pause
        yield return new WaitForSeconds(pauseDuration); // Attendre pendant la durée de la pause
        toleft = !toleft; // Inverser la direction
        isPaused = false; // Désactiver la pause
    }
}