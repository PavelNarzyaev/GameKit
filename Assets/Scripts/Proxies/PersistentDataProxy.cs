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
		var encryptedJson = EncryptionUtility.Encrypt(json);
		File.WriteAllText(_filePath, encryptedJson);
		isDirty = false;
	}

	public bool Exists() => File.Exists(_filePath);

	public void Load()
	{
		var encryptedJson = File.ReadAllText(_filePath);
		var json = EncryptionUtility.Decrypt(encryptedJson);
		data = JsonUtility.FromJson<PersistentData>(json);
	}
}
