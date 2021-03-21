using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpcionesPausa : MonoBehaviour
{
    [SerializeField] GameObject[] botonesBase;

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] GameObject optionsPanel;


    //SONORIZAR CUANDO SE PULSAN LOS BOTONES    <------------------ !!!!!!
    public void OnOptions(bool options)
    {
        foreach (GameObject b in botonesBase)
        {
            b.SetActive(!options);
        }

        optionsPanel.SetActive(options);
    }

    public void OnMusicChange()
    {
        GameManager.Instance.MusicVolume = musicSlider.value;
    }

    public void OnEffectsChange()
    {
        GameManager.Instance.SfxVolume = sfxSlider.value;
    }
    

    public void SaveVolumeSettings()
    {
        GameManager.Instance.MusicVolumeSave = musicSlider.value;
        GameManager.Instance.SfxVolumeSave = sfxSlider.value;
    }
}
