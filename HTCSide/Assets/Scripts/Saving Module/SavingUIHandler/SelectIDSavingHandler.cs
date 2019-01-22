using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectIDSavingHandler : MonoBehaviour
{
	
	private RayCast _rayCast;
	private InputManager _inputManager;
	
	
	// Use this for initialization
	void Start ()
	{
		_rayCast = GameObject.Find("LeftController").GetComponent<RayCast>();
		_inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!_inputManager.UserClick()) return;
		if (_rayCast.GetHit().transform.name == "SavingButtonDown")
		{
			_inputManager.CanClick = false;
			Text currentLetterSelection =
				_rayCast.GetHit().transform.parent.Find("LetterView").GetComponentInChildren<Text>();
			currentLetterSelection.text = GetNextChar(currentLetterSelection.text[0]).ToString();
		}

		if (_rayCast.GetHit().transform.name == "SavingButtonUp")
		{
			_inputManager.CanClick = false;
			Text currentLetterSelection =
				_rayCast.GetHit().transform.parent.Find("LetterView").GetComponentInChildren<Text>();
			currentLetterSelection.text = GetPreviousChar(currentLetterSelection.text[0]).ToString();
		}
	}

	private char GetNextChar(char currentChar)
	{
		var ascii = (++currentChar - 65) % 26 + 65;

		return (char)ascii;
	}
	
	private char GetPreviousChar(char currentChar)
	{
		var ascii = (--currentChar - 65);

		if (ascii < 0)
		{
			ascii += 26;
		}
		ascii = ascii % 26 + 65;

		return (char)ascii;
	}

	public string GetID()
	{
		var selectedID = "";
		for (int i = 0; i < transform.childCount; i++)
		{
			if (transform.GetChild(i).name == "LetterSelector")
			{
				selectedID += transform.GetChild(i).Find("LetterView").GetComponent<Text>().text;
			}
		}
		return selectedID;
	}

	public string SetID(string id)
	{
		for (int i = 0; i < id.Length; i++)
		{
			if (transform.GetChild(i).name == "LetterSelector")
			{
				transform.GetChild(i).Find("LetterView").GetComponent<Text>().text = id[i].ToString();
			}
		}
	}
}
