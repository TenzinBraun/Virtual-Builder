using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHandler : MonoBehaviour {

    private GameObject lastObjectSelected;
    private GameObject lastObjectDestroyed;
    private GameObject objectInRange;

    private InputManager inputManager;
    private RayCast rayCast;

    private float trashRange = 0.001f;

	// Use this for initialization
	void Start () {
        lastObjectSelected = null;
        lastObjectDestroyed = null;

        this.inputManager = this.GetComponent<InputManager>();
        this.rayCast = this.GetComponent<RayCast>();
    }
	
	// Update is called once per frame
	void Update () {
        updateObjectInRange();

        if (objectInRange != null && objectInRange.CompareTag("Grab"))
        {
            if (objectInRange != lastObjectSelected)
            {
                updateLastObjectSelected(); 
            }
        }
        
        if (inputManager.IsTriggerClicked())
        {
            if(lastObjectDestroyed != null)
            {
                Destroy(lastObjectDestroyed);
            }

            if (lastObjectSelected != null)
            {
                inputManager.CanClick = false;
                destroyLastObjectSelected();
            }
            
        }

        if (inputManager.isTrackpadPressed())
        {
            undoLastDestruction();
        }
    }

    private void updateObjectInRange()
    {
        objectInRange = null;

        if (rayCast.Hit())
            objectInRange = rayCast.GetHit().collider.gameObject;
    }

    private void updateLastObjectSelected()
    {
        setLastSelectedObjectShaderStandard();
        lastObjectSelected = objectInRange;
        setLastSelectedObjectShaderSemiTransparent();
    }

    private void setLastSelectedObjectShaderStandard()
    {
        if (lastObjectSelected != null)
        {
            lastObjectSelected.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
        }
    }

    private void setLastSelectedObjectShaderSemiTransparent()
    {
        lastObjectSelected.GetComponent<Renderer>().material.shader = Shader.Find("Particles/Multiply (Double)");
    }

    private void destroyLastObjectSelected()
    {
        saveLastObjectDestroyed();
        Destroy(lastObjectSelected);
        lastObjectSelected = null;
    }

    private void saveLastObjectDestroyed()
    {
        lastObjectDestroyed = Instantiate(lastObjectSelected);
        lastObjectDestroyed.SetActive(false);
    }

    private void undoLastDestruction()
    {
        if (lastObjectDestroyed != null)
        {
            lastObjectDestroyed.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
            lastObjectDestroyed.SetActive(true);
            lastObjectDestroyed = null;
        }
    }

    public void OnDisable()
    {
        setLastSelectedObjectShaderStandard();
    }



}
