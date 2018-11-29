using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools {

    public enum enumTools { TELEPORTER = 0, PROPULSER = 1, HAND = 2, TRASH = 3, CATALOG = 4 };

    public Tools()
    {

    }

    private void hideToolAsset(Tools tool)
    {
        GameObject toolAsset = getToolIcon(tool);
        toolAsset.transform.position = new Vector3(0, 1, 0);
        toolAsset.SetActive(false);
    }

    private GameObject getToolIcon(Tools tool)
    {
        return GameObject.Find(getToolName(tool) + "Icon");
    }

    private int numberOfTool()
    {
        return Enum.GetNames(typeof(Tools)).Length;
    }

    String getToolName(Tools tool)
    {
        return getCamelCase(Enum.GetName(typeof(Tools), tool));
    }

    private String getCamelCase(String toCamelCase)
    {
        String result = toCamelCase.Substring(0, 1).ToUpper();
        result += toCamelCase.Substring(1, toCamelCase.Length - 1).ToLower();
        return result;

    }



}
