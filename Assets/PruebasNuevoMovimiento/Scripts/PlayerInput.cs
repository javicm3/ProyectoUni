using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    public float inputHorizontal;
    public float ultimoInputHorizontal;
    public float inputVertical;

    //public bool puedoSaltar = true;
    public bool personajeInvertido = false;
    public bool inputHorizBlock = false;
    public bool inputVerticBlock = false;
    ControllerPersonaje cp;
    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        cp = this.GetComponent<ControllerPersonaje>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectarInput();
        DetectarSalto();
        if (cp.pegadoPared == false)
        {
            if (rb.velocity.x < 0.2f && rb.velocity.x > -0.2f)
            {
                if (ultimoInputHorizontal > 0)
                {
                    if (personajeInvertido)
                    {
                        transform.Find("Cuerpo").localScale *= new Vector2(-1, 1);
                        //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);

                        personajeInvertido = false;
                    }
                }
                else
                {
                    if (personajeInvertido == false)
                    {
                        transform.Find("Cuerpo").localScale *= new Vector2(-1, 1);
                        //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                        personajeInvertido = true;
                    }

                }
            }
            else if (inputHorizontal ==-1 && personajeInvertido == false)
            {
                transform.Find("Cuerpo").localScale *= new Vector2(-1, 1);
                //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                personajeInvertido = true;

            }
            else if (inputHorizontal ==1 && personajeInvertido == true)
            {
                transform.Find("Cuerpo").localScale *= new Vector2(-1, 1);
                //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);

                personajeInvertido = false;
            }/*else if ((inputHorizontal == 0)&& (cp.lastJumpPared == true)){*/

            //    inputHorizontal = ultimoInputHorizontal;

            //}

        }
    }
    void DetectarInput()
    {
        if (inputHorizBlock == false)
        {
            if (inputHorizontal != 0)
            {
                ultimoInputHorizontal = inputHorizontal;
            }
            inputHorizontal = Input.GetAxisRaw("Horizontal");


        }
        else
        {

            inputHorizontal = 0;
        }
        if (inputVerticBlock == false)
        {
            inputVertical = Input.GetAxisRaw("Vertical");
        }
        else
        {
            inputVertical = 0;
        }



    }
    void DetectarSalto()
    {






    }
}
