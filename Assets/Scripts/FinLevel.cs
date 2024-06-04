using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinLevel : MonoBehaviour
{
    public float transitionDelay = 2f;

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(End());
        }
    }

    void QuitLevel()
    {
        GameObject LevelsUnlocked = GameObject.FindWithTag("LevelsUnlocked");
        LevelsUnlocked.SendMessage("LevelCompleted");
        SceneManager.LoadScene("StageCleared");
    }

    IEnumerator End()
    {
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / transitionDelay;
            yield return null;
        }
        QuitLevel();
    }
}
