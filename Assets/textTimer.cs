using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textTimer : MonoBehaviour
{
    public float seconds = 10;
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        seconds -= Time.deltaTime;
        if (seconds < 0) seconds = 0;
        text.text = seconds.ToString("00.00").Replace(".", ":");
    }
}
