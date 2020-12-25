using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreNameField : MonoBehaviour
{
    public int maxLen = 10;

    void Start()
    {
        GetComponent<InputField>().characterLimit = maxLen;
    }
}
