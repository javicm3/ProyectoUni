using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class HUDController : MonoBehaviour
{
    InputDevice joystick;
    bool isController=false;
    HUDObject selected;
    int index=0;

    [SerializeField] HUDObject[] item;
    public Material normal;
    public Material select;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitCheckController());
    }

    private void OnEnable()
    {
        if (isController)
        {
            index = 0;
            selected = item[index];
            selected.Select();
        }
        else { Cursor.visible = true; } 
    }

    void Update()
    {
        if (isController)
        {
            /*if (Input.GetKeyDown(KeyCode.DownArrow) && index + 1 < item.Length)
            {
                index++;
                SelectItem();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && index - 1 >= 0)
            {
                index--;
                SelectItem();
            }*/

            if (joystick.LeftStickY.WasPressed)
            {
                if (Input.GetAxis("Vertical") > 0.05f && index - 1 >= 0)
                { index--; }
                else if(Input.GetAxis("Vertical") < -0.05f && index + 1 < item.Length) { index++; }
                
                SelectItem();
            }


            if (joystick.Action2.WasPressed)
            {
                selected.Use();
            }

            if (joystick.LeftStickX.IsPressed && Mathf.Abs(Input.GetAxis("Horizontal"))>0.1f )
            {
                selected.Slide(Input.GetAxis("Horizontal"));
            }
        }
        else
        {
            CheckController();
        }
    }

    void SelectItem()
    {
        selected.Diselect();
        selected = item[index];
        selected.Select();
    }

    public void DiselectAllItems()
    {
        foreach (HUDObject obj in item)
        {
            obj.Diselect();
        }
    }

    IEnumerator WaitCheckController()
    {
        yield return new WaitForEndOfFrame();
        CheckController();
    }

    void CheckController()
    {
        joystick = InputManager.ActiveDevice; 
        if (joystick.Name != "NullInputDevice")
        {
            isController = true;
            selected = item[index];
            selected.Select();
            Cursor.visible = false;
        }
    }
}
