using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour {

    private Camera myCamera = null;
	// Use this for initialization
	void Start () {
        this.myCamera = this.GetComponent<Camera>();
        //Debug.Log();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
