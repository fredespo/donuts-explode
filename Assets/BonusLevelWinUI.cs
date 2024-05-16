using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusLevelWinUI : MonoBehaviour
{
    public Animator anim;
    public GameObject buttons;
    public Text numDefuzedMsg;
    public Text bonusPointsText;
    public AudioSource impactSound;
    private int bonusPoints;
    private ScoreBonus scoreBonus;

    void Start()
    {
        this.scoreBonus = GameObject.FindWithTag("ScoreBonus").GetComponent<ScoreBonus>();
    }

    public void RevealAndAwardBonus(int numDefuzed, int numBonusPoints)
    {
        if (numDefuzed > 0)
        {
            this.numDefuzedMsg.text = numDefuzed + " bonus donut" + (numDefuzed == 1 ? "" : "s");
        }
        else
        {
            this.numDefuzedMsg.text = "No bonus donuts";
        }
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

    public void DeactivateAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void PlayImpactSound()
    {
        this.impactSound.Play();
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
