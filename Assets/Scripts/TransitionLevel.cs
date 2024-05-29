using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLevel : MonoBehaviour
{
    public string nextSceneName;
    public float transitionDelay = 3f;

    void Start()
    {
        Invoke("LoadNextScene", transitionDelay);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
