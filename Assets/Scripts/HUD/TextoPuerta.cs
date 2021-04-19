using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoPuerta : MonoBehaviour
{
    enum tipoPuerta
    {nivel, zona, lobby }
    [SerializeField] string numero;

    Text texto;

    [SerializeField] tipoPuerta tipo;

    void Start()
    {
        texto = GetComponent<Text>();
        int i = SistemaGuardado.indiceIdioma;

        string comando = "E ";
        if (FindObjectOfType<ControllerPersonaje>().joystick != null)
        {
            comando = "X ";
        }

        switch (tipo)
        {
            case tipoPuerta.nivel:
                texto.text = comando + Idiomas.para[i] + " " + Idiomas.nivel[i] + " " + numero;
                break;

            case tipoPuerta.zona:
                texto.text = comando + Idiomas.para[i] + " " + Idiomas.zona[i] + " " + numero;
                break;

            case tipoPuerta.lobby:
                texto.text = comando + Idiomas.para[i] + " " + Idiomas.lobby[i];
                break;
        }

    }


}
