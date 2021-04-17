using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaGuardado : MonoBehaviour
{
    public static readonly string Carpeta_Guardado = Application.dataPath + "/Guardado/";
    public static int indiceIdioma = 0;
    public static readonly string[] slots = new string[3] {"Slot 1","Slot 2","Slot 3" };
    public static int indiceSlot = 0;
    public static int[] slotExists = new int[3] {0,0,0};

    public static void CargarPlayerPrefs()
    {
        indiceIdioma = PlayerPrefs.GetInt("idioma");
        slotExists[0] = PlayerPrefs.GetInt("slot0");
        slotExists[1] = PlayerPrefs.GetInt("slot1");
        slotExists[2] = PlayerPrefs.GetInt("slot2");
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

        if (File.Exists(Carpeta_Guardado + slots[indiceSlot]))
        {
            json = File.ReadAllText(Carpeta_Guardado + slots[indiceSlot]);

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

        }
    }

    public static void Guardar()
    {
        DataGuardadoJSON datosGuardado = new DataGuardadoJSON();
        datosGuardado.listaNivel = GameManager.Instance.ListaNiveles;
        datosGuardado.habilidades = GameManager.Instance.Habilidades;

        string datos = JsonUtility.ToJson(datosGuardado);
        File.WriteAllText(Carpeta_Guardado + slots[indiceSlot], datos);

        ListaHabilidades habilidades = GameManager.Instance.Habilidades;

        Debug.Log("guardando");
        PlayerPrefs.SetInt("slot"+indiceSlot, 1);
        GameManager.Instance.MostrarSaveIcon = true;
    }
}


[System.Serializable]
public class DataGuardadoJSON
{
    public List<LevelInfo> listaNivel;
    public ListaHabilidades habilidades;

}