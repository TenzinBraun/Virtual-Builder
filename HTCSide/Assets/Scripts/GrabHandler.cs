
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GrabHandler : MonoBehaviour
{
    private InputManager inputManager;
    public Vector3 ObjectGrabOffset;
    public float GrabDistance = 0.001f;
    public string GrabTag = "Grab";
    public float ThrowMultiplier = 1.5f;
    private float rescaleMultiplier = 1.01f;

    private GameObject grabbedObject;
    private GameObject lastObjectInRange;
    private Vector3 lastFramePosition;
    private Vector3 lastFrameRotation;


    void Start()
    {

    }

    public void OnEnable()
    {
        inputManager = this.GetComponent<InputManager>();
        grabbedObject = null;
        lastFramePosition = this.transform.position;
    }

    public void OnDisable()
    {
        inputManager = null;
        grabbedObject = null;
    }

    void Update()
    {
        updateObjectInRange();

        if (!grabbing())
        {
            if (inputManager.IsTriggerClicked())
                grabLastObjectInRange();
        }
        else
        {
            if (!inputManager.IsTriggerClicked())
                releaseGrabbedObject();
            else if (inputManager.isTrackpadBottomTouched())
                grabbedObject.transform.localScale /= rescaleMultiplier;
            else if (inputManager.isTrackpadTopTouched())
                grabbedObject.transform.localScale *= rescaleMultiplier;
            else
            makeGrabbedObjectStill();
        }
        updateLastFramePositionAndRotation();
    }

    private bool grabbing()
    {
        return (grabbedObject != null);
    }

    private void updateObjectInRange()
    {
        GameObject inRange = getObjectInRange();

        if (lastObjectInRange != inRange)
        {
            setStandardObjectShader(lastObjectInRange);
        }

        

        if (isGrabbable(inRange))
            lastObjectInRange = inRange;
        else
            lastObjectInRange = null;

        setSelfIlluminObjectShader(lastObjectInRange);
    }

    public GameObject getObjectInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, GrabDistance);
        if (colliders.Length > 0)
            return colliders[0].gameObject;
        else
            return null;
    }

    private void setStandardObjectShader(GameObject toShade)
    {
        setObjectShader(toShade, "Standard");
    }

    private void setSelfIlluminObjectShader(GameObject toShade)
    {
        setObjectShader(toShade, "Legacy Shaders/Self-Illumin/Diffuse");
    }

    private void setObjectShader(GameObject toShade, string shaderPath)
    {
        if (toShade != null)
            toShade.GetComponent<Renderer>().material.shader = Shader.Find(shaderPath);
    }

    private bool isGrabbable(GameObject toTest)
    {
        if (toTest == null)
            return false;
        else
            return toTest.transform.CompareTag(GrabTag);
    }

    private void grabLastObjectInRange()
    {
        //if(this.GetComponent<ControllerManager>().getSecondController().gameObject.GetComponent<GrabHandler>().is)
        grabbedObject = lastObjectInRange;
        setStandardObjectShader(grabbedObject);
        grabbedObject.GetComponent<Rigidbody>().mass = 10000;
        grabbedObject.GetComponent<Rigidbody>().useGravity = false;
        grabbedObject.transform.parent = this.transform;
    }

    public void releaseGrabbedObject()
    {
        if (grabbedObject != null)
        {
            if(grabbedObject.transform.parent == this.transform)
                grabbedObject.transform.parent = null;

            grabbedObject.GetComponent<Rigidbody>().useGravity = true;

            grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            Vector3 CurrentVelocity = getCurrentVelocity();
            Vector3 CurrentAngularVelocity = getCurrentAngularVelocity();
            setGrabbedObjectVelocity(CurrentVelocity * ThrowMultiplier);
            //setGrabbedObjectAngularVelocity(CurrentAngularVelocity);

            grabbedObject.GetComponent<Rigidbody>().mass = 1;
            grabbedObject = null;
        }
    }

    private Vector3 getCurrentVelocity()
    {
        return (transform.position - lastFramePosition) / Time.deltaTime;
    }

    private Vector3 getCurrentAngularVelocity()
    {
        return (transform.eulerAngles - lastFrameRotation) / Time.deltaTime;
    }

    private void setGrabbedObjectVelocity(Vector3 velocity)
    {
        grabbedObject.GetComponent<Rigidbody>().velocity = velocity;
    }

    private void setGrabbedObjectAngularVelocity(Vector3 angularVelocity)
    {
        grabbedObject.GetComponent<Rigidbody>().angularVelocity = angularVelocity;
    }

    private void makeGrabbedObjectStill()
    {
        grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //setGrabbedObjectVelocity(new Vector3(0, 0, 0));
        //setGrabbedObjectAngularVelocity(new Vector3(0, 0, 0));
    }

    private void updateLastFramePositionAndRotation()
    {
        lastFramePosition = this.transform.position;
        lastFrameRotation = this.transform.eulerAngles;
    }

    public GameObject getCurrentObject()
    {
        return grabbedObject;
    }
}


