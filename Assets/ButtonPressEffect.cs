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

    void Start()
    {
        this.from = GetComponent<RectTransform>().localScale;
        this.to = this.from * this.scaleEffect;
        GetComponent<Button>().onClick.AddListener(() =>
            StartCoroutine(Animate())
        );
    }

    IEnumerator Animate()
    {
        GetComponent<RectTransform>().localScale = to;
        yield return new WaitForSeconds(Time.fixedDeltaTime * durationFrames);
        GetComponent<RectTransform>().localScale = from;
    }
}