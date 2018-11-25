using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour {

    ControllerManager secondController;
    InputManager inputManager;

    string currentTool;
    bool choosingTool = false;

	// Use this for initialization
	void Start () {
        getSecondController();
        setDefaultCurrentTool();
		
	}

    // Update is called once per frame
    void Update () {
        if (inputManager.isGrabClicked() && !choosingTool && !secondController.isChoosingTool())
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

        }
	}

    

    private void getSecondController()
    {
        throw new NotImplementedException();
    }
    private void setDefaultCurrentTool()
    {
        throw new NotImplementedException();
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
