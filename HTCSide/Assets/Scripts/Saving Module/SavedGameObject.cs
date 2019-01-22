
using System;
using UnityEngine;

[Serializable]
public class SaveGameObject
{

    public string name;
    public Vector3 gameObjectPosition;
    public Quaternion gameObjectRotation;

    public SaveGameObject(string name, Transform transform)
    {
        this.name = name;
        gameObjectPosition = transform.position;
        gameObjectRotation = transform.rotation;
    }

}
