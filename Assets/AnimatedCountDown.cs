using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedCountDown : MonoBehaviour
{
    public Text text;
    public float delay;
    private int count;
    private float lastCountdownTime;
    private float alpha;

    public void CountDownFrom(int value)
    {
        this.count = value;
        this.lastCountdownTime = Time.time;
        this.text.text = this.count.ToString();
        SetTextAlpha(1.0f);
        StartCoroutine(CountdownCoroutine());
    }

    void Update()
    {
        if(this.alpha > 0)
        {
            SetTextAlpha(Mathf.Lerp(1, 0, (Time.time - lastCountdownTime) / this.delay));
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        while(this.count > 1)
        {
            yield return new WaitForSeconds(this.delay);
            SetTextAlpha(1.0f);
            --this.count;
            this.text.text = this.count.ToString();
            this.lastCountdownTime = Time.time;
        }
    }

    private void SetTextAlpha(float alpha)
    {
        this.alpha = alpha;
        Color color = this.text.color;
        color.a = alpha;
        this.text.color = color;
    }
}
