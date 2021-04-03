using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TicMenu : HUDObject
{
    TextMeshProUGUI textMesh;
    Button button;
    Outline outline;
    [SerializeField] Color newColor;
    Color startColor;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        button = GetComponent<Button>();
        textMesh = GetComponentInParent<TextMeshProUGUI>();

        startColor = outline.effectColor;
    }

    public override void Select()
    {
        outline.effectColor = newColor;
        textMesh.fontSharedMaterial = selectMat;
    }

    public override void Diselect()
    {
        outline.effectColor = startColor;
        textMesh.fontSharedMaterial = startMat;
        //textMesh.fontSharedMaterial.SetColor("_GlowColor",Color.yellow);
    }

    public override void Use()
    {
        button.onClick.Invoke();
    }
}
