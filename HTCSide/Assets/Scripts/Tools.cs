using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ControllerManager {

    public enum Tool { TELEPORTER = 0, PROPULSER = 1, HAND = 2, TRASH = 3, CATALOG = 4 };
    public List<GameObject> ToolGameObjects;

    private void initToolAssets()
    {
        for (int i = 0; i < numberOfTool(); i++)
        {
            setToolAsset((Tool)i,i);
        }

    }

    private void hideToolAsset(int tool)
    {
        GameObject toolAsset = ToolGameObjects[tool];
        toolAsset.SetActive(false);
    }

    private void showToolAsset(int tool)
    {
        GameObject toolAsset = ToolGameObjects[tool];
        toolAsset.SetActive(true);
    }

    private void moveToolAsset(int tool, int xTransform, int yTransform, int zTransform)
    {
        GameObject toolAsset = ToolGameObjects[tool];
        toolAsset.transform.position = new Vector3(xTransform, yTransform, zTransform);

    }

    private void setToolAsset(Tool tool,int position)
    {
        ToolGameObjects.Insert(position,GameObject.Find(getToolName(tool) + "Icon"));
    }

    private int numberOfTool()
    {
        return Enum.GetNames(typeof(Tool)).Length;
    }

    String getToolName(Tool tool)
    {
        return getCamelCase(Enum.GetName(typeof(Tool), tool));
    }

    private String getCamelCase(String toCamelCase)
    {
        String result = toCamelCase.Substring(0, 1).ToUpper();
        result += toCamelCase.Substring(1, toCamelCase.Length - 1).ToLower();
        return result;

    }
}
