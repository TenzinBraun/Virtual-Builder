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

        setSecondController();

        setDefaultCurrentTool();
        initToolAssets();

        hideMenuAssets();
    }


    // Update is called once per frame
    void Update () {
       if (Grab() && !choosingTool)
            displayMenu();

       if (!Grab() && choosingTool)
        {
            hideMenu();

            if (getSelectedTool() != -1)
                setCurrentTool(getSelectedTool());
        }

       if(!choosingTool && currentTool!=null)
            updateCurrentToolPosition(currentTool);
    }

    private void setSecondController()
    {
        if (name.Equals("RightController"))
            secondController = GameObject.Find("LeftController").GetComponent<ControllerManager>();
        else if (name.Equals("LeftController"))
            secondController = GameObject.Find("RightController").GetComponent<ControllerManager>();

        if (secondController == null)
            vrMode = true;
        else
            vrMode = false;
    }

    private void setDefaultCurrentTool()
    {
        if (name.Equals("RightController"))
            setCurrentTool(Tool.HAND);
        else if (name.Equals("LeftController"))
            setCurrentTool(Tool.TELEPORTER);
    }

    private void hideMenu()
    {
        choosingTool = false;
        hideMenuAssets();
    }

    private void hideMenuAssets()
    {
        for (int i = 0; i < numberOfTool(); i++)
            hideToolAsset(i);
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
        choosingTool = true;
        showMenuAssets();
        initMenuAssetsPosition();
    }

    private void showMenuAssets()
    {
        for (int i = 0; i < numberOfTool(); i++)
            showToolAsset(i);
    }

    private void initMenuAssetsPosition()
    {
        for (int i = 0; i < numberOfTool(); i++)
            moveToolAsset(i, 0, 0, i);
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
        showCurrentTool();
    }

    private void setCurrentTool(Tool Tool)
    {
        currentTool = getToolName(Tool);
        showCurrentTool();
    }

    private void showCurrentTool()
    {
        if (vrMode)
        {
            updateCurrentToolPosition(currentTool);
        } else
        {

        }
    }
}
