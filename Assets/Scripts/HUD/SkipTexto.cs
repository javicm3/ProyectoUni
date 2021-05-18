using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using TMPro;

public class SkipTexto : MonoBehaviour
{
    [SerializeField] string[] texto;

    void Start()
    {
        InputDevice joystick = InputManager.ActiveDevice;
        if (joystick != null && joystick.Name != "NullInputDevice" && joystick.Name != "Unknown Device")
        {
            GetComponent<TextMeshProUGUI>().text = "[A]  " + texto[SistemaGuardado.indiceIdioma];
        }
        else { GetComponent<TextMeshProUGUI>().text = "[ESC]  " + texto[SistemaGuardado.indiceIdioma]; }
    }


}
