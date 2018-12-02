using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class ControllerManager : MonoBehaviour {
    //Partial class -> Tool declaration & methods in Tools.cs
    
    private InputManager inputManager;
    private ControllerManager secondController;

    String currentTool; 
    bool choosingTool = false;
    bool vrMode;

	// Use this for initialization
	void Start () {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

        getSecondController();

        setDefaultCurrentTool();
        initToolAssets();

        hideMenuAssets();
    }


    // Update is called once per frame
    void Update () {
        if (Grab()&&!choosingTool)
        {
            displayMenu();
            choosingTool = true;
        }
       if (!Grab() && choosingTool)
        {
            hideMenuAssets();
            if (getSelectedTool() != -1)
            {
                setCurrentTool(getSelectedTool());
                Debug.Log(currentTool);
            }
            choosingTool = false;
        }
	}

    private void getSecondController()
    {
        if (name.Equals("RightController"))
        {
            secondController = GameObject.Find("LeftController").GetComponent<ControllerManager>();
        } 
        else if (name.Equals("LeftController"))
        {
            secondController = GameObject.Find("RightController").GetComponent<ControllerManager>();
        }
    }

    private void setDefaultCurrentTool()
    {
        if (name.Equals("RightController"))
        {
            setCurrentTool(Tool.HAND);
        }
        else if (name.Equals("LeftController"))
        {
            setCurrentTool(Tool.TELEPORTER);
        }
    }

    private void hideMenuAssets()
    {
        for (int i = 0; i < numberOfTool(); i++)
        {
            hideToolAsset(i);
        }
    }
    private void showMenuAssets()
    {
        for (int i = 0; i < numberOfTool(); i++)
        {
            showToolAsset(i);
        }
    }

    private bool Grab()
    {
        if (secondController != null)
            return inputManager.UserGrip() && !choosingTool && !secondController.isChoosingTool();
        else
            return inputManager.UserGrip();
    }

    private void displayMenu()
    {
        showMenuAssets();
        initMenuAssetsPosition();
    }

    private void initMenuAssetsPosition()
    {
        for (int i = 0; i < numberOfTool(); i++)
        {
            moveToolAsset(i, 0, 0, i);
        }
    }

    private int getSelectedTool()
    {
        return getToolAssetIndice(inputManager.selectedTool());
    }

    private bool isChoosingTool()
    {
        return choosingTool;
    }

    private void setCurrentTool(int toolIndex)
    {
        currentTool = getToolName((Tool)toolIndex);
    }

    private void setCurrentTool(Tool Tool)
    {
        currentTool = getToolName(Tool);
    }
    

}
