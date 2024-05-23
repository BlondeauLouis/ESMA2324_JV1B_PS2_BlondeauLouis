using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public bool toleft = true;

    public bool to_up = true;

    private Collider2D enemyCollider;
    private PlayerController playerController;

    [SerializeField] private float speed;

    [SerializeField] private float speedb;

    public Rigidbody2D rgbd;
    // Start is called before the first frame update
    void Start()
    {
        enemyCollider = GetComponent<Collider2D>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (toleft)
        {
            if (to_up)
            {
                rgbd.velocity = new Vector2(-speed, speedb);
            }
            else if (!to_up)
            {
                rgbd.velocity = new Vector2(-speed, -speedb);
            }
        }

        else if (!toleft)
        {
            if (to_up)
            {
                rgbd.velocity = new Vector2(speed, speedb);
            }
            else if (!to_up)
            {
                rgbd.velocity = new Vector2(speed, -speedb);
            }
        }

        if (playerController.isInvisible)   //JOUEUR INVISIBLE -> PAS TOUCHER
        {
            Physics2D.IgnoreCollision(playerController.GetComponent<Collider2D>(), enemyCollider, true);
        }
        else
        {
            Physics2D.IgnoreCollision(playerController.GetComponent<Collider2D>(), enemyCollider, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LeftLimit"))
        {
            toleft = false;
            if (to_up)
            {
                to_up = false;
            }
            else if (!to_up)
            {
                to_up = true;
            }
        }

        if (other.CompareTag("RightLimit"))
        {
            toleft = true;
            if (to_up)
            {
                to_up = false;
            }
            else if (!to_up)
            {
                to_up = true;
            }
        }


        if (other.CompareTag("UpLimit"))
        {
            to_up = false;

        }


        if (other.CompareTag("DownLimit"))
        {
            to_up = true;

        }
        
        if (other.CompareTag("Player"))
        {
            playerController.PerdPv();
        }
    }
}