using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusLevelWinUI : MonoBehaviour
{
    public Animator anim;
    public GameObject buttons;
    public Text numDefuzedMsg;
    public Text bonusPoints;

    public void Reveal(int numDefuzed, int numBonusPoints)
    {
        this.numDefuzedMsg.text = numDefuzed + " defuzed";
        this.bonusPoints.text = numBonusPoints + "";
        this.anim.SetTrigger("show");
    }

    public void AwardBonusPoints()
    {
        Debug.Log("Awarding bonus points...");
        StartCoroutine(ShowButtonsAfterDelay(2.0f));
    }

    private IEnumerator ShowButtonsAfterDelay(float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        this.buttons.SetActive(true);
    }
}
