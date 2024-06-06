using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnStartButtonClicked()
    {
        GameObject LevelsUnlocked = GameObject.FindWithTag("LevelsUnlocked");
        LevelsUnlocked.SendMessage("LevelCompleted");
    }
}
