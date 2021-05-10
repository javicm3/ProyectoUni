using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoPuerta : MonoBehaviour
{
    enum tipoPuerta
    {nivel, zona, lobby, none }
    [SerializeField] string numero;

    Text texto;

    [SerializeField] tipoPuerta tipo;
    ControllerPersonaje personaje;

    void Awake()
    {   
        texto = GetComponent<Text>();        
    }

    private void Start()
    {
        if (FindObjectOfType<ControlPuertasTexto>()==null)
        {
            if (numero == "")
            {
                tipo = tipoPuerta.none;
            }

            this.gameObject.AddComponent<ControlPuertasTexto>();
        }
    }

    public void ChangeTexts(string comando)
    {
        int i = SistemaGuardado.indiceIdioma;

        switch (tipo)
        {
            case tipoPuerta.nivel:
                texto.text = "["+comando + "] - " + Idiomas.nivel[i] + " " + numero;
                break;

            case tipoPuerta.zona:
                texto.text = "[" + comando + "] - " + Idiomas.zona[i] + " " + numero;
                break;

            case tipoPuerta.lobby:
                texto.text = "[" + comando + "] - " + Idiomas.lobby[i];
                break;

            case tipoPuerta.none:
                texto.text = "[" + comando + "] - " + "LOBBY";
                break;
        }
    }
}
