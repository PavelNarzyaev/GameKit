using System;
using System.IO;
using UnityEngine;
using Zenject;

public class StateProxy
{
	private const string _fileName = "state.json";

	public State data;
	public bool isDirty { get; private set; }
	public event Action refreshFromJsonEvent;

	private static string GetFilePath() => Path.Combine(Application.persistentDataPath, _fileName);

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
		File.WriteAllText(GetFilePath(), encryptedJson);
		isDirty = false;
	}

	public bool Exists() => File.Exists(GetFilePath());

	public void RefreshFromFile()
	{
		var json = LoadJsonFromFile();
		data = JsonUtility.FromJson<State>(json);
	}

	public string LoadJsonFromFile()
	{
		var encryptedJson = File.ReadAllText(GetFilePath());
		return EncryptionUtility.Decrypt(encryptedJson);
	}

	public static void DeleteFile()
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
