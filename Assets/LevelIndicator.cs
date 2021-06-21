using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIndicator : MonoBehaviour
{
    public Text txt;
    
    public void Set(int currLevel, int levelCount)
    {
        this.txt.text = currLevel + "/" + levelCount;
        
    }
}
