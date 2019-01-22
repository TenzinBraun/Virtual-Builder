using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SavingUtils
{

	[CanBeNull] public static readonly string SAVING_EXTENSION = " .save";

	private static string SavingDirectory
	{
		get { return Application.dataPath + "/Saves/"; }
	}

	public static string CurrentSavingDirectory
	{
		get { return SavingDirectory + SceneManager.GetActiveScene().name + "/"; }
	}

	public static void CreateSavingDirectoryIfNotExist()
	{
		if (!Directory.Exists(CurrentSavingDirectory))
			Directory.CreateDirectory(CurrentSavingDirectory);
	}
	
	public static String GenerateFreeSaveID()
	{
		List<String> usedSaveIdentifiers = GetUsedSavedIds();
		String saveIdentifier;

		do
		{
			saveIdentifier = GenerateRandomSaveID();
		}
		while (usedSaveIdentifiers.Contains(saveIdentifier));

		return saveIdentifier;
	}

	private static string GenerateRandomSaveID()
	{
		String saveUserId = "";
		for (var i = 0; i < 3; i++)
		{
			saveUserId += (char) UnityEngine.Random.Range('A', 'Z');
		}

		return saveUserId;
	}

	public static bool IsIdsUsed(string id)
	{
		return GetUsedSavedIds().Contains(id);
	}

	private static List<String> GetUsedSavedIds()
	{
		List<String> usedSaveIdentifiable = new List<string>();
		foreach (var directoryPath in Directory.GetDirectories(SavingDirectory))
		{
			IEnumerable<string> filesName = GetFilesName(directoryPath);
			usedSaveIdentifiable.AddRange(filesName);
		}

		return usedSaveIdentifiable;
	}

	private static IEnumerable<string> GetFilesName(string directoryPath)
	{
		String[] files = Directory.GetFiles(directoryPath, '*' + SAVING_EXTENSION);

		for (var i = 0; i < files.Length; i++)
		{
			String[] splitterFiles = files[i].Split('\\');
			files[i] = splitterFiles[splitterFiles.Length - 1].Split('.')[0];
		}

		return files;
	}
	
	
	
	
}
