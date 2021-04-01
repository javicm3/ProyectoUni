using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    bool isController=false;
    HUDObject selected;
    int index=0;

    [SerializeField] HUDObject[] item;
    [SerializeField] Material normal;
    [SerializeField] Material select;


    // Start is called before the first frame update
    void Start()
    {
        CheckController();
    }

    // Update is called once per frame
    void Update()
    {
        //Metodo para detectar si hay mando?

        if (isController)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow)&& index+1<item.Length)
            {
                print(":(");
                index++;
                SelectItem();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && index-1>=0)
            {
                index--;
                SelectItem();
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                print("use");
                selected.Use();
            }

            if (Input.GetAxis("Horizontal")!=0)
            {
                selected.Slide(Input.GetAxis("Horizontal"));
            }

            //Meter input sliders
        }
    }

    void SelectItem()
    {
        selected.Diselect(normal);
        selected = item[index];
        selected.Select(select);
    }

    void CheckController()
    {
        if(true)
        {
            isController = true;
            selected = item[index];
            selected.Select(select);
        }
    }
}
