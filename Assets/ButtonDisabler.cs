using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class ButtonDisabler : MonoBehaviour
{
    private Button btn;
    private Image img;
    private float origAlpha;

    void Start()
    {
        this.btn = GetComponent<Button>();
        this.img = GetComponent<Image>();
        this.origAlpha = this.img.color.a;
    }

    public void DisableButton()
    {
        this.btn.interactable = false;
        SetImageColorAlpha(this.origAlpha * 0.5f);
    }

    public void EnableButton()
    {
        this.btn.interactable = true;
        SetImageColorAlpha(this.origAlpha);
    }

    private void SetImageColorAlpha(float alpha)
    {
        Color color = this.img.color;
        color.a = alpha;
        this.img.color = color;
    }
}
