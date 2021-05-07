using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using InControl;

public class VideosTutorial : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI derecha;
    [SerializeField] TextMeshProUGUI izquierda;
    [SerializeField] TextMeshProUGUI cerrar;
    [SerializeField] TextMeshProUGUI textoHab;

    [Header("Clips de Video")]
    [SerializeField] VideoClip[] clipsDash;
    [SerializeField] VideoClip[] clipsChispazo;
    [SerializeField] VideoClip[] clipsMovCables;
    [SerializeField] VideoClip[] clipsMovParedes;

    VideoClip[] clipsActual;
    int index = 0;

    //-------------------------------------------------->Mirar para cambiar lo de devolver el input con el gm

    public void AbrirTutorial(DesbloquearHabilidades.habilidad habilidad)
    {
        panel.SetActive(true);
        FindObjectOfType<PlayerInput>().enabled = false;

        int l = SistemaGuardado.indiceIdioma;

        switch (habilidad)
        {
            case DesbloquearHabilidades.habilidad.dash:
                clipsActual = clipsDash;
                textoHab.text = Idiomas.skill[l] + "dash";
                break;
            case DesbloquearHabilidades.habilidad.chispazo:
                clipsActual = clipsChispazo;
                textoHab.text = Idiomas.skill[l] + Idiomas.spark[l];
                break;
            case DesbloquearHabilidades.habilidad.movimientoCable:
                clipsActual = clipsMovCables;
                textoHab.text = Idiomas.skill[l] + Idiomas.wireTravel[l];
                break;
            case DesbloquearHabilidades.habilidad.movimientoPared:
                clipsActual = clipsMovParedes;
                textoHab.text = Idiomas.skill[l] + Idiomas.climbWalls[l];
                break;
        }

        ChangeVideo();
        CheckController();
    }

    enum Select {derecha, izquierda, cerrar }
    Select select;
    private void Update()
    {
        if (isController)
        {
            CheckInputController();
        }
    }

    public void CerrarTutorial()
    {
        Cursor.visible = false;

        videoPlayer.Stop();
        FindObjectOfType<PlayerInput>().enabled = true;
        panel.SetActive(false);
    }

    void ChangeVideo()
    {
        videoPlayer.clip = clipsActual[index];
        print(videoPlayer.clip+ " " + clipsActual.Length + " " + index);
        if (index == 0) { izquierda.gameObject.SetActive(false); }
        else { izquierda.gameObject.SetActive(true); }

        if (index >= clipsActual.Length - 1) { derecha.gameObject.SetActive(false); }
        else { derecha.gameObject.SetActive(true); }

        videoPlayer.Play();
    }

    public void ClicFlecha(bool izq)
    {
        if (izq) { index--; izquierda.fontSharedMaterial = diselected; }
        else { index++;derecha.fontSharedMaterial = diselected; }

            ChangeVideo();
    }

    [Header("Materiales UI")]
    [SerializeField] Material selected;
    [SerializeField] Material diselected;

    public void SelectFlecha(bool izq)
    {
        if (izq) izquierda.fontSharedMaterial = selected;
        else derecha.fontSharedMaterial = selected;
    }

    public void DiselectFlecha(bool izq)
    {
        if (izq) izquierda.fontSharedMaterial = diselected;
        else derecha.fontSharedMaterial = diselected;
    }


    InputDevice joystick;
    bool isController = false;

    private Select Select1 { get => select;
        set {
            select = value;

            derecha.fontSharedMaterial = diselected;
            izquierda.fontSharedMaterial = diselected;
            cerrar.fontSharedMaterial = diselected;

            switch (value)
            {
                case Select.derecha:
                    derecha.fontSharedMaterial = selected;
                    break;

                case Select.izquierda:
                    izquierda.fontSharedMaterial = selected;
                    break;

                case Select.cerrar:
                    cerrar.fontSharedMaterial = selected;
                    break;
            }
        }
    }

    void CheckController()
    {
        joystick = InputManager.ActiveDevice;
        if (joystick != null && joystick.Name != "NullInputDevice")
        {
            isController = true;
            if (!derecha.gameObject.activeSelf) Select1 = Select.cerrar;
            else Select1 = Select.derecha;
        }
        else { Cursor.visible = true; }
    }

    void CheckInputController()
    {
        if (Select1 != Select.cerrar)
        {
            if (joystick.LeftStickX.IsPressed)
            {
                if (Input.GetAxis("Horizontal") > 0.1f && derecha.gameObject.activeSelf)
                {
                    Select1 = Select.derecha;
                }
                else if (Input.GetAxis("Horizontal") < -0.1f && izquierda.gameObject.activeSelf)
                {
                    Select1 = Select.izquierda;
                }
            }

            if (joystick.LeftStickY.IsPressed && Input.GetAxis("Vertical") < -0.1f)
            {
                Select1 = Select.cerrar;
            }
        }
        else
        {
            if (joystick.LeftStickY.IsPressed && Input.GetAxis("Vertical") > 0.1f)
            {
                if (derecha.gameObject.activeSelf)
                {
                    Select1 = Select.derecha;
                }
                else if(izquierda.gameObject.activeSelf)
                {
                    Select1 = Select.izquierda;
                }                
            }
        }


        if ( joystick.Action2.WasPressed)
        {
            switch (Select1)
            {
                case Select.derecha:
                    ClicFlecha(false);
                    if (!derecha.gameObject.activeSelf)
                    { Select1 = Select.cerrar; } else Select1 = Select.derecha;
                    break;

                case Select.izquierda:
                    ClicFlecha(true);
                    if (!izquierda.gameObject.activeSelf)
                    { Select1 = Select.derecha;} else Select1 = Select.izquierda;
                    break;

                case Select.cerrar:
                    CerrarTutorial();
                    break;

                default:
                    break;
            }
        }
    }

    public void SelectCerrar(bool select)
    {
        if (select) cerrar.fontSharedMaterial = selected;
        else cerrar.fontSharedMaterial = diselected;
    }
}
