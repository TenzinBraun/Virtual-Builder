
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR; //needs to be UnityEngine.VR in version before 2017.2

public class GrabHandler : MonoBehaviour
{
    private InputManager inputManager;
    public Vector3 ObjectGrabOffset;
    public float GrabDistance = 0.001f;
    public string GrabTag = "Grab";

    public float ThrowMultiplier = 1.5f;


    private GameObject currentObject;
    private GameObject lastSelected;
    private Vector3 lastFramePosition;
    private Vector3 lastFrameRotation;

    // Use this for initialization
    void Start()
    {
        currentObject = null;
        inputManager = this.GetComponent<InputManager>();
        lastFramePosition = this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //if we don't have an active object in hand, look if there is one in proximity
        if (currentObject == null)
        {
            //check for colliders in proximity
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, GrabDistance);
            if (colliders.Length > 0)
            {
                lastSelected = colliders[0].gameObject;
                lastSelected.GetComponent<Renderer>().material.shader = Shader.Find("Particles/Multiply (Double)");

                //if there are colliders, take the first one if we press the grab button and it has the tag for grabbing
                if (inputManager.IsTriggerClicked() && colliders[0].transform.CompareTag(GrabTag))
                {
                    //set current object to the object we have picked up
                    currentObject = colliders[0].gameObject;

                    currentObject.GetComponent<Rigidbody>().mass = 10000;
                    currentObject.GetComponent<Rigidbody>().useGravity = false;
                    currentObject.transform.parent = this.transform;
                }
            }
        }
        else
        //we have object in hand, update its position with the current hand position
        {
            //if we we release grab button, release current object
            if (!inputManager.IsTriggerClicked())
            {
                currentObject.transform.parent = null;
                //set grab object to non-kinematic (enable physics)

                currentObject.GetComponent<Rigidbody>().useGravity = true;

                //calculate the hand's current velocity
                Vector3 CurrentVelocity = (transform.position - lastFramePosition) / Time.deltaTime;
                Vector3 CurrentAngularVelocity = (transform.eulerAngles - lastFrameRotation) / Time.deltaTime;

                //set the grabbed object's velocity to the current velocity of the hand
                currentObject.GetComponent<Rigidbody>().velocity = CurrentVelocity * ThrowMultiplier;
                //currentObject.GetComponent<Rigidbody>().ang = CurrentAngularVelocity;

                currentObject.GetComponent<Rigidbody>().mass = 1;

                //release the reference
                currentObject = null;
            }
            else
            {
                currentObject.GetComponent<Rigidbody>().velocity *= 0;
                currentObject.GetComponent<Rigidbody>().angularVelocity *= 0;
            }

        }
        //save the current position for calculation of velocity in next frame
        lastFramePosition = transform.position;
        lastFrameRotation = transform.eulerAngles;
    }
}


