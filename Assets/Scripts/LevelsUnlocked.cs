using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsUnlocked : MonoBehaviour
{
    public float unlockedLevels;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void LevelCompleted()
    {
        unlockedLevels++;
    }
}
