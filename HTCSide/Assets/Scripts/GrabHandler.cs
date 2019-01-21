
using UnityEngine;

public class GrabHandler : MonoBehaviour
{
	private InputManager _inputManager;
	private GameObject player;
	private GameObject grabbedObject;
	private bool isOnDrag = false;
	
	private static readonly float CLICK_COOLDOWN_IN_SECOND = 0.5f;

    
	// Use this for initialization
	void Start ()
	{
		_inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
		player = GameObject.Find("RightTrigger");
		grabbedObject = null;
	}

	void Update()
	{
		if (!isOnDrag || !_inputManager.UserClick()) return;
		Debug.Log("Ok 4");
		grabbedObject.transform.parent = null;
		player.transform.DetachChildren();
		isOnDrag = false;
	}

	private void OnTriggerStay(Collider other)
	{
		grabbedObject = other.gameObject;
		if (!_inputManager.UserClick()) return;
		
		if (!grabbedObject.gameObject.CompareTag("Grabbable") || isOnDrag) return;
		
		Debug.Log("Ok 2");
		
		var localScale = grabbedObject.transform.localScale;
		grabbedObject.transform.parent = player.transform;
		grabbedObject.transform.localScale = localScale;
		
		isOnDrag = true;
	}

    public void CreateMeshCollider()
    {

    }
}

