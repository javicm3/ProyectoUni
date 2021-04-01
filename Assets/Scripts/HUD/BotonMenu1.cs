using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotonMenu1 : HUDObject
{
    Button button;
    TextMeshProUGUI textMesh;

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
    }

    public override void Select(Material mat)
    {
        textMesh.fontSharedMaterial = mat;
    }

    public override void Diselect(Material mat)
    {
        textMesh.fontSharedMaterial = mat;

    }

    public override void Use()
    {
        button.onClick.Invoke();
    }
}
