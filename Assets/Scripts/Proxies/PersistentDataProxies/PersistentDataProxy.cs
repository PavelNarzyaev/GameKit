using System.IO;
using UnityEngine;
using Zenject;

public class PersistentDataProxy
{
	public PersistentData data;
	public bool isDirty { get; private set; }
	private string _filePath;

	[Inject]
	private void Inject()
	{
		_filePath = Path.Combine(Application.persistentDataPath, PersistentDataFileName.fileName);
	}

	public void MarkAsDirty()
	{
		isDirty = true;
	}

	public void Save()
	{
		var json = JsonUtility.ToJson(data);
		File.WriteAllText(_filePath, json);
		isDirty = false;
	}

	public bool Exists() => File.Exists(_filePath);

	public void Load()
	{
		var json = File.ReadAllText(_filePath);
		data = JsonUtility.FromJson<PersistentData>(json);
	}
}
