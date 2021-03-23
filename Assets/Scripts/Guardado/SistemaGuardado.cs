using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaGuardado : MonoBehaviour
{
    public static readonly string Carpeta_Guardado = Application.dataPath + "/Guardado/";
    public static int indiceIdioma = 0;

    public static void CargarPlayerPrefs()
    {
        indiceIdioma = PlayerPrefs.GetInt("idioma");
    }

    public static void CambiarIndiceIdioma(int index)
    {
        indiceIdioma = index;
        PlayerPrefs.SetInt("idioma",index);
    }

    public static void Incializar()
    {
        if (!Directory.Exists(Carpeta_Guardado))
        {
            Directory.CreateDirectory(Carpeta_Guardado);
        }

        CargarPlayerPrefs();
    }

    public static void Cargar()
    {
        string json;

        if (File.Exists(Carpeta_Guardado + "save_DatosGM.txt"))
        {
            json = File.ReadAllText(Carpeta_Guardado + "save_DatosGM.txt");

            DataGuardadoJSON datos = JsonUtility.FromJson<DataGuardadoJSON>(json);

            //Cargar lista de niveles (coleccionables y bool completado)
            GameManager.Instance.ListaNiveles.Clear();
            foreach (LevelInfo level in datos.listaNivel)
            {
                GameManager.Instance.ListaNiveles.Add(level);
                foreach (string item in level.coleccionablesCogidos)
                {
                    GameManager.Instance.totalColeccionables.Add(item);
                }                
            }


            //Cargar booleanos de las habilidades
            GameManager.Instance.Habilidades.dash = datos.habilidades.dash;
            GameManager.Instance.Habilidades.chispazo = datos.habilidades.chispazo;
            GameManager.Instance.Habilidades.movParedes = datos.habilidades.movParedes;
            GameManager.Instance.Habilidades.movCables = datos.habilidades.movCables;

            print(GameManager.Instance.Habilidades.movParedes);

            //En el lobby no se cambia el numero al guardar pero en principio da igual porque no se deberá cargar la partida ahi
        }
    }

    public static void Guardar()
    {
        DataGuardadoJSON datosGuardado = new DataGuardadoJSON();
        datosGuardado.listaNivel = GameManager.Instance.ListaNiveles;
        datosGuardado.habilidades = GameManager.Instance.Habilidades;

        string datos = JsonUtility.ToJson(datosGuardado);
        File.WriteAllText(Carpeta_Guardado + "save_DatosGM.txt", datos);

        ListaHabilidades habilidades = GameManager.Instance.Habilidades;

        GameManager.Instance.MostrarSaveIcon = true;
    }
}


[System.Serializable]
public class DataGuardadoJSON
{
    public List<LevelInfo> listaNivel;
    public ListaHabilidades habilidades;

}