using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotonMenu : HUDObject
{
    Vector2 size;
    Vector2 size1;
    Vector2 size2;

    Outline outline;

    RectTransform rt;
    Button button;
    TextMeshProUGUI textMesh;

    [SerializeField] float increaseSpeed = 50;
    [SerializeField] float decreaseSpeed = 25;
    [SerializeField] float addWide = 100;

    enum action { nothing, increase, decrease, }
    action doing = action.nothing;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        rt = GetComponent<RectTransform>();
        button = GetComponent<Button>();
    }

    void Start()
    {
        size = rt.sizeDelta;
        size1 = rt.sizeDelta;
        size2 = new Vector2(rt.sizeDelta.x + addWide, rt.sizeDelta.y);
    }

    private void Update()
    {
        switch (doing)
        {
            case action.increase:
                size = Vector2.MoveTowards(size, size2, increaseSpeed * Time.deltaTime);
                rt.sizeDelta = size; 
                break;
            case action.decrease:
                size = Vector2.MoveTowards(size, size1, decreaseSpeed * Time.deltaTime);
                rt.sizeDelta = size; rt.sizeDelta = size; 
                break;
        }
    }

    public override void Select(Material mat)
    {
        doing = action.increase; print(mat);
        textMesh.fontSharedMaterial = mat;
        outline.enabled = true;
    }

    public override void Diselect(Material mat)
    {
        doing = action.decrease;
        textMesh.fontSharedMaterial = mat;
        outline.enabled = false;

    }

    public override void Use()
    {
        button.onClick.Invoke();
    }
}
