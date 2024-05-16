using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsButton : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Paramètres");
    }
}
