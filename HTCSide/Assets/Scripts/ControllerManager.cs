using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour {

    private ControllerManager secondController;
    private InputManager inputManager;

    enum Tools {TELEPORTER,PROPULSER,HAND,TRASH,CATALOG};
    string currentTool;
    bool choosingTool = false;

	// Use this for initialization
	void Start () {
        getSecondController();

        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

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
            setCurrentTool(Tools.HAND);
        }
        else if (name.Equals("LeftController"))
        {
            setCurrentTool(Tools.TELEPORTER);
        }
    }

    private void displayMenu()
    {
        throw new NotImplementedException();
    }

    private GameObject getToolIcon(Tools tool)
    {
        //RECUPERER ASSETS DANS DOSSIER UNITY

        return null;
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
