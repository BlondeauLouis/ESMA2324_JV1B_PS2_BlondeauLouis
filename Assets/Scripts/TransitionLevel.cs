using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public string nextSceneName; // Le nom de la prochaine sc�ne � charger
    public float transitionDelay = 3f; // D�lai en secondes avant de passer � la prochaine sc�ne

    private Renderer transitionRenderer;
    private Color originalColor;

    void Start()
    {
        transitionRenderer = GetComponentInChildren<Renderer>();
        originalColor = transitionRenderer.material.color;

        // D�marrez la coroutine de fondu au noir
        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / transitionDelay;
            Color newColor = Color.Lerp(originalColor, Color.black, t);
            newColor.a = Mathf.Clamp01(newColor.a); // Assure que l'alpha reste entre 0 et 1
            transitionRenderer.material.color = newColor;
            yield return null;
        }

        // Charger la prochaine sc�ne
        SceneManager.LoadScene(nextSceneName);
    }
}
