using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHandler : MonoBehaviour {

    private LineRenderer laserLine;
    private RayCast rayCast;

    private GameObject cylinder;

    

    // Use this for initialization
    void Start () {
        laserLine = GetComponent<LineRenderer>();
        rayCast = GameObject.Find("LeftController").GetComponent<RayCast>();

        InitCylinderPointer();
    }
	
	// Update is called once per frame
	void Update () {
        if (rayCast.Hit())
        {
            laserLine.enabled = true;
            
            UpdateLaserAndPointerPos(rayCast.GetHit());
            UpdateLaserAndPointerColor();
        }
        else
        {
            laserLine.enabled = false;
        }

    }

    private void UpdateLaserAndPointerPos(RaycastHit hit)
    {

        laserLine.SetPosition(1, hit.point);
        laserLine.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z));

        cylinder.transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
        cylinder.GetComponent<Renderer>().enabled = true;

        UpdateLaserAndPointerColor();
    }

    private void UpdateLaserAndPointerColor()
    {
        cylinder.GetComponent<Renderer>().material.color = Color.red;
    }

    private void InitCylinderPointer()
    {
        cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.localScale = new Vector3(0.3f, 0.001f, 0.3f);

        cylinder.GetComponent<Collider>().enabled = false;
        cylinder.GetComponent<MeshRenderer>().allowOcclusionWhenDynamic = false;
        cylinder.GetComponent<MeshRenderer>().receiveShadows = false;
        cylinder.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }
}
