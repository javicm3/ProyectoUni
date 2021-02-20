using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoLaser : MonoBehaviour
{
    public float velocidadBalas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * velocidadBalas * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }
}
