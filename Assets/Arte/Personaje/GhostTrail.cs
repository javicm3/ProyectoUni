using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    public GameObject ghostPrefab;
    public GameObject ghostPrefab2;
    public float delay = 1.0f;
    float delta = 0;

    public GameObject player;
    SpriteRenderer spriteRenderer;
    public float destroyTime = 0.1f;
    public Color color;
    public Material material = null;
    public Transform posicionHueso;
    Rigidbody2D rbCmp;
    public Transform parent;
    GameObject ghostObj;
    

    float colorDesvanecerse = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        //player = GetComponent<PlayerController>();
        rbCmp = ghostPrefab.GetComponent<Rigidbody2D>();
        material = ghostPrefab.GetComponent<SpriteRenderer>().sharedMaterial;
        
    }

    // Update is called once per frame
    void Update()
    {
        colorDesvanecerse -= Time.deltaTime;
        if(colorDesvanecerse <= 0)
        {
            colorDesvanecerse = 0;
        }
        if (delta > 0)
        {
            delta -= Time.deltaTime;
        }
        else
        {
            delta = delay;
            CreateGhost();
        }
        color.a = colorDesvanecerse;
    }
    void CreateGhost()
    {

        //if(Mathf.Abs(GetComponentInParent<ControllerPersonaje>().rb.velocity.x) > 0)
        if(GetComponentInParent<ControllerPersonaje>().tengoMaxspeed)
        {
            print("ghost");

            //GameObject ghostObj = Instantiate(ghostPrefab, player.transform.position + ghostPrefab.transform.position, ghostPrefab.transform.rotation /*parent.transform*/);
            
            colorDesvanecerse = 1;
            
            if (GetComponentInParent<PlayerInput>().personajeInvertido == true)
            {
                 ghostObj = Instantiate(ghostPrefab2, player.transform.position + ghostPrefab2.transform.position, ghostPrefab2.transform.rotation /*parent.transform*/);
            }
            else
            {
                 ghostObj = Instantiate(ghostPrefab, player.transform.position + ghostPrefab.transform.position, ghostPrefab.transform.rotation /*parent.transform*/);

            }
            rbCmp.velocity = GetComponentInParent<ControllerPersonaje>().rb.velocity;
            print(rbCmp.velocity);
            //ghostObj.GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("Vector1_22FA4E47", colorDesvanecerse);
            //ghostPrefab.GetComponent<SpriteRenderer>().material.color = color;
            //spriteRenderer = ghostPrefab.GetComponent<SpriteRenderer>();
            //spriteRenderer.color = color;
            //ghostObj.transform.localScale = player.transform.localScale;
            Destroy(ghostObj, destroyTime);
            //spriteRenderer.sprite = player.GetComponent<SpriteRenderer>().sprite;
            
            //if (material != null) spriteRenderer.material = material;
        }
        
    }
}
