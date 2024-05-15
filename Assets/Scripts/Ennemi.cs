using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour
{
    public bool toleft = true;
    public float speed;
    public Rigidbody2D rgbd;

    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toleft)
        {

            rgbd.velocity = new Vector2(-speed, rgbd.velocity.y);

        }

        else if (!toleft)
        {

            rgbd.velocity = new Vector2(speed, rgbd.velocity.y);

        if (playerController.isInvisible)
            {
                rgbd.isKinematic = true;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LeftLimit"))
        {
            toleft = false;

        }

        if (other.CompareTag("RightLimit"))
        {
            toleft = true;

        }


    }
}
