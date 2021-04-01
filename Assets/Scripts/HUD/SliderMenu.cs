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

    public override void Select(Material mat)
    {
        textMesh.fontSharedMaterial = mat;
    }

    public override void Diselect(Material mat)
    {
        textMesh.fontSharedMaterial = mat;

    }
}
