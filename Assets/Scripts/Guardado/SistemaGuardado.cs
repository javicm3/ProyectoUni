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
    public static DataSlots dataSlots = new DataSlots();
    public static float timeToIgnore;

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

        if (File.Exists(Carpeta_Guardado + "slotsData.txt"))
        {
            string json = File.ReadAllText(Carpeta_Guardado + "slotsData.txt");
            dataSlots = JsonUtility.FromJson<DataSlots>(json);
        }
        else
        {
            string datos = JsonUtility.ToJson(dataSlots);
            File.WriteAllText(Carpeta_Guardado + "slotsData.txt", datos);
        }

        CargarPlayerPrefs();
    }

    public static void Cargar()
    {
        string json;

        if (File.Exists(Carpeta_Guardado + slots[indiceSlot]+".txt"))
        {
            json = File.ReadAllText(Carpeta_Guardado + slots[indiceSlot] + ".txt");

            DataGuardadoJSON datos = JsonUtility.FromJson<DataGuardadoJSON>(json);

            //Cargar lista de niveles (coleccionables y bool completado)
            GameManager.Instance.ListaNiveles.Clear();
            foreach (LevelInfo level in datos.listaNivel)
            {
                GameManager.Instance.ListaNiveles.Add(level);
                GameManager.Instance.totalColeccionables.AddRange(level.coleccionablesCogidos);             
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
        File.WriteAllText(Carpeta_Guardado + slots[indiceSlot]+ ".txt", datos);

        ListaHabilidades habilidades = GameManager.Instance.Habilidades;

        Debug.Log("guardando");
        GameManager.Instance.MostrarSaveIcon = true;

        if (File.Exists(Carpeta_Guardado + "slotsData.txt"))
        {
            string json = File.ReadAllText(Carpeta_Guardado + "slotsData.txt");

            dataSlots = JsonUtility.FromJson<DataSlots>(json);           
        }

        SaveSlotsData();
        timeToIgnore = Time.time;
    }

    static void SaveSlotsData()
    {
        dataSlots.slotArray[indiceSlot].exists = true;
        dataSlots.slotArray[indiceSlot].coleccionables = GameManager.Instance.totalColeccionables.Count;
        dataSlots.slotArray[indiceSlot].gameTime += (Time.time-timeToIgnore);


        string datos = JsonUtility.ToJson(dataSlots);
        File.WriteAllText(Carpeta_Guardado + "slotsData.txt", datos);
    }

    [System.Serializable]
    public class DataSlots
    {
        public Slot[] slotArray = new Slot[] { new Slot(), new Slot(), new Slot() };
    }
    [System.Serializable]
    public class Slot
    {
        public bool exists;
        public float gameTime;
        public int coleccionables;
    }
}


[System.Serializable]
public class DataGuardadoJSON
{
    public List<LevelInfo> listaNivel;
    public ListaHabilidades habilidades;

}