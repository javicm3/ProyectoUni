using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    public float inputHorizontal;
    public float ultimoInputHorizontal;
    public float inputVertical;
    public Transform particulasVelMax;
    //public bool puedoSaltar = true;
    public bool personajeInvertido = false;
    public bool inputHorizBlock = false;
    public bool inputVerticBlock = false;
    ControllerPersonaje cp;
    Vector2 originalScale;
    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        cp = this.GetComponent<ControllerPersonaje>();
        originalScale = transform.Find("Cuerpo").localScale;
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
                    transform.Find("Cuerpo").localScale = originalScale;
                    if (personajeInvertido)
                    {


                        particulasVelMax.localScale *= new Vector2(-1, 1);
                        //transform.Find("Cuerpo").localScale *= new Vector2(-1, 1);
                        //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);

                        personajeInvertido = false;

                    }
                }
                else if (ultimoInputHorizontal < 0)
                {

                    transform.Find("Cuerpo").localScale = new Vector2(-originalScale.x, originalScale.y);
                    if (personajeInvertido == false)
                    {

                        particulasVelMax.localScale *= new Vector2(-1, 1);
                        //transform.Find("Cuerpo").localScale *= new Vector2(-1, 1);
                        //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                        personajeInvertido = true;


                    }



                }
            }
            else if (inputHorizontal == -1 && personajeInvertido == false && (Mathf.Sign(cp.ultimaNormal.y) > 0) && (cp.grounded)&& !cp.looping)
            {
                particulasVelMax.localScale = new Vector2(-1, 1);
                transform.Find("Cuerpo").localScale *= new Vector2(-1, 1);
                //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                personajeInvertido = true;

            }

            else if (inputHorizontal == 1 && personajeInvertido == true && (Mathf.Sign(cp.ultimaNormal.y) > 0) && (cp.grounded) && !cp.looping)
            {
                particulasVelMax.localScale = new Vector2(-1, 1);
                transform.Find("Cuerpo").localScale *= new Vector2(-1, 1);
                //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);

                personajeInvertido = false;
            }
            else if (inputHorizontal == 1 && personajeInvertido == false && (Mathf.Sign(cp.ultimaNormal.y) < 0) && (!cp.grounded))
            {
                if (rb.velocity.x > 0.2f)
                {
                    particulasVelMax.localScale = new Vector2(1, 1);
                    transform.Find("Cuerpo").localScale = new Vector2(1, 1);
                    //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                    personajeInvertido = false;
                }
                else if (rb.velocity.x < 0.2f)
                {
                    particulasVelMax.localScale = new Vector2(-1, 1);
                    transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
                    //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                    personajeInvertido = true;
                }

            }
            else if (inputHorizontal == 1 && personajeInvertido == true && (Mathf.Sign(cp.ultimaNormal.y) < 0) && (!cp.grounded))
            {
                if (rb.velocity.x > 0.2f)
                {
                    particulasVelMax.localScale = new Vector2(1, 1);
                    transform.Find("Cuerpo").localScale = new Vector2(1, 1);
                    //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                    personajeInvertido = false;
                }
                else if (rb.velocity.x < 0.2f)
                {
                    particulasVelMax.localScale = new Vector2(-1, 1);
                    transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
                    //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                    personajeInvertido = true;
                }
                //particulasVelMax.localScale = new Vector2(1, 1);
                //transform.Find("Cuerpo").localScale = new Vector2(1, 1);
                ////GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);

                //personajeInvertido = false;
            }
            else if (inputHorizontal == -1 && personajeInvertido == true && (Mathf.Sign(cp.ultimaNormal.y) < 0) && (!cp.grounded))
            {
                if (rb.velocity.x > 0.2f)
                {
                    particulasVelMax.localScale = new Vector2(1, 1);
                    transform.Find("Cuerpo").localScale = new Vector2(1, 1);
                    //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                    personajeInvertido = false;
                }
                else if (rb.velocity.x < 0.2f)
                {
                    particulasVelMax.localScale = new Vector2(-1, 1);
                    transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
                    //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                    personajeInvertido = true;
                }
                //particulasVelMax.localScale = new Vector2(-1, 1);
                //transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
                ////GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);

                //personajeInvertido = true;
            }

            else if (inputHorizontal == -1 && personajeInvertido == false && (Mathf.Sign(cp.ultimaNormal.y) < 0) && (!cp.grounded))
            {
                if (rb.velocity.x > 0.05f)
                {
                    particulasVelMax.localScale = new Vector2(1, 1);
                    transform.Find("Cuerpo").localScale = new Vector2(1, 1);
                    //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                    personajeInvertido = false;
                }
                else if (rb.velocity.x < 0.05f)
                {
                    particulasVelMax.localScale = new Vector2(-1, 1);
                    transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
                    //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                    personajeInvertido = true;
                }
                //particulasVelMax.localScale = new Vector2(-1, 1);
                //transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
                ////GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);

                //personajeInvertido = true;
            }
            else if ((Mathf.Sign(cp.ultimaNormal.y) < 0) && (cp.grounded))
            {
                //if (cp.ultimaNormal.y < 0.15f && cp.ultimaNormal.y > -0.15f)
                //{
                //    if (rb.velocity.y > 0.05f)
                //    {
                //        if (cp.ultimaNormal.x > 0)
                //        {
                //            particulasVelMax.localScale = new Vector2(-1, 1);
                //            transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
                //            //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                //            personajeInvertido = true;
                //        }
                //        else
                //        {
                //            particulasVelMax.localScale = new Vector2(1, 1);
                //            transform.Find("Cuerpo").localScale = new Vector2(1, 1);
                //            //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                //            personajeInvertido = false;
                //        }

                //    }
                //    else if (rb.velocity.y < 0.05f)
                //    {
                //        if (cp.ultimaNormal.x > 0)
                //        {
                //            particulasVelMax.localScale = new Vector2(1, 1);
                //            transform.Find("Cuerpo").localScale = new Vector2(1, 1);
                //            //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                //            personajeInvertido = false;
                //        }
                //        else
                //        {
                //            particulasVelMax.localScale = new Vector2(-1, 1);
                //            transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
                //            //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                //            personajeInvertido = true;
                //        }

                //    }
                //}
                //else
                if (rb.velocity.x > 0.05f)
                {
                    particulasVelMax.localScale = new Vector2(1, 1);
                    transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
                    //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                    personajeInvertido = false;
                }
                else if (rb.velocity.x < 0.05f)
                {
                    particulasVelMax.localScale = new Vector2(-1, 1);
                    transform.Find("Cuerpo").localScale = new Vector2(1, 1);
                    //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                    personajeInvertido = true;
                }
            }
            else if ((Mathf.Sign(cp.ultimaNormal.y) > 0) && (cp.grounded) && cp.looping)
            {
                if (rb.velocity.x > 0.05f)
                {
                    particulasVelMax.localScale = new Vector2(1, 1);
                    transform.Find("Cuerpo").localScale = new Vector2(1, 1);
                    //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                    personajeInvertido = false;
                }
                else if (rb.velocity.x < 0.05f)
                {
                    particulasVelMax.localScale = new Vector2(-1, 1);
                    transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
                    //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                    personajeInvertido = true;
                }
            }
            else if ((!cp.grounded) )
            {
                if (rb.velocity.x > 0.05f)
                {
                    particulasVelMax.localScale = new Vector2(1, 1);
                    transform.Find("Cuerpo").localScale = new Vector2(1, 1);
                    //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                    personajeInvertido = false;
                }
                else if (rb.velocity.x < 0.05f)
                {
                    particulasVelMax.localScale = new Vector2(-1, 1);
                    transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
                    //GetComponent<ControllerPersonaje>().VFX.transform.localScale *= new Vector2(-1, 1);
                    personajeInvertido = true;
                }
            }

            /*else if ((inputHorizontal == 0)&& (cp.lastJumpPared == true)){*/

            //    inputHorizontal = ultimoInputHorizontal;

            //}

        }
        //else if (cp.pegadoPared == true && personajeInvertido == false)
        //{
        //    transform.Find("Cuerpo").localScale *= new Vector2(-1, 1);
        //    personajeInvertido = true;
        //}
    }
    void DetectarInput()
    {
        if (inputHorizBlock == false)
        {
            //inputHorizontal =Input.GetAxis("Horizontal");
            if (cp.joystick != null)
            {
                inputHorizontal = cp.joystick.LeftStickX + Input.GetAxisRaw("Horizontal");
                //print(inputHorizontal + "horiz22");
            }
            else
            {
                inputHorizontal = Input.GetAxisRaw("Horizontal");
                //print(inputHorizontal + "horiz");
            }

            if (Mathf.Abs(inputHorizontal) <= 0.2f)
            {
                inputHorizontal = 0;
            }
            else if (inputHorizontal > 0.2f)
            {
                inputHorizontal = 1;
            }
            else if (inputHorizontal < -0.2f)
            {
                inputHorizontal = -1;
            }


            if (inputHorizontal != 0)
            {
                ultimoInputHorizontal = inputHorizontal;
            }



        }
        else
        {

            inputHorizontal = 0;
        }
        if (inputVerticBlock == false)
        {
            //inputVertical = Input.GetAxis("Vertical");
            if (cp.joystick != null)
            {
                inputVertical = cp.joystick.LeftStickY + Input.GetAxisRaw("Vertical");
            }
            else
            {
                inputVertical = Input.GetAxisRaw("Vertical");
            }

            if (inputVertical > 0.05f)
            {
                inputVertical = 1;
            }
            else if (inputVertical < -0.05f)
            {
                inputVertical = -1;
            }
            else
            {
                inputVertical = 0;
            }

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

