using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip salto;
    public AudioClip dobleSalto;
    public AudioClip caidaSuelo;
    public AudioClip velMaxima;
    public AudioClip dash;
    public AudioClip dashAbajo;
    public AudioClip caidaDashAbajo;
    public AudioClip pegarPared;
    public AudioClip moverPared;
    public AudioClip saltarPared;
    public AudioClip morir;
    public AudioClip daño;
    public AudioClip coleccionable;
    public AudioClip estrella;
    public AudioClip entradaCables;
    public AudioClip salidaCables;
    public AudioClip pasarPorNodo;
    public AudioClip MoverseCables;
    public AudioClip pasos;
    
    public AudioSource sonidosUnaVez;
    public AudioSource[] sonidosArrayUnavez;
    public AudioSource sonidoLoop;
    public AudioSource[] sonidosLoop;
    public AudioSource sonidoPasos;
    public AudioSource[] sonidosPasos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play(AudioSource source,AudioClip clipp)
    {
        if (source == sonidosUnaVez)
        {
            if (sonidosUnaVez.isPlaying == true&&sonidosUnaVez.clip!= clipp)
            {
                for (int i = 0; i < sonidosArrayUnavez.Length; i++)
                {
                    if (sonidosArrayUnavez[i].isPlaying == false  &&sonidosArrayUnavez[i].clip != clipp)
                    {
                        Stop(sonidosArrayUnavez[i]);
                        sonidosArrayUnavez[i].PlayOneShot(clipp);
                        break;
                    }
                }
            }
            else
            {
              
                sonidosUnaVez.PlayOneShot(clipp);
            }
           
        }
        else if(source==sonidoLoop)
        {
            if (sonidoLoop.isPlaying == true && sonidoLoop.clip!=clipp)
            {
                for (int i = 0; i < sonidosLoop.Length; i++)
                {
                    if (sonidosLoop[i].isPlaying == false && sonidosLoop[i].clip!= clipp )
                    {
                        Stop(sonidosLoop[i]);
                        sonidosLoop[i].PlayOneShot(clipp);
                        break;
                    }
                }
            }
            else
            {
               
                sonidoLoop.PlayOneShot(clipp);
            }

        }
        else if (source == sonidoPasos)
        {
            if (sonidoPasos.isPlaying == true && sonidoPasos.clip != clipp)
            {
                for (int i = 0; i < sonidosLoop.Length; i++)
                {
                    if (sonidosPasos[i].isPlaying == false && sonidosPasos[i].clip != clipp)
                    {
                        Stop(sonidosPasos[i]);
                        sonidosPasos[i].PlayOneShot(clipp);
                        break;
                    }
                }
            }
            else
            {
                sonidoPasos.PlayOneShot(clipp);
              
            }
          
           
        }
    }
    public void Stop(AudioSource source)
    {
        source.Stop();
    }
}
