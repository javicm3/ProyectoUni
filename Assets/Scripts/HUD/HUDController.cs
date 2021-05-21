using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.EventSystems;
public class HUDController : MonoBehaviour
{
    InputDevice joystick;
    public bool isController=false;
    HUDObject selected;
    int index=0;

    [SerializeField] HUDObject[] item;
    public Material normal;
    public Material select;

    [SerializeField] bool horizontal;


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
        if (isController&&joystick != null )
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

            if (!horizontal)
            {
                if (joystick.LeftStickY.WasPressed)
                {
                    if (Input.GetAxis("Vertical") > 0.05f && index - 1 >= 0)
                    { index--; }
                    else if (Input.GetAxis("Vertical") < -0.05f && index + 1 < item.Length) { index++; print("nav" +index); }

                    SelectItem();
                }
                else if (joystick.DPadUp.WasPressed && index - 1 >= 0)
                {
                    index--;
                    SelectItem();
                }
                else if (joystick.DPadDown.WasPressed && index + 1 < item.Length)
                {
                    index++;
                    SelectItem();
                    
                }


                if (joystick.LeftStickX.IsPressed && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
                {
                    selected.Slide(Input.GetAxis("Horizontal"));
                }
                else if (joystick.DPadLeft.IsPressed)
                {
                    selected.Slide(-1);
                }
                else if (joystick.DPadRight.IsPressed)
                {
                    selected.Slide(1);
                }
            }
            else
            {
                if (joystick.LeftStickX.WasPressed)
                {
                    if (Input.GetAxis("Horizontal") < 0.05f && index - 1 >= 0)
                    { index--; }
                    else if (Input.GetAxis("Horizontal") > -0.05f && index + 1 < item.Length) { index++; }

                    SelectItem();
                }
                else if (joystick.DPadLeft.WasPressed && index - 1 >= 0)
                {
                    index--;
                    SelectItem();
                }
                else if (joystick.DPadRight.WasPressed && index + 1 < item.Length)
                {
                    index++;
                    SelectItem();
                }
            }

            if (joystick.Action1.WasPressed)
            {
                print(index);
                print(selected.name);
                selected.Use();
            }


        }

        CheckController();

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
        if (!isController)
        {
            joystick = InputManager.ActiveDevice;
            if (joystick != null && joystick.Name != "NullInputDevice" && joystick.Name != "Unknown Device")
            {
               
                isController = true;
                selected = item[index];
                selected.Select();
                Cursor.visible = false;
            }            
        }
        else
        {
            joystick = InputManager.ActiveDevice;
            if (joystick == null || joystick.Name == "NullInputDevice" || joystick.Name == "Unknown Device")
            {
                isController = false;
                selected.Diselect();
                Cursor.visible = true;
            }            
        }

    }
}
