using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thwomp : MonoBehaviour
{
    public bool isActif = false;
    private Rigidbody2D rb;

    public Transform spawn;
    public float resetTime = 2f;

    private Vector2 originalPosition;

    private PlayerController playerController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        originalPosition = transform.position;
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (isActif)
        {
            Tomber();
        }
    }

    void Tomber()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ResetThwomp());
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.PerdPv();
        }
    }

    IEnumerator ResetThwomp()
    {
        yield return new WaitForSeconds(resetTime);
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.position = originalPosition;
        rb.velocity = Vector2.zero;
        isActif = false;
    }
}
