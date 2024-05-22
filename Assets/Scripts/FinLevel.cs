using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinLevel : MonoBehaviour
{

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject LevelsUnlocked = GameObject.FindWithTag("LevelsUnlocked");
            LevelsUnlocked.SendMessage("LevelCompleted");
            SceneManager.LoadScene("Choix Niveau");
        }
    }
}
