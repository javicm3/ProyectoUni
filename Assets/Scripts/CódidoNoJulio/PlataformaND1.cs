using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaND1 : MonoBehaviour
{

    public float speed;
    public Transform pos1, pos2;
    public Transform startPos;
    public float tiempoParada = 2;
    public float auxtiempoParada;
    public bool vuelta = false;
    public bool verticales = false;
    public Vector3 nextPos;
    GameObject player;
    public bool tocando;
    float aux = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;
        auxtiempoParada = tiempoParada;
        player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (tocando)
        {


            //player.GetComponent<Rigidbody2D>().velocity = new Vector3(player.GetComponent<Rigidbody2D>().velocity.x + speed*x*Time.fixedDeltaTime, player.GetComponent<Rigidbody2D>().velocity.y + speed*y* Time.fixedDeltaTime, 0);
            if (player.GetComponent<ControllerPersonaje>().auxCdDashReal <=0.2f&& (player.GetComponent<PlayerInput>().inputHorizontal==0))
            {
                aux = 0.3f;
                if (nextPos == pos1.position&&Vector2.Distance(player.transform.position,nextPos)>15)
                {
                    player.transform.position = Vector2.MoveTowards(player.transform.position, nextPos, ((0.1f * player.GetComponent<Rigidbody2D>().velocity.x) + speed) * Time.deltaTime);

                }
                //player.GetComponent<Rigidbody2D>().velocity = player.GetComponent<Rigidbody2D>().velocity + speed * Time.fixedDeltaTime * new Vector2((nextPos.x - player.transform.position.x), 0).normalized;
            }
            else if ((Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x )<5.5f))
            {
                print(Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x) + "speedx");
                //aux -= Time.deltaTime;
                //if (aux > 0)
                //{
                if (nextPos == pos1.position && Vector2.Distance(player.transform.position, nextPos) > 15)
                {
                    player.transform.position = Vector2.MoveTowards(player.transform.position, nextPos, ((0.1f * player.GetComponent<Rigidbody2D>().velocity.x)+ speed) * Time.deltaTime);

                }
                //}

            }
            //else if ((Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x) < 11.5f))
            //{
            //    print(Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x) + "speedx");
            //    //aux -= Time.deltaTime;
            //    //if (aux > 0)
            //    //{
            //    if (nextPos == pos1.position && Vector2.Distance(player.transform.position, nextPos) > 10)
            //    {
            //        player.transform.position = Vector2.MoveTowards(player.transform.position, nextPos, ((0.2f * player.GetComponent<Rigidbody2D>().velocity.x) + speed) * Time.deltaTime);

            //    }
            //    //}

            //}



        }
        if (verticales)
        {
            auxtiempoParada -= Time.deltaTime;
            if ((auxtiempoParada <= 0) && (vuelta = false))
            {
                nextPos = pos1.position;
                vuelta = true;
            }
        }
        if (FindObjectOfType<VidaPlayer>().reiniciando)
        {



            tocando = false;
            vuelta = false;
            this.transform.position = startPos.position;
            //if (gameObject.GetComponentInChildren<ControllerPersonaje>()!=null)
            //{
            //    gameObject.GetComponentInChildren<ControllerPersonaje>().gameObject.transform.parent = null;
            //}
            this.transform.gameObject.SetActive(false);
        }
        if (Vector2.Distance(this.transform.position, startPos.position) <= 0.05)
        {
            if (!verticales)
            {
                auxtiempoParada -= Time.deltaTime;
            }
            if (this.gameObject.GetComponentInChildren<FlechasPlataforma>() != null) this.gameObject.GetComponentInChildren<FlechasPlataforma>().abajo = false;
            if (auxtiempoParada <= 0)
            {
                nextPos = pos1.position;
                vuelta = true;
            }
        }

        if ((Vector2.Distance(this.transform.position, pos1.position) <= 0.05))
        {
            if (!verticales)
            {


                //nextPos = pos2.position;
                if (vuelta == true)
                {
                    nextPos = pos2.position;
                    if (this.gameObject.GetComponentInChildren<FlechasPlataforma>() != null) this.gameObject.GetComponentInChildren<FlechasPlataforma>().abajo = true;
                    //if (FindObjectOfType<ControllerPersonaje>().gameObject.transform.parent != null) FindObjectOfType<ControllerPersonaje>().gameObject.transform.parent = null;

                    //vuelta = false;
                    //this.transform.position = startPos.position;
                    //transform.gameObject.SetActive(false);

                }
            }
            else
            {
                this.transform.position = pos1.position;
                transform.gameObject.SetActive(false);
            }
        }
        if (Vector2.Distance(this.transform.position, pos2.position) <= 0.05)
        {
            //nextPos = pos2.position;
            if (!verticales)
            {
                if (this.gameObject.GetComponentInChildren<FlechasPlataforma>() != null) this.gameObject.GetComponentInChildren<FlechasPlataforma>().abajo = false;
                if (vuelta == true)
                {
                    nextPos = pos2.position;
                    //if (FindObjectOfType<ControllerPersonaje>().gameObject.transform.parent != null) FindObjectOfType<ControllerPersonaje>().gameObject.transform.parent = null;

                    vuelta = false;
                    this.transform.position = startPos.position;
                    transform.gameObject.SetActive(false);

                }
            }
            else
            {
                if (vuelta == true)
                {
                    nextPos = pos1.position;


                    vuelta = false;
                    this.transform.position = startPos.position;


                }
            }

        }
     
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OndDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }

    public IEnumerator Scuttle()
    {
        yield return new WaitForSeconds(tiempoParada);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.parent = FindObjectOfType<ControllerPersonaje>().gameObject.transform)
            {
                //collision.gameObject.transform.parent = this.transform;
                tocando = true;
            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.parent = FindObjectOfType<ControllerPersonaje>().gameObject.transform)
            {
                //collision.gameObject.transform.parent = this.transform;
                tocando = true;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tocando = false;
            //if (collision.gameObject.transform.parent == this.transform)
            //{
            //    collision.gameObject.transform.parent = null;

            //}
        }
    }
}
