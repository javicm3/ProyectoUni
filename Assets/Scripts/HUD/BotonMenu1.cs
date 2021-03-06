﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotonMenu1 : HUDObject
{
    Button button;
    Outline outline;
    TextMeshProUGUI[] textMesh;


    private void Awake()
    {
        textMesh = GetComponentsInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
        outline = GetComponent<Outline>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && outline.enabled)
        {
            button.onClick.Invoke();
        }
    }

    public override void Select()
    {
        foreach (TextMeshProUGUI item in textMesh)
        {
            item.fontSharedMaterial = selectMat;
        }
        
        outline.enabled = true;
    }

    public override void Diselect()
    {
        foreach (TextMeshProUGUI item in textMesh)
        {
            item.fontSharedMaterial = startMat;
        }

        outline.enabled = false;
    }

    public override void Use()
    {
        button.onClick.Invoke();
    }


}
