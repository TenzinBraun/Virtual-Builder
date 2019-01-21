﻿using UnityEngine;

public class RayCast : MonoBehaviour {

    public GameObject source;
 
    private RaycastHit hit;
    private bool rayCastHit;
  
    void Update()
    {
        rayCastHit = Physics.Raycast(source.transform.position, source.transform.forward, out hit);
    }

    // Public Interface

    public bool Hit()
    {
        return rayCastHit;
    }
    public bool HitFurniture()
    {
        return hit.transform.CompareTag("Grabbable");
    }

    public bool HitMenu()
    {
        return hit.transform.CompareTag("MenuContainer");
    }

    public RaycastHit GetHit()
    {
        return hit;
    }

    public GameObject GetSource()
    {
        return source;
    }
}
