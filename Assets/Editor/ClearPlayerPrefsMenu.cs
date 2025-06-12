using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [UsedImplicitly]
    public class ClearPlayerPrefsMenu
    {
        [MenuItem("GameKit/Clear PlayerPrefs", false, 100)]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs cleared.");
        }
    }
}
