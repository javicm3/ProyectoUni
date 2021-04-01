﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotonMenu1 : HUDObject
{
    Button button;
    Outline outline;
    TextMeshProUGUI textMesh;

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
        outline = GetComponent<Outline>();
    }

    public override void Select(Material mat)
    {
        textMesh.fontSharedMaterial = mat;
        outline.enabled = true;
    }

    public override void Diselect(Material mat)
    {
        textMesh.fontSharedMaterial = mat;
        outline.enabled = false;

    }

    public override void Use()
    {
        button.onClick.Invoke();
    }


}
