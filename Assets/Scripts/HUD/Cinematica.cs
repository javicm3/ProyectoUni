using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Cinematica : MonoBehaviour
{
    [SerializeField] VideoClip c1;
    [SerializeField] VideoClip c2;
    [SerializeField] VideoClip c3;
    [SerializeField] VideoClip c5;

    [SerializeField] VideoPlayer videoPlayer;
    float timeDelay;

    private void Awake()
    {
        
    }

    private void Start()
    {
        PlayCinematica((int)GameObject.FindObjectOfType<GameManager>().cinematicaIndex);
    }

    public void PlayCinematica(int i)
    {
        switch(i)
        {
            case 1:
                videoPlayer.clip = c1;
                break;
            case 2:
                videoPlayer.clip = c2;
                break;
            case 3:
                videoPlayer.clip = c3;
                break;
            case 5:
                videoPlayer.clip = c5;
                break;
        }

        timeDelay = (float)videoPlayer.clip.length;
        print(timeDelay + "  " + videoPlayer.clip.length);
        videoPlayer.enabled = true;
        StartCoroutine(GoToScene());        
    }

    IEnumerator GoToScene()
    {
        yield return new WaitForSeconds(timeDelay);
        string scene = GameManager.Instance.cinematicaScene;
        //SceneManager.LoadSceneAsync(GameManager.Instance.cinematicaScene);
        if (scene=="PantallaInicio")
        {
            SistemaGuardado.Guardar();
            Destroy(GameManager.Instance);
        }
        SceneManager.LoadScene(scene);

    }
}
