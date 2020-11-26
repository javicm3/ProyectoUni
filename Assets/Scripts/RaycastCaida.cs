using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCaida : MonoBehaviour
{
    public Animator anim;
    public LayerMask suelo;
    ControllerPersonaje controller;
    public float distanciaSuelo;
    public float distanciaSueloDash;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ControllerPersonaje>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector2(controller.rb.velocity.x * 0.5f, controller.rb.velocity.y), distanciaSuelo, suelo); ;
        Debug.DrawRay(this.transform.position, new Vector2(controller.rb.velocity.x * 0.5f, controller.rb.velocity.y) * 14, Color.green);
        if (hit.collider != null /*&& hit.collider.gameObject.layer == suelo*/)
        {
            //print(hit.collider.name + " coliflor");
           //print(Vector2.Distance(hit.point, transform.position));
            if (Vector2.Distance(hit.point, transform.position) <= distanciaSuelo && controller.rb.velocity.y < 0 && !controller.dashEnCaida)
            {

                anim.SetBool("RaycastCaida", true);
                //print("raycastcaida");
            }
            else if (controller.rb.velocity.y > 0)
            {
                anim.SetBool("RaycastCaida", false);
            }
            else if (Vector2.Distance(hit.point, transform.position) > distanciaSuelo)
            {
                anim.SetBool("RaycastCaida", false);
            }
            if (Vector2.Distance(hit.point, transform.position) <= 3f && controller.grounded == false && controller.rb.velocity.y < 0 && !controller.dashEnCaida) /*&& controller.rb.velocity.y < 0*/
            {
            
                this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().caidaSuelo);
            }
        }
        else
        {
            anim.SetBool("RaycastCaida", false);
        }
        RaycastHit2D hit2 = Physics2D.Raycast(this.transform.position, Vector2.down, distanciaSueloDash, suelo); ;
        //Debug.DrawRay(this.transform.position, new Vector2(controller.rb.velocity.x * 0.5f, controller.rb.velocity.y) * 14, Color.green);
        if (hit2.collider != null /*&& hit.collider.gameObject.layer == suelo*/&& controller.dashEnCaida)
        {
            //print(hit.collider.name + " coliflor");
            //print(Vector2.Distance(hit.point, transform.position));
            if (Vector2.Distance(hit2.point, transform.position) <= distanciaSueloDash /*&& controller.rb.velocity.y < 0*/)
            {

                anim.SetBool("RaycastCaidaDash", true);
                //print("raycastcaida");
            }
            else if (controller.rb.velocity.y > 0)
            {
                anim.SetBool("RaycastCaidaDash", false);
            }
            else if (Vector2.Distance(hit2.point, transform.position) > distanciaSueloDash)
            {
                anim.SetBool("RaycastCaidaDash", false);
            }
            if (Vector2.Distance(hit2.point, transform.position) <= 3f && controller.grounded==false && controller.rb.velocity.y<0&&controller.dashEnCaida) /*&& controller.rb.velocity.y < 0*/
            {
           
                this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().caidaDashAbajo);
            }
        }
        else
        {
            anim.SetBool("RaycastCaidaDash", false);
        }
    }

}
