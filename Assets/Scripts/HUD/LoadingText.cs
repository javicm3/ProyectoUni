using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingText : MonoBehaviour
{
    string texto;
    TextMeshProUGUI cargando;

    int index = 0;

    void Start()
    {
        cargando = GetComponent<TextMeshProUGUI>();

        if (SistemaGuardado.indiceIdioma == 0)
        {
            texto = "Cargando";
        }
        else { texto = "Loading"; }

        cargando.text = texto;
        StartCoroutine(ChangeText());
    }

    IEnumerator ChangeText()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.8f);
            index++;
            switch (index)
            {
                case 0:
                    cargando.text = texto;
                    break;

                case 1:
                    cargando.text = texto+".";
                    break;

                case 2:
                    cargando.text = texto + "..";
                    break;

                case 3:
                    cargando.text = texto + "...";
                    index = -1;
                    break;

            }

        }

    }
}
