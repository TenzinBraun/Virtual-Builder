﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {


    private static readonly string CLICKED_LEFT_TRIGGER_NAME = "TriggerLeft";
    private static readonly string CLICKED_RIGHT_TRIGGER_NAME = "TriggerRight";
    private static readonly string CLICKED_LEFT_GRIP_NAME = "GripLeft";
    private static readonly string CLICKED_RIGHT_GRIP_NAME = "GripRight";

    private RayCast rayCast;


    private bool canClick;
    private bool canGrip;
    private static readonly float CLICK_COOLDOWN_IN_SECOND = 0.2f;

    void Start()
    {
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        canClick = true;
        canGrip = true;
    }

    

    public bool CanClick
    {
        set
        {
            if (!value) Invoke("EnableClick", CLICK_COOLDOWN_IN_SECOND);

            canClick = value;

        }
        get
        {
            return canClick;

        }
    }

    private void EnableClick()
    {
        canClick = true;
    }

    public bool IsRightTriggerClicked()
    {
        return CanClick && (Input.GetAxis(CLICKED_RIGHT_TRIGGER_NAME) == 1 || Input.GetButton("LeftClick"));
    }

    public bool IsLeftTriggerClicked()
    {
        return CanClick && (Input.GetAxis(CLICKED_LEFT_TRIGGER_NAME) == 1 || Input.GetButton("LeftClick"));
    }

    public bool IsRightGripClicked()
    {
        return canGrip && (Input.GetAxis(CLICKED_RIGHT_GRIP_NAME) == 1 );

    }

    public bool IsLeftGripClicked()
    {
        return canGrip && (Input.GetAxis(CLICKED_LEFT_GRIP_NAME) == 1);
    }

    internal bool UserGrip()
    {
        if (IsLeftGripClicked() || IsRightGripClicked() || Input.GetKeyDown(KeyCode.G))
        {
                canGrip = false;
                return true;   
        }
        else
        {
            canGrip = true;
            return false;
        }
    }

    public bool UserClick()
    {
        return (IsLeftTriggerClicked() || IsRightTriggerClicked()) && rayCast.Hit();
    }
}
