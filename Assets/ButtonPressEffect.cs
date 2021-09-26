using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonPressEffect : MonoBehaviour
{
    public float scaleEffect = 0.95f;
    public float durationFrames = 3;
    private Vector3 from;
    private Vector3 to;
    private RectTransform rectTransform;

    void Awake()
    {
        this.rectTransform = GetComponent<RectTransform>();
        this.from = this.rectTransform.localScale;
        this.to = this.from * this.scaleEffect;
        GetComponent<Button>().onClick.AddListener(() =>
            StartCoroutine(Animate())
        );
    }

    void OnEnable()
    {
        this.rectTransform.localScale = from;
    }

    IEnumerator Animate()
    {
        this.rectTransform.localScale = to;
        yield return new WaitForSeconds(Time.fixedDeltaTime * durationFrames);
        this.rectTransform.localScale = from;
    }
}