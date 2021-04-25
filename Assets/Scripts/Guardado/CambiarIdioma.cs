using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CambiarIdioma : MonoBehaviour
{
    [Header("0: ESPAÑOL, 1: INGLES")]
    [SerializeField] string[] idiomas;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = idiomas[SistemaGuardado.indiceIdioma];
    }
}
