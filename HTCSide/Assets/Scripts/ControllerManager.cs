using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class ControllerManager : MonoBehaviour {
    //Partial class -> Tool declaration & methods in Tools.cs
    
    private InputManager inputManager;
    private ControllerManager secondController;
    private RayCast rayCast;

    private GameObject menu;

    string currentTool;
    GameObject currentToolIcon;

    bool choosingTool = false;
    bool vrMode;

	// Use this for initialization
	void Start () {
        inputManager = this.GetComponent<InputManager>();
        setSecondController();
        rayCast = GameObject.Find(name).GetComponent<RayCast>();
        
        getControllerMenu();

        initToolAssets();
        initMenuAssetsPosition();

        setDefaultCurrentTool();
        
        hideMenuAssets();
    }
    

    // Update is called once per frame
    void Update () {
        if (Grab() && !choosingTool && !secondController.isChoosingTool() && !inputManager.IsTriggerClicked())
        {
            makePlayerStatic();
            disableCurrentTool();
            displayMenu();
            choosingTool = true;
        }
            

        if (!Grab() && choosingTool)
        {
            if (getSelectedTool() != -1)
                setCurrentTool(getSelectedTool());

            hideMenu();
        }

       if(!choosingTool && currentTool!=null)
            updateCurrentToolPosition();
    }

    private void setSecondController()
    {
        if (name.Equals("RightController"))
            secondController = GameObject.Find("LeftController").GetComponent<ControllerManager>();
        else if (name.Equals("LeftController"))
            secondController = GameObject.Find("RightController").GetComponent<ControllerManager>();

        if (secondController != null)
            vrMode = true;
        else
            vrMode = false;
    }

    private void getControllerMenu()
    {
        if (vrMode)
        {
            if (name.Equals("RightController"))
                menu = GameObject.Find("RightMenu");
            else if (name.Equals("LeftController"))
                menu = GameObject.Find("LeftMenu");
        } else
        {
            menu = GameObject.Find("Menu");
        }
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
        return inputManager.UserGrip();
    }

    

    private void displayMenu()
    {
        showMenuAssets();
        initMenuPostition();
    }
    
    private void showMenuAssets()
    {
        for (int i = 0; i < numberOfTool(); i++)
            showToolAsset(i);
    }

    private void initMenuPostition()
    {

        if (vrMode)
        {
            menu.transform.position = this.transform.position;
            menu.transform.position += this.transform.forward * 0.2f;
            menu.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            menu.transform.rotation = new Quaternion(0, 0, 0, 0);
            menu.transform.Rotate(new Vector3(0, this.transform.rotation.eulerAngles.y,0), Space.Self);
        } 
        else     
        {
            menu.transform.position = rayCast.GetHit().point;
            menu.transform.Rotate(new Vector3(0, 0, 0), Space.World);
        }
    }
        

    private void initMenuAssetsPosition()
    {
        for (int i = 0; i < numberOfTool(); i++)
            moveToolAsset(i, 0f, 0f, i-2f);
    }

    

    private int getSelectedTool()
    {
        return getToolAssetIndice(inputManager.selectedTool(vrMode,name));
    }

    private bool isChoosingTool()
    {
        return choosingTool;
    }

    private void setCurrentTool(int toolIndex)
    {
        currentTool = getToolName((Tool)toolIndex);

        enableCurrentToolScript();

        showCurrentTool();
    }

  

    private void setCurrentTool(Tool Tool)
    {
        currentTool = getToolName(Tool);

        enableCurrentToolScript();

        showCurrentTool();
    }

    private void makePlayerStatic()
    {
        GameObject.Find("Player").GetComponent<Rigidbody>().velocity *= 0;
    }

    private void disableCurrentTool()
    {
        disableCurrentToolScripts();
        destroyCurrentToolIcon();
    }

    private void disableCurrentToolScripts()
    {
        if (currentTool == getToolName(Tool.CATALOG))
        {
            this.GetComponent<CatalogHandler>().enabled = false;

            this.GetComponent<LineRenderer>().enabled = false;
            this.GetComponent<LaserHandler>().enabled = false;
        }

        if (currentTool == getToolName(Tool.HAND))
        {
            this.GetComponent<GrabHandler>().enabled = false;
        }

        if (currentTool == getToolName(Tool.PROPULSER))
        {
            this.GetComponent<PropulserHandler>().enabled = false;
            
            GameObject.Find("Player").GetComponent<Rigidbody>().mass = 10000;
        }

        if (currentTool == getToolName(Tool.TELEPORTER))
        {
            this.GetComponent<TeleportationHandler>().enabled = false;

            this.GetComponent<LineRenderer>().enabled = false;
            this.GetComponent<LaserHandler>().enabled = false;
        }

        if (currentTool == getToolName(Tool.TRASH))
        {
            this.GetComponent<TrashHandler>().leaveTrash();
            this.GetComponent<TrashHandler>().enabled = false;

            this.GetComponent<LineRenderer>().enabled = false;
            this.GetComponent<LaserHandler>().enabled = false;
        }
    }

    private void enableCurrentToolScript()
    {
        disableAllScripts();

        if (currentTool == getToolName(Tool.CATALOG))
        {
            this.GetComponent<CatalogHandler>().enabled = true;

            this.GetComponent<LaserHandler>().enabled = true;
            this.GetComponent<LineRenderer>().enabled = true;

            this.GetComponent<LaserHandler>().setColor(Color.magenta);

            this.GetComponent<CatalogHandler>().DropCatalog();
        }

        if (currentTool == getToolName(Tool.HAND))
        {
            this.GetComponent<GrabHandler>().enabled = true;
        }

        if (currentTool == getToolName(Tool.PROPULSER))
        {
            this.GetComponent<PropulserHandler>().enabled = true;
            GameObject.Find("Player").GetComponent<Rigidbody>().mass = 1;
        }

        if (currentTool == getToolName(Tool.TELEPORTER))
        {
            this.GetComponent<TeleportationHandler>().enabled = true;
            
            this.GetComponent<LineRenderer>().enabled = true;
            this.GetComponent<LaserHandler>().enabled = true;

            this.GetComponent<LaserHandler>().setColor(Color.green);
        }

        if (currentTool == getToolName(Tool.TRASH))
        {
            this.GetComponent<TrashHandler>().enabled = true;

            this.GetComponent<LineRenderer>().enabled = true;
            this.GetComponent<LaserHandler>().enabled = true;
            
            this.GetComponent<LaserHandler>().setColor(Color.red);

        }
    }

    private void disableAllScripts()
    {

        this.GetComponent<CatalogHandler>().enabled = false;
        this.GetComponent<GrabHandler>().enabled = false;
        this.GetComponent<PropulserHandler>().enabled = false;
        this.GetComponent<TeleportationHandler>().enabled = false;
        this.GetComponent<TrashHandler>().enabled = false;

        this.GetComponent<LaserHandler>().enabled = false;
        this.GetComponent<LineRenderer>().enabled = false;

    }

    private void showCurrentTool()
    {
       initCurrentToolIcon();
    }

    public string getCurrentTool()
    {
        return currentTool;
    }

    public ControllerManager getSecondController()
    {
        return secondController;
    }

}
