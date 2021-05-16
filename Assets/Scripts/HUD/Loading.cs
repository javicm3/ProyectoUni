using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    AsyncOperation async;

    // Start is called before the first frame update
    private void Start()
    {
        async=SceneManager.LoadSceneAsync(GameManager.Instance.cinematicaScene);
        async.allowSceneActivation = false;

        StartCoroutine(Enable());
        //SceneManager.sceneLoaded += FinishLoading;
    }

    /*void FinishLoading(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= FinishLoading;
        //Destroy(this.gameObject);
    }*/

    IEnumerator Enable()
    {
        do
        {
            yield return new WaitForSeconds(0.5f);
        } while (async.progress < 0.9f);

        async.allowSceneActivation = true;
    }
}
