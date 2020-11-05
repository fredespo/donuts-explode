using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject canvas;
    public textTimer timer;
    public List<Level> levels;

    public void Start()
    {
        LoadLevel(0);
    }

    private void LoadLevel(int levelIndex)
    {
        Level level = levels[levelIndex];
        GameObject bomb = Instantiate(level.bomb);
        bomb.transform.SetParent(canvas.transform, false);
        timer.setTime(level.secondsOnTimer);
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    [System.Serializable]
    public class Level
    {
        public GameObject bomb;
        public float secondsOnTimer;
    }
}
