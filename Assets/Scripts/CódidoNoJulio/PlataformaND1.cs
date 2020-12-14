using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaND1 : MonoBehaviour
{

    public float speed;
    public Transform pos1, pos2;
    public Transform startPos;
    public float tiempoParada = 2;
    public static bool vuelta = false;

    Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(transform.position == pos2.position)
        {
            StartCoroutine(Scuttle());     
        }

        if (this.transform.position == pos1.position)
        {
            //nextPos = pos2.position;
            if (vuelta == true)
            {
                nextPos = pos2.position;
                transform.parent.gameObject.SetActive(false);
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
        nextPos = pos1.position;
        vuelta = true;
    }
}
