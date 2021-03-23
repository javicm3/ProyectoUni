using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiarIdioma : MonoBehaviour
{
    [Header("0: ESPAÑOL, 1: INGLES")]
    [SerializeField] string[] idiomas;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = idiomas[SistemaGuardado.indiceIdioma];
    }
}
