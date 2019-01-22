using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHandler : MonoBehaviour {

    private GameObject lastSelected;
    private InputManager inputManager;
    private RayCast rayCast;

    public float trashRange = 0.001f;

	// Use this for initialization
	void Start () {
        lastSelected = null;
        this.inputManager = this.GetComponent<InputManager>();
        this.rayCast = this.GetComponent<RayCast>();
    }
	
	// Update is called once per frame
	void Update () {
        GameObject inRange = null;

        if (rayCast.Hit())
            inRange = rayCast.GetHit().collider.gameObject;

        if (inRange != null && inRange.CompareTag("Grab"))
        {
            if (inRange != lastSelected)
            {
                resetLastSelected();
                lastSelected = inRange;
                lastSelected.GetComponent<Renderer>().material.shader = Shader.Find("Particles/Multiply (Double)");
            }
        }
        
        if (inputManager.IsTriggerClicked())
        {
            Destroy(lastSelected);
        }
    }

    private void resetLastSelected()
    {
        if (lastSelected != null)
        {
            lastSelected.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
        }
    }


    public void leaveTrash()
    {
        resetLastSelected();
    }



}
