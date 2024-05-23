using UnityEngine;

public class FlicCam : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rgbd;
    private Collider2D enemyCollider;
    private PlayerController playerController;

    private Transform playerTransform;
    private float initialY;

    public bool toleft = true;
    public bool isActif = false;

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        initialY = transform.position.y;
    }

    void Update()
    {
        if (isActif == true)
        {
            Actif();
        }
        else if (isActif == false)
        {
            Passif();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActif && other.CompareTag("LeftLimit"))
        {
            toleft = false;
        }

        if (!isActif && other.CompareTag("RightLimit"))
        {
            toleft = true;
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
        if (toleft)
        {
            rgbd.velocity = new Vector2(-speed, rgbd.velocity.y);
        }
        else
        {
            rgbd.velocity = new Vector2(speed, rgbd.velocity.y);
        }
    }
}
