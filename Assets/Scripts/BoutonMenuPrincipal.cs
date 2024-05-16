using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Menu");
    }
}
