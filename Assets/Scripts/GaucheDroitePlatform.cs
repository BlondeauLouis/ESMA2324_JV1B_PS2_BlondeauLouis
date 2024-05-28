using UnityEngine;
using System.Collections;

public class MovingPlatformGD : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private bool movingToB = true;
    public float pauseDuration = 1.0f; // Durée de la pause en secondes
    private bool isPaused = false;

    void Update()
    {
        if (!isPaused)
        {
            if (movingToB)
            {
                transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, pointB.position) < 0.1f)
                {
                    StartCoroutine(PauseAndChangeDirection());
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, pointA.position) < 0.1f)
                {
                    StartCoroutine(PauseAndChangeDirection());
                }
            }
        }
        
    }

    private IEnumerator PauseAndChangeDirection()
    {
        isPaused = true; // Activer la pause
        yield return new WaitForSeconds(pauseDuration); // Attendre pendant la durée de la pause
        movingToB = !movingToB; // Inverser la direction
        isPaused = false; // Désactiver la pause
    }
}
