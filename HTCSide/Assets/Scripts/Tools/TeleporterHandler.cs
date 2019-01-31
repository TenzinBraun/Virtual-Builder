using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterHandler : ToolsHandler {


    private RayCast rayCast;
    private InputManager inputManager;
    private GameObject player;

    void Start()
    {
        rayCast = this.GetComponent<RayCast>();
        inputManager = this.GetComponent<InputManager>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (enabled)
        {
            if (inputManager.IsTriggerClicked())
            {
                Teleport(rayCast.GetHit());
                inputManager.CanClick = false;
            }
        }
    }

    override
    public void enable()
    {
        enabled = true;
        this.GetComponent<LineRenderer>().enabled = true;
        this.GetComponent<LaserHandler>().enabled = true;
    }

    override
    public void disable()
    {
        enabled = false;
        this.GetComponent<LineRenderer>().enabled = false;
        this.GetComponent<LaserHandler>().enabled = false;
    }

    public void Teleport(RaycastHit hit)
    {
        Vector3 newPlayerPos = hit.point;
        //newPlayerPos.y = player.transform.position.y;

        player.transform.position = newPlayerPos;
    }
}
