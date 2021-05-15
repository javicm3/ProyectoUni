using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.U2D;

public class CablesLobbyIluminar : MonoBehaviour
{
    public bool funcionaConColecc = true;
    public bool funcionaConHabilidades = true;
    public bool needDash = false;
    public bool needChispazo = false;
    public bool needParedes = false;
    public bool needviajeEntreCables = false;

    public int coleccionablesParaIluminar = 0;

    Material shader;

    Color original;
    Color actual;

    public bool puedo = true;

    // Start is called before the first frame update
    void Start()
    {     
        shader = GetComponent<SpriteShapeRenderer>().materials[1];
       
        original= shader.GetColor("Color_C112F92E");
        actual = Color.black ;
        
        shader.SetColor("Color_C112F92E", actual);
      
    }

    // Update is called once per frame
    void Update()
    {
        if (funcionaConColecc && funcionaConHabilidades)
        {
            if (shader.GetColor("Color_C112F92E")!=original)
            {
               
                if (coleccionablesParaIluminar >= GameManager.Instance.totalColeccionables.Count)
                {
                    puedo = false;
                }
                if (needChispazo)
                {
                    if (!GameManager.Instance.Habilidades.chispazo)
                    {
                        puedo = false;
                    }
                }
                if (needDash)
                {
                    if (!GameManager.Instance.Habilidades.dash)
                    {
                        puedo = false;
                    }
                }
                if (needParedes)
                {
                    if (!GameManager.Instance.Habilidades.movParedes)
                    {
                        puedo = false;
                    }
                }
                if (needviajeEntreCables)
                {
                    if (!GameManager.Instance.Habilidades.movCables)
                    {
                        puedo = false;
                    }
                }
                if (puedo)
                {
                    actual = Color.Lerp(actual, original, 0.2f);
             

                    shader.SetColor("Color_C112F92E", actual);
                }
            }
          
        }
        else if(funcionaConHabilidades)
        {
            if (shader.GetColor("Color_C112F92E") != original)
            {
              
              
                if (needChispazo)
                {
                    if (!GameManager.Instance.Habilidades.chispazo)
                    {
                        puedo = false;
                    }
                }
                if (needDash)
                {
                    if (!GameManager.Instance.Habilidades.dash)
                    {
                        puedo = false;
                    }
                }
                if (needParedes)
                {
                    if (!GameManager.Instance.Habilidades.movParedes)
                    {
                        puedo = false;
                    }
                }
                if (needviajeEntreCables)
                {
                    if (!GameManager.Instance.Habilidades.movCables)
                    {
                        puedo = false;
                    }
                }
                if (puedo)
                {
                    actual = Color.Lerp(actual, original, 0.2f);

                    shader.SetColor("Color_C112F92E", actual);
                }
            }
        }
        else if (funcionaConColecc)
        {
            if (shader.GetColor("Color_C112F92E") != original)
            {
             
            
                if (coleccionablesParaIluminar >= GameManager.Instance.totalColeccionables.Count)
                {  
                    puedo = false;
                }
                else
                {
                    puedo = true;
                }

                if (puedo)
                {
                    actual = Color.Lerp(actual, original, 0.2f);

                    shader.SetColor("Color_C112F92E", actual);
                }
            }
        }
    }
}
