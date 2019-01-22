using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveScene
{
    public string name;
    public List<SaveGameObject> SaveGameObjects;

    public SaveScene(string name, List<GameObject> gameObjectToSave)
    {
        this.name = name;
        SaveGameObjects = new List<SaveGameObject>();
        foreach (var editableGameObject in gameObjectToSave)
        {
            SaveGameObjects.Add(new SaveGameObject(editableGameObject.name, editableGameObject.transform));
        }
    }
}
