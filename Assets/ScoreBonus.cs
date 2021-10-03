using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBonus : MonoBehaviour
{
    public PieceShooter shooter;
    public int bonusPerConsecutiveShot = 100;
    private Text text;
    private int bonus = 0;

    void Awake()
    {
        this.text = GetComponent<Text>();
    }

    void Update()
    {
        int bonus = this.bonusPerConsecutiveShot * this.shooter.GetConsecutiveGoodShots();
        if (this.bonus != bonus)
        {
            this.bonus = bonus;
            RefreshText();
        }

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

    public void SetTextAlpha(float alpha)
    {
        Color color = this.text.color;
        color.a = alpha;
        this.text.color = color;
    }
}
