using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderMenu : HUDObject
{
    TextMeshProUGUI textMesh;
    Slider slider;

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        slider = GetComponentInChildren<Slider>();
    }

    public override void Select()
    {
        textMesh.fontSharedMaterial = selectMat;
    }

    public override void Diselect()
    {
        textMesh.fontSharedMaterial = startMat;
    }

    public override void Slide(float value)
    {
        slider.value += value * Time.deltaTime;
    }
}
