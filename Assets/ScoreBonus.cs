using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBonus : MonoBehaviour
{
    private Text text;
    private int bonus = 0;

    void Awake()
    {
        this.text = GetComponent<Text>();
    }
    
    public void AddBonus(int value)
    {
        this.bonus += value;
        RefreshText();
    }

    private void RefreshText()
    {
        if (this.bonus > 0)
        {
            this.text.text = "+" + this.bonus;
        }
        else
        {
            this.text.text = "";
        }
    }

    public int GetBonus()
    {
        return this.bonus;
    }

    public void Reset()
    {
        this.bonus = 0;
        RefreshText();
    }

    public void SetTextAlpha(float alpha)
    {
        Color color = this.text.color;
        color.a = alpha;
        this.text.color = color;
    }
}
