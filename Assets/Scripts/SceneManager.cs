using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public string LevelToLoad;

    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelToLoad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LoadLevel();
    }
}
