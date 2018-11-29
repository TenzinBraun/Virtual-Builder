using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {


    private static readonly string CLICKED_LEFT_TRIGGER_NAME = "TriggerLeft";
    private static readonly string CLICKED_RIGHT_TRIGGER_NAME = "TriggerRight";

    private RayCast rayCast;


    private bool canClick;
    private static readonly float CLICK_COOLDOWN_IN_SECOND = 0.2f;

    void Start()
    {
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        CanClick = true;
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

    internal bool isGrabClicked()
    {
        throw new NotImplementedException();
    }

    public bool UserClick()
    {
        return (IsLeftTriggerClicked() || IsRightTriggerClicked()) && rayCast.Hit();
    }
}
