using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderMenu : HUDObject
{
    [SerializeField] Material selectedFillMat;
    [SerializeField] Material startFillMat;
    Image fill;
    TextMeshProUGUI textMesh;
    Slider slider;

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        slider = GetComponentInChildren<Slider>();
        fill = transform.Find("Slider_/Fill Area/Fill").GetComponent<Image>(); print(fill);
    }

    public override void Select()
    {
        textMesh.fontSharedMaterial = selectMat;
        fill.material = selectedFillMat;
    }

    public override void Diselect()
    {
        textMesh.fontSharedMaterial = startMat;
        fill.material = startFillMat;
    }

    public override void Slide(float value)
    {
        slider.value += value * Time.deltaTime;
    }
}
