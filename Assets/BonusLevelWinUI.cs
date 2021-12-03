using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelWinUI : MonoBehaviour
{
    public Animator anim;
    public GameObject buttons;

    public void Reveal()
    {
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
