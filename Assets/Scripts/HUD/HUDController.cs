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

    // Update is called once per frame
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

            if (joystick.LeftStickY.WasPressed && index - 1 >= 0)
            {
                if (Input.GetAxis("Vertical") > 0)
                { index--; }
                else { index++; }
                
                SelectItem();
            }


            if (joystick.Action2.WasPressed)
            {
                selected.Use();
            }

            if (joystick.LeftStickX.IsPressed)
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
