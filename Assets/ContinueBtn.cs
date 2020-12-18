using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueBtn : MonoBehaviour
{
    private LevelLoader levelLoader;

    public void Start()
    {
        levelLoader = GameObject.FindGameObjectsWithTag("LevelLoader")[0].GetComponent<LevelLoader>();
    }

    public void Continue()
    {
        levelLoader.LoadNextLevel();
    }
}
