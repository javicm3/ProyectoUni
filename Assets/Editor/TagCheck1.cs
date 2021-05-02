using UnityEngine;
using System.Collections;
using UnityEditor;

public class TagCheck1 : MonoBehaviour
{

    private static string SelectedTag = "Coleccionable";

    [MenuItem("Helpers/Select By Tag")]
    public static void SelectObjectsWithTag()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(SelectedTag);
        Selection.objects = objects;
    }
}