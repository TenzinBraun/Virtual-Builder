using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {


    private static readonly string CLICKED_LEFT_TRIGGER_NAME = "LeftTrigger";
    private static readonly string CLICKED_RIGHT_TRIGGER_NAME = "RightTrigger";

    private bool canClick;
    private static readonly float CLICK_COOLDOWN_IN_SECOND = 0.2f;

    void Start()
    {
        CanClick = true;
    }

    void Update()
    {

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
        return CanClick && (Input.GetAxis(CLICKED_RIGHT_TRIGGER_NAME) == 1);
    }

    public bool IsLeftTriggerClicked()
    {
        return CanClick && (Input.GetAxis(CLICKED_LEFT_TRIGGER_NAME) == 1);
    }
}
