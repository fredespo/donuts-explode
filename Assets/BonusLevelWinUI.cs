using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusLevelWinUI : MonoBehaviour
{
    public Animator anim;
    public GameObject buttons;
    public Text numDefuzedMsg;

    public void Reveal(int numDefuzed)
    {
        numDefuzedMsg.text = numDefuzed + " defuzed";
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
