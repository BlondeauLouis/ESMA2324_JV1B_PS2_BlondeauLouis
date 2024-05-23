using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public bool toleft = true;
    public float speed;

    private Rigidbody2D rgbd;
    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("LeftLimit"))
        {
            toleft = false;
        }

        if (collision.CompareTag("RightLimit"))
        {
            toleft = true;
        }
    }
}
