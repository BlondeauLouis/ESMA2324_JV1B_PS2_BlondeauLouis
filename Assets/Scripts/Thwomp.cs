using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thwomp : MonoBehaviour
{
    public bool isActif = false;
    private Rigidbody2D rb;

    public Transform spawn;
    public float resetTime = 2f;
    public GameObject thwompRange;
    public float fallSpeedMultiplier = 2f;

    private PlayerController playerController;

    private float originalGravityScale;

    private Vector2 originalPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        originalPosition = transform.position;
        originalPosition = transform.position;
        originalGravityScale = rb.gravityScale;
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
        rb.gravityScale = originalGravityScale * fallSpeedMultiplier;
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
        rb.gravityScale = originalGravityScale;
        transform.position = originalPosition;
        rb.velocity = Vector2.zero;
        isActif = false;
        if (thwompRange != null)
        {
            thwompRange.SetActive(true);
        }
    }
}
