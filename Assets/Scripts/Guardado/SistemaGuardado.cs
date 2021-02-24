using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaGuardado : MonoBehaviour
{
    public static readonly string Carpeta_Guardado = Application.dataPath + "/Guardado/";

    public static void Incializar()
    {
        if (!Directory.Exists(Carpeta_Guardado))
        {
            Directory.CreateDirectory(Carpeta_Guardado);
        }
    }

    public static void Cargar()
    {
        string json;

        if (File.Exists(Carpeta_Guardado + "save_ListaNiveles.txt"))
        {
            json = File.ReadAllText(Carpeta_Guardado + "save_ListaNiveles.txt");

            ListaNivelGuardadoJSON lista = JsonUtility.FromJson<ListaNivelGuardadoJSON>(json);

            GameManager.Instance.ListaNiveles.Clear();
            foreach (LevelInfo level in lista.listaNivel)
            {
                GameManager.Instance.ListaNiveles.Add(level);
            }            
        }
    }

    public static void Guardar()
    {
        ListaNivelGuardadoJSON lista = new ListaNivelGuardadoJSON();
        lista.listaNivel = GameManager.Instance.ListaNiveles;

        string listaNiveles = JsonUtility.ToJson(lista);
        File.WriteAllText(Carpeta_Guardado + "save_ListaNiveles.txt", listaNiveles);
    }

}


[System.Serializable]
public class ListaNivelGuardadoJSON
{
    public List<LevelInfo> listaNivel;
}