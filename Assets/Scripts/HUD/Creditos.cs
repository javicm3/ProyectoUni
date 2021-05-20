using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    [SerializeField] VideoClip[] creditos;

    [SerializeField] VideoPlayer videoPlayer;

    float timeDelay;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.clip = creditos[SistemaGuardado.indiceIdioma];
        timeDelay = (float)videoPlayer.clip.length;
        videoPlayer.enabled = true;
        StartCoroutine(GoToScene());
    }

    IEnumerator GoToScene()
    {
        yield return new WaitForSeconds(timeDelay);

        SceneManager.LoadScene("PantallaInicio");
    }

}
