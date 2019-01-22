using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{

	[SerializeField] private List<string> tags;

	private List<GameObject> editableGameObjects;
	private string currentSaveID;
	
	private void Start ()
	{
		currentSaveID = null;
		editableGameObjects = new List<GameObject>();

		foreach (string tag in tags)
		editableGameObjects.AddRange(GameObject.FindGameObjectsWithTag(tag));
	}

	public string GetCurrentSaveID()
	{
		return currentSaveID;
	}

	public bool UpdateCurrentSave()
	{
		if (currentSaveID == null) return false;

		SaveGameObjects(currentSaveID);
		return true;
	}

	private void SaveGameObjects(string currentSaveId)
	{
		var saveScene = new SaveScene(SceneManager.GetActiveScene().name, editableGameObjects);

		SavingUtils.CreateSavingDirectoryIfNotExist();

		var json = JsonUtility.ToJson(saveScene);
		File.WriteAllText(SavingUtils.CurrentSavingDirectory + currentSaveId + SavingUtils.SAVING_EXTENSION, json);
		
	}

	public bool LoadGameObject(string savingID)
	{
		string savingPath = SavingUtils.CurrentSavingDirectory + savingID + SavingUtils.SAVING_EXTENSION;
		if (!File.Exists(savingPath)) return false;

		string jsonTextRead = File.ReadAllText(savingPath);
		SaveScene saveScene = JsonUtility.FromJson<SaveScene>(jsonTextRead);

		PlaceGameObjects(saveScene);
		currentSaveID = savingID;

		return true;

	}

	private void PlaceGameObjects(SaveScene saveScene)
	{
		foreach (SaveGameObject saveGameObject in saveScene.SaveGameObjects)
		{
			GameObject gameObject = GameObject.Find(saveGameObject.name);
			gameObject.transform.position = saveGameObject.gameObjectPosition;
			gameObject.transform.rotation = saveGameObject.gameObjectRotation;
		}
	}
}
