using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    AsyncOperation async;

    // Start is called before the first frame update
    private void OnEnable()
    {
        async=SceneManager.LoadSceneAsync(GameManager.Instance.cinematicaScene);
        async.allowSceneActivation = false;
        print("PantallaDeCarga");
        //SceneManager.sceneLoaded += FinishLoading;
    }

    /*void FinishLoading(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= FinishLoading;
        //Destroy(this.gameObject);
    }*/

    private void Update()
    {
        if (async.progress>=0.9f)
        {
            async.allowSceneActivation = true;
        }
        
    }
}
