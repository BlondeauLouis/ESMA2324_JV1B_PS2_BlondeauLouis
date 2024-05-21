using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2.0f; // Vitesse de la plateforme
    public float height = 3.0f; // Hauteur maximale du mouvement
    public float pauseDuration = 1.0f; // Durée de la pause en secondes

    private Vector3 startPosition;
    private bool movingUp = true;
    private bool isPaused = false;

    void Start()
    {
        startPosition = transform.position; // Sauvegarder la position de départ
    }

    void Update()
    {
        if (!isPaused)
        {
            // Calculer la nouvelle position
            float newY = transform.position.y + (movingUp ? speed : -speed) * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            // Inverser le sens du mouvement si la plateforme atteint la hauteur maximale ou minimale
            if (movingUp && transform.position.y >= startPosition.y + height)
            {
                StartCoroutine(PauseAndChangeDirection());
            }
            else if (!movingUp && transform.position.y <= startPosition.y)
            {
                StartCoroutine(PauseAndChangeDirection());
            }
        }
    }

    private IEnumerator PauseAndChangeDirection()
    {
        isPaused = true; // Activer la pause
        yield return new WaitForSeconds(pauseDuration); // Attendre pendant la durée de la pause
        movingUp = !movingUp; // Inverser la direction
        isPaused = false; // Désactiver la pause
    }
}
