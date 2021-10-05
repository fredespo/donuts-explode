using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBonus : MonoBehaviour
{
    private Text text;
    private int bonus = 0;
    private Score score;
    private Animator anim;

    void Awake()
    {
        this.text = GetComponent<Text>();
        this.anim = GetComponent<Animator>();
    }

    void Start()
    {
        this.score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
    }
    
    public void AddBonus(int value)
    {
        this.bonus += value;
        RefreshText();
        this.anim.SetTrigger("Increase");
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
        SetTextAlpha(1);
    }

    public void SetTextAlpha(float alpha)
    {
        Color color = this.text.color;
        color.a = alpha;
        this.text.color = color;
    }

    public void AddBonusToScore()
    {
        this.score.AddInstant(this.bonus);
    }

    public void ShowWinUi()
    {
        foreach (Transform child in GameObject.FindGameObjectWithTag("WinUI").transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
