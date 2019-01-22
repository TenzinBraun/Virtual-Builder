using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropulserHandler : MonoBehaviour {

    private InputManager inputManager;
    private GameObject player;
    public float propulsionForce = 10f;
    public float frixion = 1f;
    public float FOVMultiplyer = 5f;
	// Use this for initialization
	void Start()
    {
        this.inputManager = this.GetComponent<InputManager>();
        this.player = GameObject.Find("Player");
	}

    // Update is called once per frame
    void Update () {
        propulsionForce = calculateSpeed();
        
        StartCoroutine(changeFOV());
        if (inputManager.IsTriggerClicked())
        {
            player.GetComponent<Rigidbody>().AddForce(this.transform.forward * propulsionForce);
        }
        if(this.GetComponent<ControllerManager>().getSecondController().getCurrentTool() == "PROPULSER")
            player.GetComponent<Rigidbody>().AddForce(player.GetComponent<Rigidbody>().velocity * -1 * (frixion/2));
        else
            player.GetComponent<Rigidbody>().AddForce(player.GetComponent<Rigidbody>().velocity * -1 * frixion);

        endFrame();
    }

    private float calculateSpeed()
    {
        if (inputManager.getTrackpadPos() == 0)
        {
            return propulsionForce;
        } else
        {
            return 10f*(2 - (inputManager.getTrackpadPos() + 1));
        }
         
    }

    IEnumerator endFrame()
    {
        yield return null;
    }

    IEnumerator changeFOV()
    {
        yield return new WaitForEndOfFrame();
        GameObject.Find("Main Camera").GetComponent<Camera>().fieldOfView += FOVMultiplyer * player.GetComponent<Rigidbody>().velocity.magnitude;
        Debug.Log(GameObject.Find("Main Camera").GetComponent<Camera>().fieldOfView);
    }
}
