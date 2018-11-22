 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControllerProtocol1 : MonoBehaviour {


    private static readonly Vector3 DEGRADED_MODEL_POSITION = new Vector3(0.463f, 1.212f, -0.906f);
    private static readonly Vector3 FULL_RESOLUTION_MODEL_POSITION = new Vector3(0.463f, 1.212f, 1.138f);
    private static readonly Vector3 SPAWN_POINT = new Vector3(100f, 100f, 100f);


    private string[] degradedModelExtensions;
    private string[] typeOfModel;

    private GameObject fullResolutionGameObject;
    private GameObject degradedResolutionGameObject;

    private string degradedGameObjectName;

    private int indicatorDegradedModel;
    private int indicatorFullResolution;

    private InputManager inputManager;


    // Use this for initialization
    void Start () {
        Initialize();
    }
	
	// Update is called once per frame
	void Update () {
        SwitchBeetwenModels(indicatorDegradedModel, indicatorFullResolution);
	}

    /// <summary>
    /// Initialize component
    /// </summary>
    private void Initialize()
    {
        degradedModelExtensions = new string[] { "0_5", "0_10", "0_15" };
        typeOfModel = new string[] { "longDinosaur", "horse", "Rabbit" };

        fullResolutionGameObject = GameObject.Find(typeOfModel[0]);
        fullResolutionGameObject.transform.position = FULL_RESOLUTION_MODEL_POSITION;

        degradedResolutionGameObject = GameObject.Find(fullResolutionGameObject.name + degradedModelExtensions[0]);
        degradedResolutionGameObject.transform.position = DEGRADED_MODEL_POSITION;

        indicatorDegradedModel = 0;
        indicatorFullResolution = 0;

        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        inputManager.CanClick = true;
    }

    /// <summary>
    /// Change Between Degraded Model and Full resolution model with Input Triggered
    /// </summary>
    /// <param name="indicatorDegradedModel"></param>
    /// <param name="indicatorFullResolution"></param>
    private void SwitchBeetwenModels(int indicatorDegradedModel, int indicatorFullResolution)
    {
        if(inputManager.IsRightTriggerClicked())
        {
            ChangeDegradeModel(indicatorDegradedModel);

            if (this.indicatorDegradedModel == 2)
                this.indicatorDegradedModel = 0;
            else
                this.indicatorDegradedModel++;
            inputManager.CanClick = false;
        }
        else if(inputManager.IsLeftTriggerClicked())
        {
            ChangeTypeOfModel(indicatorFullResolution);
            ChangeDegradeModel(indicatorDegradedModel);

            if (this.indicatorFullResolution == 2)
                this.indicatorFullResolution = 0;
            else
                this.indicatorFullResolution++;
            inputManager.CanClick = false;
        }
    }

    /// <summary>
    /// Switch with another Degraded Model and change its position 3D space
    /// </summary>
    /// <param name="indicatorDegradedModel"></param>
    private void ChangeDegradeModel(int indicatorDegradedModel)
    {

        degradedGameObjectName = fullResolutionGameObject.name + degradedModelExtensions[indicatorDegradedModel];
        
        degradedResolutionGameObject.transform.position = SPAWN_POINT;
        degradedResolutionGameObject = GameObject.Find(degradedGameObjectName);
        degradedResolutionGameObject.transform.position = DEGRADED_MODEL_POSITION;
    }

    /// <summary>
    /// Change the type of Model and change its position
    /// </summary>
    /// <param name="indicatorFullResolution"></param>
    private void ChangeTypeOfModel(int indicatorFullResolution)
    {
        fullResolutionGameObject.transform.position = SPAWN_POINT;
        fullResolutionGameObject = GameObject.Find(typeOfModel[indicatorFullResolution]);
        fullResolutionGameObject.transform.position = FULL_RESOLUTION_MODEL_POSITION;
    }

   
}
