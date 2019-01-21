<<<<<<< HEAD
﻿using UnityEngine;
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR; //needs to be UnityEngine.VR in version before 2017.2
>>>>>>> menu

public class GrabHandler : MonoBehaviour
{

<<<<<<< HEAD
	private InputManager _inputManager;
	private RayCast _rayCast;
	private GameObject _grabbedGameObject;

	private bool _isOnDrag;
	private bool _isClicked;
	

    
	// Use this for initialization
	void Start ()
	{
		_inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
		_rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();

		_isClicked = _isOnDrag = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (_inputManager.UserClick())
		{
			if (_isOnDrag)
			{
				_inputManager.CanClick = false;

				_grabbedGameObject.GetComponent<Collider>().enabled = true;
				_grabbedGameObject = null;

				_isOnDrag = false;
			}
			else if(_rayCast.HitFurniture() && !_isClicked && !_isOnDrag)
			{
				_inputManager.CanClick = false;
				_grabbedGameObject = GameObject.Find(_rayCast.GetHit().transform.name);
				_isOnDrag = _isClicked = true;
			}
			else if (_isClicked && !_isOnDrag) // UnSelect Game Object
			{
				_inputManager.CanClick = false;

				_isClicked = false;
			}                              
		}
		if (_isOnDrag)
		{
			UpdateGrabbedObjectPosition(_rayCast.GetHit());
			
		}
	}

	private void UpdateGrabbedObjectPosition(RaycastHit getHit)
	{
		HandleMainCollision(getHit);
		HandleAdvancedCollision();
	}

	private void HandleMainCollision(RaycastHit getHit)
	{
		var newPos = getHit.point;

		var size = _grabbedGameObject.GetComponentInChildren<Renderer>().bounds.size / 2 + (0.025f * Vector3.one);
		size.Scale(getHit.normal);
       
		newPos += size;

		if (getHit.normal != Vector3.up)
			newPos.y = _grabbedGameObject.GetComponentInChildren<Renderer>().bounds.size.y / 2 + 0.0820f;

		_grabbedGameObject.transform.position = newPos;
	}
	
	private void HandleAdvancedCollision()
	{
		var colliderIntersection = Physics.OverlapBox(_grabbedGameObject.transform.position,
			_grabbedGameObject.GetComponentInChildren<Renderer>().bounds.size / 2,
			_grabbedGameObject.transform.rotation);

		foreach(var collide in  colliderIntersection)
		{
			if (collide.transform.parent == null || collide.transform.parent.parent == null) continue;
			if (collide.transform.parent.parent.name != "Grabble") continue;
			var newPos = _grabbedGameObject.transform.position;

			var temp = GetSharedDimension(collide);
			temp.y = 0;
			newPos -= temp;

			_grabbedGameObject.transform.position = newPos;
		}
	}
	/// <summary>
	/// Return dimmension common between collider and funiture in drag
	/// </summary>
	/// <param name="collide"></param>
	/// <returns></returns>
	private Vector3 GetSharedDimension(Collider collide)
	{
		var direction = _grabbedGameObject.transform.position - collide.transform.position;
		direction.y = 0;

		var furnitureSide = _grabbedGameObject.GetComponentInChildren<Renderer>().bounds.size / 2;
		furnitureSide.Scale(direction.normalized);
		var furnitureInsidePoint = _grabbedGameObject.transform.position - furnitureSide;

		var colliderSide = collide.bounds.size / 2;
		colliderSide.Scale(direction.normalized);
		Vector3 colliderInsidePoint = collide.transform.position + colliderSide;

		var correction = (Vector3.one * 0.025f);
		correction.Scale(direction);

		return furnitureInsidePoint - colliderInsidePoint - correction;
	}


}
=======
    public string InputName;
    private InputManager inputManager;
    public XRNode NodeType;
    public Vector3 ObjectGrabOffset;
    public float GrabDistance = 0.1f;
    public string GrabTag = "Grab";
    public float ThrowMultiplier = 1.5f;

    private Transform _currentObject;
    private Vector3 _lastFramePosition;

    // Use this for initialization
    void Start()
    {
        _currentObject = null;
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        _lastFramePosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //if we don't have an active object in hand, look if there is one in proximity
        if (_currentObject == null)
        {
            //check for colliders in proximity
            Collider[] colliders = Physics.OverlapSphere(transform.position, GrabDistance);
            if (colliders.Length > 0)
            {
                //if there are colliders, take the first one if we press the grab button and it has the tag for grabbing
                if (inputManager.IsRightTriggerClicked() && colliders[0].transform.CompareTag(GrabTag))
                {
                    //set current object to the object we have picked up
                    _currentObject = colliders[0].transform;

                    //if there is no rigidbody to the grabbed object attached, add one
                    if (_currentObject.GetComponent<Rigidbody>() == null)
                    {
                        _currentObject.gameObject.AddComponent<Rigidbody>();
                    }

                    //set grab object to kinematic (disable physics)
                    _currentObject.GetComponent<Rigidbody>().isKinematic = true;


                }
            }
        }
        else
        //we have object in hand, update its position with the current hand position (+defined offset from it)
        {
            _currentObject.position = transform.position + ObjectGrabOffset;

            //if we we release grab button, release current object
            if (Input.GetAxis(InputName) < 0.01f)
            {
                //set grab object to non-kinematic (enable physics)
                Rigidbody _objectRGB = _currentObject.GetComponent<Rigidbody>();
                _objectRGB.isKinematic = false;

                //calculate the hand's current velocity
                Vector3 CurrentVelocity = (transform.position - _lastFramePosition) / Time.deltaTime;

                //set the grabbed object's velocity to the current velocity of the hand
                _objectRGB.velocity = CurrentVelocity * ThrowMultiplier;

                //release the reference
                _currentObject = null;
            }

        }

        //save the current position for calculation of velocity in next frame
        _lastFramePosition = transform.position;


    }

    /*private GameObject currentlyGrabbed;
    private string grabTag = "Grab";
    private Vector3 grabOffset;

    void Start()
    {
        currentlyGrabbed = null;
    }

    void Update()
    {
        if(currentlyGrabbed != null)
        {

        }
    }

    private void updatePositionAndRotation()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (inputManager.IsLeftTriggerClicked() && collision.gameObject.CompareTag(grabTag))
        {
            currentlyGrabbed = collision.gameObject;
            currentlyGrabbed.transform.SetParent(GameObject.Find("LeftController").transform);
        }
    }*/


}
>>>>>>> menu
