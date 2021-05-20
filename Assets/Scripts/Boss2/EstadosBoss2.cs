using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EstadosBoss2 : MonoBehaviour
{
    public int brazosCortados = 0;

    public bool ataqueTerminado = false;
    public bool bossActivo;
    public bool bossStuneado;
    
    public float tiempoStunFase1;
    public float tiempoStunFase2;
    public float tiempoStunFase3;
    public float tiempoStunFase4;

    public int ataquesFase1;
    public int ataquesFase2;
    public int ataquesFase3;
    public int acumulacion = 0;
    
    AtaquesBoss ab;
    
    public float tiempoParadaActual;
    public GameObject laseresLimite;
    public GameObject[] chapasFinales;
    public GameObject cabeza;
    public GameObject[] tentaculosCaer;
    
    // Start is called before the first frame update
    void Start()
    {
        bossStuneado = false;
        ataqueTerminado = true;
        acumulacion = 0;
        ab = GetComponent<AtaquesBoss>();

        for (int i = 0; i < chapasFinales.Length; i++)
        {
            chapasFinales[i].GetComponent<BoxCollider2D>().enabled = false;
        }
        cabeza.GetComponent<Collider2D>().enabled = false;
        cabeza.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            bossActivo = true;
        }
        if (bossActivo)
        {
            if (laseresLimite.activeSelf == true && brazosCortados >= 2)
            {
                laseresLimite.SetActive(false);
            }
            if (brazosCortados == 0 || brazosCortados == 1)
            {
                tiempoParadaActual = tiempoStunFase1;

                print("fase1");
                if(ataqueTerminado == true)
                {
                    float ataque = Random.Range(0, 2);
                    if(ataque == 0 && acumulacion < ataquesFase1)
                    {
                        ataqueTerminado = false;
                        ab.LaserVertical();
                        
                    }
                    else if(ataque > 0 && acumulacion < ataquesFase1)
                    {
                        ataqueTerminado = false;
                        ab.SeleccionarLaserHorizontal();
                    }
                    if(acumulacion >= ataquesFase1)
                    {
                        print("disparando");
                        ataqueTerminado = false;
                        StartCoroutine(ab.DisparosBoss());
                        
                    }

                }
            }
            else if (brazosCortados == 2 || brazosCortados == 3)
            {
                GetComponent<CinematicaBoss>().Mensajes(1);
                print("fase2");
                tiempoParadaActual = tiempoStunFase2;
                if (ataqueTerminado == true)
                {
                    
                    float ataque = Random.Range(0, 2);
                    if (ataque == 0 && acumulacion < ataquesFase2)
                    {
                        ataqueTerminado = false;
                        StartCoroutine(ab.LaserDiagonal(ataqueTerminado));
                    }
                    else if (ataque == 1 && acumulacion < ataquesFase2)
                    {
                        ataqueTerminado = false;
                        ab.SeleccionarLaserHorizontal();
                    }
                    if (acumulacion >= ataquesFase2)
                    {
                        ataqueTerminado = false;
                        StartCoroutine(ab.DisparosBoss());
                    }

                }
            }
            else if(brazosCortados == 4 || brazosCortados == 5)
            {
                GetComponent<CinematicaBoss>().Mensajes(1);
                ab.drones.SetActive(true);
                print("fase3");
                tiempoParadaActual = tiempoStunFase3;
                if (ataqueTerminado == true)
                {
                    float ataque = Random.Range(0, 2);
                    if (ataque == 0 && acumulacion < ataquesFase3)
                    {
                        ataqueTerminado = false;
                        StartCoroutine(ab.LaserDiagonal(ataqueTerminado));
                        //acumulacion++;
                    }
                    else if (ataque == 1 && acumulacion < ataquesFase3)
                    {
                        ataqueTerminado = false;
                        ab.SeleccionarLaserHorizontal();
                        //acumulacion++;
                    }
                    if (acumulacion >= ataquesFase3)
                    {
                        ataqueTerminado = false;
                        StartCoroutine(ab.DisparosBoss());
                        acumulacion = 0;
                    }

                }
            }
            else if((brazosCortados == 6 || brazosCortados >= 6) && brazosCortados < 10)
            {
                ab.drones.SetActive(false);
                ab.dronesFinal.SetActive(true);
                bossStuneado = true;
                for (int i = 0; i < chapasFinales.Length; i++)
                {
                    chapasFinales[i].GetComponent<BoxCollider2D>().enabled = true;
                }

                print("faseFinal");
            }
            else if(brazosCortados >= 10)
            {
                for(int i = 0; i < tentaculosCaer.Length; i++)
                {
                    tentaculosCaer[i].transform.parent = cabeza.transform;
                    //tentaculosCaer[i].transform.position = new Vector3(0, 0, 0);
                }

                ab.drones.SetActive(false);
                ab.dronesFinal.SetActive(false);
                GetComponent<Animator>().enabled = false;
                cabeza.GetComponent<Collider2D>().enabled = true;
                cabeza.GetComponent<Rigidbody2D>().isKinematic = false;

                Invoke("LlamarCinematica", 2);
            }

        }
    }
    public void GuardarDatos()
    {
        if (GameManager.Instance.NivelActual.actualColeccionablesCogidos.Count == 0)
        {
            if (SceneManager.GetActiveScene().name != "Nivel_4_Boss1" && SceneManager.GetActiveScene().name != "Nivel_12_BossFinal")
            {
                ManagerLogros.Instance.DesbloquearLogro(25);
            }

        }
        foreach (string go in GameManager.Instance.NivelActual.actualColeccionablesCogidos)
        {
            if (!GameManager.Instance.NivelActual.coleccionablesCogidos.Contains(go))
            {
                GameManager.Instance.NivelActual.coleccionablesCogidos.Add(go);


                GameManager.Instance.totalColeccionables.Add(go);
            }
        }
        if (GameManager.Instance.NivelActual.coleccionablesCogidos.Count == GameManager.Instance.NivelActual.maxColeccionables)
        {
            if (GameManager.Instance.NivelActual.nombreNivel != "Nivel_4_Boss1" && GameManager.Instance.NivelActual.nombreNivel != "Nivel_12_BossFinal") ManagerLogros.Instance.DesbloquearLogro(13);
            if (!GameManager.Instance.NivelActual.todosColeccionablesCogidos)
            {
                GameManager.Instance.NivelActual.todosColeccionablesCogidos = true;
                ManagerLogros.Instance.AddStat("NivelesTodosColeccionables");
            }

        }
        GameManager.Instance.NivelActual.completado = true;


        GameManager.Instance.UltimoCheck = null;
    }
    void LlamarCinematica()
    {
        if (GhostData.Instance != null)
        {
            GhostData.Instance.TerminarNivel(SceneManager.GetActiveScene().name);
        }
        if (SceneManager.GetActiveScene().name == "Nivel_12_BossFinal")
        {
            ManagerLogros.Instance.DesbloquearLogro(8);
        }
        GuardarDatos();
        if (FindObjectOfType<VidaPlayer>().heMuertoEnEsteNivel == false)
        {
            ManagerLogros.Instance.DesbloquearLogro(15);
            if (!GameManager.Instance.NivelActual.pasadoSinMorir)
            {
                GameManager.Instance.NivelActual.pasadoSinMorir = true;
                ManagerLogros.Instance.AddStat("NivelesSinMuerte");
            }


        }
        SistemaGuardado.Guardar();
        GameManager.Instance.PlayCinematica(5, "PantallaInicio");
    }
}
