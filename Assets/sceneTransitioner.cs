using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransitioner : MonoBehaviour
{
    public GameObject obj;

    public void LoadScene()
    {
        Destroy(obj);
        SceneManager.LoadScene(1);
    }
}
