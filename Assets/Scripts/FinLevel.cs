using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinLevel : MonoBehaviour
{
    private LevelSelector levelSelector;

    void Start()
    {
        // Rechercher l'instance de LevelSelector
        levelSelector = FindObjectOfType<LevelSelector>();
        if (levelSelector == null)
        {
            Debug.LogError("LevelSelector instance not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (levelSelector != null)
            {
                levelSelector.UnlockNextLevel();
            }
            SceneManager.LoadScene("Choix Niveau");
        }
    }
}
