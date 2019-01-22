using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class CatalogHandler : MonoBehaviour {

    public string cellName = "CatalogObjectElement";
    private string cellTag = "CatalogGrab";
    private string grabTag = "Grab";
    public static readonly int NUMBER_OF_ROWS = 3;
    public static readonly int NUMBER_OF_COLUMNS = 3;
    public static readonly float MARGIN_BETWEEN_CELLS = .001f;
    private GameObject catalog;
    private List<ModelData> modelsData;
    private RayCast rayCast;
    private InputManager inputManager;
    private ModelImporter modelImporter;
    private GameObject selectedObject;

	// Use this for initialization
	void Start ()
    {
        catalog = GameObject.Find("Catalog");
        modelsData = new List<ModelData>();
        loadModelDataFromDB();
        DisplayCatalog();
        rayCast = this.GetComponent<RayCast>();
        inputManager = this.GetComponent<InputManager>();
        modelImporter = new ModelImporter();
    }
	
	// Update is called once per frame
	void Update () {
		if(rayCast.Hit())
        {
            if (inputManager.UserClick() && rayCast.GetHit().collider.gameObject.CompareTag(cellTag))
            {
                Thread thread = new Thread(importByThread);
                thread.Start();
            }

        }
	}

    private void importByThread()
    {
        selectedObject = modelImporter.ImportModel(rayCast.GetHit().collider.gameObject.name);
        selectedObject.transform.localScale.Normalize();
        selectedObject.transform.position = this.transform.position;
        selectedObject.AddComponent<Rigidbody>();
        selectedObject.AddComponent<MeshCollider>();
        selectedObject.GetComponent<MeshCollider>().convex = true;
        selectedObject.tag = grabTag;
    }

    public void loadModelDataFromDB()
    {
        modelsData.Add(new ModelData("stickman", "stickman.png", "stickman.obj", 0));
        modelsData.Add(new ModelData("man", "man.png", "man.obj", 1));
        modelsData.Add(new ModelData("sword", "sword.png", "sword.obj", 2));
        modelsData.Add(new ModelData("dog", "dog.png", "dog.obj", 3));
    }

    public void DropCatalog()
    {
        catalog.transform.position = this.transform.position;
    }

    public void DisplayCatalog()
    {

        for(int i =0;i< modelsData.Count; i++)
        {
            GameObject catalogObjectElement = GameObject.Find("CatalogObjectElement");

            Vector3 cellScale = catalogObjectElement.transform.localScale;
            Vector3 cellPos = catalogObjectElement.GetComponent<RectTransform>().position;

            GameObject cell = Instantiate(catalogObjectElement);
            cell.transform.parent = GameObject.Find("CatalogCanvas").transform;
            cell.transform.name = modelsData[i].getFilename();
            cell.transform.localScale = cellScale;
            cell.GetComponent<RectTransform>().position = cellPos;
            cell.transform.rotation = catalogObjectElement.transform.rotation;
            cell.transform.Find("Text").GetComponent<Text>().text = modelsData[i].getName();

            cell.GetComponent<RectTransform>().position += new Vector3((cellScale.x + MARGIN_BETWEEN_CELLS) * (i % 3),(cellScale.y + MARGIN_BETWEEN_CELLS) * (i / 3), 0);
        }
        
    }
    
    public GameObject getCatalog()
    {
        return catalog;
    }
}
