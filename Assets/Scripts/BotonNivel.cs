using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonNivel : MonoBehaviour
{ public float nivelesnecesarios = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.nivelescompletados >= nivelesnecesarios)
        {
            this.GetComponent<Image>().color = Color.white;
            this.GetComponent<Button>().interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
