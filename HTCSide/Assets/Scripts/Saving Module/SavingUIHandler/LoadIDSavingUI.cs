using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadIDSavingUI : MonoBehaviour
{

	private static readonly string SUCCESS_MESSAGE = "Your scene has been loaded";
	private static readonly string FAIL_MESSAGE = "Problem while loading the scene";
	private static readonly string CONFIRM_RESET_MESSAGE = "Are you sure you want to delete all the objects inside the scene";
	private static readonly string RESET_MESSAGE = "All the objects has been deleted";

	private RayCast _rayCast;
	private InputManager _inputManager;
	private SaveManager _saveManager;

	private GameObject _loadedObject;
	private SelectIDSavingHandler _selectIdSavingHandler;

	// Use this for initialization
	private void Start ()
	{
		_rayCast = GameObject.Find("LeftController").GetComponent<RayCast>();
		_inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
		_saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();

		_loadedObject = GameObject.Find("GameObject");
		_selectIdSavingHandler =
			transform.GetChild(0).Find("IdSelectorUI").GetComponentInChildren<SelectIDSavingHandler>();

	}
	
	// Update is called once per frame
	private void Update ()
	{
		if (!_inputManager.UserClick()) return;
		if (_rayCast.GetHit().transform.name == "Reset")
		{
			_inputManager.CanClick = false;
			deleteLoadedObjects();
			//PopUp.DisplayValidationPopUp(gameObject, MESSAGE_RESET_CONFIRM, resetButtonCallback);
		}
		else if(_rayCast.GetHit().transform.name == "Load")
		{
			_inputManager.CanClick = false;
			if (_saveManager.LoadGameObject(_selectIdSavingHandler.GetID()))
			{
				//PopUp.DisplayValidationPopUp(gameObject, SUCCESS_MESSAGE, 2);
			}
			else
			{
				//PopUp.DisplayValidationPopUp(gameObject, FAIL_MESSAGE, 3);
			}
		}
	}
	
	private void deleteLoadedObjects()
	{
		for (int i = 0; i < _loadedObject.transform.childCount; i++)
		{
			Transform table = _loadedObject.transform.GetChild(i);
			for (int j = 0; j < table.childCount; j++)
			{
				Destroy(table.GetChild(j));
				
			}
		}
		//furnitureUIHandler.UpdateRightUIPart(0);

		//PopUp.DisplayScheduledPopUp(gameObject, MESSAGE_RESET, 3);
	}
}
