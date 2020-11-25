using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCaida : MonoBehaviour
{
    public Animator anim;
    public LayerMask suelo;
    ControllerPersonaje controller;
    public float distanciaSuelo;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ControllerPersonaje>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), 30, suelo);
        if (hit.collider != null && hit.collider.gameObject.layer == suelo)
        {
            if(Vector2.Distance(hit.point, transform.position) <= distanciaSuelo && controller.rb.velocity.y < 0)
            {
                anim.SetBool("RaycastCaida", true);
                print("raycastcaida");
            }
            else
            {
                anim.SetBool("RaycastCaida", false);
            }
            
        }
        else
        {
            anim.SetBool("RaycastCaida", false);
        }
    }
}
