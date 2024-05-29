using UnityEngine;

public class ParasolPlatform : MonoBehaviour
{
    public Vector3 retractedScale = new Vector3(1f, 0.5f, 1f);
    public float lerpSpeed = 2f;

    private Vector3 originalScale;
    private bool isPlayerOnPlatform = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isPlayerOnPlatform)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, retractedScale, Time.deltaTime * lerpSpeed);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * lerpSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
        }
    }
}
