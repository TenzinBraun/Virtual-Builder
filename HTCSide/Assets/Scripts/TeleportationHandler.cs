using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationHandler : MonoBehaviour {


    private RayCast rayCast;
    private InputManager inputManager;
    private GameObject player;

    void Start()
    {

        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (inputManager.UserClick())
        {
            Teleport(rayCast.GetHit());
            inputManager.CanClick = false;

        }
    }

    public void Teleport(RaycastHit hit)
    {
        Vector3 newPlayerPos = hit.point;
        newPlayerPos.y = player.transform.position.y;

        player.transform.position = newPlayerPos;
    }
}
