using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxAbsorb : MonoBehaviour
{
   EnemigoAbsorb script;

    private void Awake()
    {
        script = GetComponentInParent<EnemigoAbsorb>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        script.TriggerStay2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        script.TriggerExit2D(collision);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        script.TriggerEnter2D(collision);
    }

}
