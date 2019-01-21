using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR; //needs to be UnityEngine.VR in version before 2017.2

public class GrabHandler : MonoBehaviour
{
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
