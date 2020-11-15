using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    
    float actualtiempo;
    Text textotiempo;
    public GameObject[] bestmonedasImage;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            bestmonedasImage = GameObject.FindGameObjectsWithTag("MaxScore");
            if (GameManager.Instance.maxScore1 == 0)
            {
                foreach(GameObject go in bestmonedasImage)
                {
                    go.GetComponent<Image>().color = Color.black;
                }
            }
            if (GameManager.Instance.maxScore1 == 1)
            {
                bestmonedasImage[0].GetComponent<Image>().color = Color.white;
            }
            if (GameManager.Instance.maxScore1 ==2)
            {
                bestmonedasImage[0].GetComponent<Image>().color = Color.white;
                bestmonedasImage[1].GetComponent<Image>().color = Color.white;
            }
            if (GameManager.Instance.maxScore1 == 3)
            {
                foreach (GameObject go in bestmonedasImage)
                {
                    go.GetComponent<Image>().color = Color.white;
                }
            }
            actualtiempo =GameManager.Instance.tiemponivel1;
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            bestmonedasImage = GameObject.FindGameObjectsWithTag("MaxScore");
            if (GameManager.Instance.maxScore2 == 0)
            {
                foreach (GameObject go in bestmonedasImage)
                {
                    go.GetComponent<Image>().color = Color.black;
                }
            }
            if (GameManager.Instance.maxScore2 == 1)
            {
                bestmonedasImage[0].GetComponent<Image>().color = Color.white;
            }
            if (GameManager.Instance.maxScore2 == 2)
            {
                bestmonedasImage[0].GetComponent<Image>().color = Color.white;
                bestmonedasImage[1].GetComponent<Image>().color = Color.white;
            }
            if (GameManager.Instance.maxScore2 == 3)
            {
                foreach (GameObject go in bestmonedasImage)
                {
                    go.GetComponent<Image>().color = Color.white;
                }
            }
            actualtiempo = GameManager.Instance.tiemponivel2;
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            bestmonedasImage = GameObject.FindGameObjectsWithTag("MaxScore");
            if (GameManager.Instance.maxScore3 == 0)
            {
                foreach (GameObject go in bestmonedasImage)
                {
                    go.GetComponent<Image>().color = Color.black;
                }
            }
            if (GameManager.Instance.maxScore3 == 1)
            {
                bestmonedasImage[0].GetComponent<Image>().color = Color.white;
            }
            if (GameManager.Instance.maxScore3 == 2)
            {
                bestmonedasImage[0].GetComponent<Image>().color = Color.white;
                bestmonedasImage[1].GetComponent<Image>().color = Color.white;
            }
            if (GameManager.Instance.maxScore3 == 3)
            {
                foreach (GameObject go in bestmonedasImage)
                {
                    go.GetComponent<Image>().color = Color.white;
                }
            }
            actualtiempo = GameManager.Instance.tiemponivel3;
        }
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            bestmonedasImage = GameObject.FindGameObjectsWithTag("MaxScore");
            if (GameManager.Instance.maxScore4 == 0)
            {
                foreach (GameObject go in bestmonedasImage)
                {
                    go.GetComponent<Image>().color = Color.black;
                }
            }
            if (GameManager.Instance.maxScore4 == 1)
            {
                bestmonedasImage[0].GetComponent<Image>().color = Color.white;
            }
            if (GameManager.Instance.maxScore4 == 2)
            {
                bestmonedasImage[0].GetComponent<Image>().color = Color.white;
                bestmonedasImage[1].GetComponent<Image>().color = Color.white;
            }
            if (GameManager.Instance.maxScore4 == 3)
            {
                foreach (GameObject go in bestmonedasImage)
                {
                    go.GetComponent<Image>().color = Color.white;
                }
            }
            actualtiempo = GameManager.Instance.tiemponivel4;
        }
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            bestmonedasImage = GameObject.FindGameObjectsWithTag("MaxScore");
            if (GameManager.Instance.maxScore5 == 0)
            {
                foreach (GameObject go in bestmonedasImage)
                {
                    go.GetComponent<Image>().color = Color.black;
                }
            }
            if (GameManager.Instance.maxScore5 == 1)
            {
                bestmonedasImage[0].GetComponent<Image>().color = Color.white;
            }
            if (GameManager.Instance.maxScore5 == 2)
            {
                bestmonedasImage[0].GetComponent<Image>().color = Color.white;
                bestmonedasImage[1].GetComponent<Image>().color = Color.white;
            }
            if (GameManager.Instance.maxScore5 == 3)
            {
                foreach (GameObject go in bestmonedasImage)
                {
                    go.GetComponent<Image>().color = Color.white;
                }
            }
            actualtiempo = GameManager.Instance.tiemponivel5;
        }
       
        textotiempo = GameObject.Find("TextoTiempo").GetComponent<Text>();
        GameManager.Instance.monedasImage = GameObject.FindGameObjectsWithTag("ImageMoneda");
   
        
        GameManager.Instance.actualMonedas = 0;
        GameManager.Instance.personajevivo = true;
    }

    // Update is called once per frame
    void Update()
    {
        int min = Mathf.FloorToInt(actualtiempo/ 60);
        int sec = Mathf.FloorToInt(actualtiempo % 60);
        if (min < 10)
        {
           textotiempo.text = (min.ToString("0") + ":" + sec.ToString("00"));
        }
        else if (min >= 10)
        {
            textotiempo.text = (min.ToString("00") + ":" + sec.ToString("00"));
        }
       
        //actualtiempo -= Time.deltaTime;
        //if (actualtiempo <= 0)
        //{
        //    actualtiempo = 0;
        //    GameManager.Instance.MuertePJ();
        //}
    }
}
