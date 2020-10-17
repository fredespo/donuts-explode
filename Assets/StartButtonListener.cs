using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButtonListener : MonoBehaviour
{
    public Button startButton;

    void Start()
    {
        Button btn = startButton.GetComponent<Button>();
        btn.onClick.AddListener(OnStartButtonClicked);
    }

    void OnStartButtonClicked()
    {
        SceneManager.LoadScene(1);
    }
}
