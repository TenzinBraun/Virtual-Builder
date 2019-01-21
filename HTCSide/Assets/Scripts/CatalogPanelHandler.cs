using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogPanelHandler : MonoBehaviour {

    private GameObject[] lastUsedElements;
	// Use this for initialization
	void Start () {
        lastUsedElements = new GameObject[6];

	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(GameObject.Find("Main Camera").transform);
        this.transform.Rotate(0, 180, 0);
	}
}
