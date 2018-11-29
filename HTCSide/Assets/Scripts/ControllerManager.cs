using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour {

    private ControllerManager secondController;
    private InputManager inputManager;

    Tools tools;
    string currentTool; 
    bool choosingTool = false;

	// Use this for initialization
	void Start () {
        //getSecondController();

        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

        tools = new Tools();
        Enum enumTools=tools.

        //setDefaultCurrentTool();

        initDisplayMenu();

    }


    // Update is called once per frame
    void Update () {
        /*if (inputManager.isGrabClicked() && !choosingTool && !secondController.isChoosingTool())
        {
            displayMenu();
            choosingTool = true;
        }
        if (!inputManager.isGrabClicked() && choosingTool)
        {
            hideMenu();
            if (getSelectedTool() != null)
            {
                setCurrentTool(getSelectedTool());
                choosingTool = false;
            }

        }*/
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

    private void initDisplayMenu()
    {
        hideMenuAssets();
        
    }

    private void hideMenuAssets()
    {
        for (int i = 0; i < numberOfTool(); i++)
        {
            hideToolAsset((Tool)i);
        }
    }

    





    private void displayMenu()
    {
        throw new NotImplementedException();
    }

    private void hideMenu()
    {
        throw new NotImplementedException();
    }
    private object getSelectedTool()
    {
        throw new NotImplementedException();
    }
    private bool isChoosingTool()
    {
        throw new NotImplementedException();
    }

    private void setCurrentTool(object v)
    {
        throw new NotImplementedException();
    }

    

}
