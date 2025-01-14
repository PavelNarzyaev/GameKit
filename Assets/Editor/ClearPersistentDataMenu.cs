using System.IO;
using UnityEditor;
using UnityEngine;

public class ClearPersistentDataMenu
{
	[MenuItem("GameKit/Clear Persistent Data", false, 101)]
	public static void ClearPersistentData()
	{
		var filePath = Path.Combine(Application.persistentDataPath, PersistentDataFileName.fileName);

		if (File.Exists(filePath))
		{
			File.Delete(filePath);
			Debug.Log("Persistent data file deleted.");
		}
		else
			Debug.LogWarning("Persistent data file not found.");
	}
}
