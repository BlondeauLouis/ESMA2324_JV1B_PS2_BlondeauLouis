using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerLives = 3;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoseLife()
    {
        playerLives--;
        if (playerLives <= 0)
        {
            SceneManager.LoadScene("Game Over");
            playerLives = 3;
        }
    }

    public void GainLife()
    {
        playerLives++;
    }
}