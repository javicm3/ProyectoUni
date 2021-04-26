using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscensorBoss2 : MonoBehaviour
{
    public float speed;
    public Transform pos2;
    public Transform startPos;
    public float tiempoParada = 2;
    public float auxtiempoParada;
    public GameObject player;
    bool tocandoPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = startPos.position;
        auxtiempoParada = tiempoParada;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position != startPos.position && player.transform.position.y < this.transform.position.y && tocandoPlayer == false)
        {
            //nextPos = pos2.position;
            transform.position = Vector3.MoveTowards(transform.position, startPos.position, speed * Time.deltaTime);

        }
        else if (this.transform.position != pos2.position )
        {
            transform.position = Vector3.MoveTowards(transform.position, pos2.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.parent = FindObjectOfType<ControllerPersonaje>().gameObject.transform)
            {
                collision.gameObject.transform.parent = this.transform;
            }
            tocandoPlayer = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.parent == this.transform)
            {
                collision.gameObject.transform.parent = null;
            }
            tocandoPlayer = false;
        }
    }

}
