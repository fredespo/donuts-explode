using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreBonus : MonoBehaviour
{
    public AudioSource bonusSound;
    private Text text;
    private int bonus = 0;
    private Score score;
    private Animator anim;
    private Action onFinishedApplying;

    void Awake()
    {
        this.text = GetComponent<Text>();
        this.anim = GetComponent<Animator>();
    }

    void Start()
    {
        this.score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
    }

    public void AddToScoreWithAnimationAndThen(Action andThen)
    {
        this.anim.SetTrigger("Apply");
        this.onFinishedApplying = andThen;
    }
    
    public void AddBonus(int value)
    {
        this.bonus += value;
        RefreshText();
        this.anim.SetTrigger("Increase");
        this.bonusSound.Play();
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
    
    public void OnFinsihedApplying()
    {
        if(this.onFinishedApplying != null)
        {
            this.onFinishedApplying.Invoke();
            this.onFinishedApplying = null;
        }
    }
}
