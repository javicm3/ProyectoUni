using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class senalgiratoria : MonoBehaviour
{
    public float velocidadMov;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void FixedUpdate()
    {
        transform.RotateAround(transform.position, transform.forward, velocidadMov * Time.deltaTime);
    }

}
