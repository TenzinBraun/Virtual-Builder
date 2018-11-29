using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ControllerManager {

    public enum Tool { TELEPORTER = 0, PROPULSER = 1, HAND = 2, TRASH = 3, CATALOG = 4 };

    private void hideToolAsset(Tool tool)
    {
        GameObject toolAsset = getToolAsset(tool);
        toolAsset.SetActive(false);
    }

    private void showToolAsset(Tool tool)
    {
        GameObject toolAsset = getToolAsset(tool);
        toolAsset.SetActive(true);
    }

    private void moveToolAsset(Tool tool, int xTransform, int yTransform, int zTransform)
    {
        GameObject toolAsset = getToolAsset(tool);
        Debug.Log(toolAsset.name);
        toolAsset.transform.position = new Vector3(xTransform, yTransform, zTransform);

    }

    private GameObject getToolAsset(Tool tool)
    {
        return GameObject.Find(getToolName(tool) + "Icon");
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
