using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonMenu : MonoBehaviour
{
    Vector2 size;
    Vector2 size1;
    Vector2 size2;

    RectTransform rt;

    [SerializeField] float increaseSpeed = 50;
    [SerializeField] float decreaseSpeed = 25;
    [SerializeField] float addWide = 100;

    enum action { nothing, increase, decrease, }
    action doing = action.nothing;

    void Start()
    {
        rt = GetComponent<RectTransform>();
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

    public void Select()
    {
        doing = action.increase;
    }
    public void Diselect()
    {
        doing = action.decrease;
    }
}
