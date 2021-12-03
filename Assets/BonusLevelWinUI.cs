using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusLevelWinUI : MonoBehaviour
{
    public Animator anim;
    public BonusBombs bonusBombs;
    public GameObject buttons;
    public Text numDefuzedMsg;
    public Text bonusPointsText;
    public ScoreBonus scoreBonus;
    private int bonusPoints;

    public void RevealAndAwardBonus(int numDefuzed, int numBonusPoints)
    {
        this.numDefuzedMsg.text = numDefuzed + " bonus bomb" + (numDefuzed == 1 ? "" : "s");
        this.bonusPointsText.text = numBonusPoints + "";
        this.anim.SetTrigger("show");
        this.bonusPoints = numBonusPoints;
    }

    public void ShowScoreBonus()
    {
        this.scoreBonus.AddBonus(this.bonusPoints);
    }

    public void AwardBonusPointsThenShowButtons()
    {
        this.scoreBonus.AddToScoreWithAnimationAndThen(() => ShowButtons());
    }

    public void ShowButtons()
    {
        this.buttons.SetActive(true);
    }
}
