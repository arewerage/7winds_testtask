using UnityEditor;
using UnityEngine;

namespace _Project.Codebase.Editor
{
    public class Tools
    {
        [MenuItem("Tools/Clear Prefs")]
        public static void CrearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("Data was deleted!");
        }
    }
}
