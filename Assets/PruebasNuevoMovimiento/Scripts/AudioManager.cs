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
    public AudioSource sonidoLoop;
    public AudioSource sonidoPasos;
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
            Stop(sonidosUnaVez);
            sonidosUnaVez.PlayOneShot(clipp);
        }
        else if(source==sonidoLoop)
        {
            sonidoLoop.clip = clipp;
            sonidoLoop.Play();
        }else if (source == sonidoPasos)
        {
            Stop(sonidoPasos);
            sonidoPasos.PlayOneShot(clipp);
        }
    }
    public void Stop(AudioSource source)
    {
        source.Stop();
    }
}
