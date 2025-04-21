using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrlOpener : MonoBehaviour
{
    public string url = "https://www.example.com"; // Replace with your URL

    public void OpenURL()
    {
        Application.OpenURL(this.url);
    }
}
