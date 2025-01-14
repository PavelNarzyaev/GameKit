using System.IO;
using UnityEngine;

public class PersistentDataProxy
{
	public PersistentData data;
	private string _filePath => Path.Combine(Application.persistentDataPath, PersistentDataFileName.fileName);

	public void Save()
	{
		var json = JsonUtility.ToJson(data);
		File.WriteAllText(_filePath, json);
	}

	public bool Exists() => File.Exists(_filePath);

	public void Load()
	{
		var json = File.ReadAllText(_filePath);
		data = JsonUtility.FromJson<PersistentData>(json);
	}
}
