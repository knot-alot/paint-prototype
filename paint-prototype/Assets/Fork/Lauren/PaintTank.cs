using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintTank : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxFill(int fillAmount)
    {
        slider.maxValue = fillAmount;
        slider.value = fillAmount;

        fill.color = gradient.Evaluate(1.0f);
    }

    public void SetFill(int fillAmount)
    {
        slider.value = fillAmount;
        fill.color = gradient.Evaluate(slider.normalizedValue);

    }
}
