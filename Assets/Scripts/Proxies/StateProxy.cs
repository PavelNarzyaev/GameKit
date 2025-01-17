using System;
using System.IO;
using UnityEngine;
using Zenject;

public class StateProxy
{
	public const string fileName = "state.json";

	public State data;
	public bool isDirty { get; private set; }
	public event Action refreshFromJsonEvent;

	private string _filePath;

	[Inject]
	private void Inject()
	{
		_filePath = Path.Combine(Application.persistentDataPath, fileName);
	}

	public void MarkAsDirty()
	{
		isDirty = true;
	}

	public void RefreshFile()
	{
		SaveJsonToFile(JsonUtility.ToJson(data));
	}

	public void RefreshFromJson(string json)
	{
		try
		{
			data = JsonUtility.FromJson<State>(json);
		}
		catch
		{
			// TODO: show loading error popup
			throw;
		}

		SaveJsonToFile(json);
		refreshFromJsonEvent?.Invoke();
	}

	private void SaveJsonToFile(string json)
	{
		var encryptedJson = EncryptionUtility.Encrypt(json);
		File.WriteAllText(_filePath, encryptedJson);
		isDirty = false;
	}

	public bool Exists() => File.Exists(_filePath);

	public void RefreshFromFile()
	{
		var json = LoadJsonFromFile();
		data = JsonUtility.FromJson<State>(json);
	}

	public string LoadJsonFromFile()
	{
		var encryptedJson = File.ReadAllText(_filePath);
		return EncryptionUtility.Decrypt(encryptedJson);
	}
}
