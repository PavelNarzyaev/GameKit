using System;
using System.IO;
using UnityEngine;

public class LocalStateProxy
{
	private const string _fileName = "state.json";

	public State data;
	public bool IsDirty { get; private set; }
	public event Action refreshFromJsonEvent;

	private static string GetFilePath() => Path.Combine(Application.persistentDataPath, _fileName);

	public void MarkAsDirty()
	{
		IsDirty = true;
	}

	public void Save()
	{
		SaveJsonToFile(JsonUtility.ToJson(data));
	}

	public void Set(string json)
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
		File.WriteAllText(GetFilePath(), encryptedJson);
		IsDirty = false;
	}

	public bool CheckIfExists() => File.Exists(GetFilePath());

	public void Refresh()
	{
		var json = Get();
		data = JsonUtility.FromJson<State>(json);
	}

	public string Get()
	{
		var encryptedJson = File.ReadAllText(GetFilePath());
		return EncryptionUtility.Decrypt(encryptedJson);
	}

	public static void Delete()
	{
		var filePath = GetFilePath();
		if (File.Exists(filePath))
		{
			File.Delete(filePath);
			Debug.Log("Persistent data file deleted.");
		}
		else
			Debug.LogWarning("Persistent data file not found.");
	}
}
