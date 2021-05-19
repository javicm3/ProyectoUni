using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPuertasTexto : MonoBehaviour
{
    ControllerPersonaje personaje;
    TextoPuerta[] textos;

    bool isController;

    void Start()
    {
        personaje = FindObjectOfType<ControllerPersonaje>();

        textos = FindObjectsOfType<TextoPuerta>();

        string comando = "E ";

        if (personaje.joystick != null && personaje.joystick.Name != "NullInputDevice" && personaje.joystick.Name != "Unknown Device")
        {
            comando = "B ";
        }

        if (textos != null)
        {
            foreach (TextoPuerta item in textos)
            {
                item.ChangeTexts(comando);
            }
        }
        else Destroy(this);
    }


    void Update()
    {
        CheckController();
    }


    void CheckController()
    {
        if (!isController)
        {
            if (personaje.joystick != null && personaje.joystick.Name != "NullInputDevice" && personaje.joystick.Name != "Unknown Device")
            {
                isController = true;
                foreach (TextoPuerta item in textos)
                {
                    item.ChangeTexts("B");
                }
            }
        }
        else
        {
            if (personaje.joystick == null || personaje.joystick.Name == "NullInputDevice" || personaje.joystick.Name == "Unknown Device")
            {
                isController = false;
                foreach (TextoPuerta item in textos)
                {
                    item.ChangeTexts("E");
                }

            }
        }

    }
}
