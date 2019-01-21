using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationHandler : MonoBehaviour {


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
        if (inputManager.IsTriggerClicked())
        {
            Teleport(rayCast.GetHit());
            inputManager.CanClick = false;
        }
    }

    public void Teleport(RaycastHit hit)
    {
        Vector3 newPlayerPos = hit.point;
        //newPlayerPos.y = player.transform.position.y;

        player.transform.position = newPlayerPos;
    }
}
