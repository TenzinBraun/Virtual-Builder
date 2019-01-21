using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogHandler : MonoBehaviour {

    private GameObject catalog;

	// Use this for initialization
	void Start ()
    {
        catalog = GameObject.Find("Catalog");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject getCatalog()
    {
        return catalog;
    }
}
