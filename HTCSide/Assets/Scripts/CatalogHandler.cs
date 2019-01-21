using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogHandler : MonoBehaviour {

    public string cellName = "CatalogObjectElement";
    public static readonly int NUMBER_OF_ROWS = 3;
    public static readonly int NUMBER_OF_COLUMNS = 3;
    public static readonly float MARGIN_BETWEEN_CELLS = .001f;
    private GameObject catalog;
    private List<ModelData> modelsData;

	// Use this for initialization
	void Start ()
    {
        Debug.Log("Start");
        catalog = GameObject.Find("Catalog");
        modelsData = new List<ModelData>();
        loadModelDataFromDB();
        DisplayCatalog();
    }
	
	// Update is called once per frame
	void Update () {
		
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
        GameObject cell = Instantiate(GameObject.Find("CatalogObjectElement"));
        cell.transform.parent = GameObject.Find("CatalogCanvas").transform;

        cell.transform.localScale = GameObject.Find("CatalogObjectElement").transform.localScale;
        cell.GetComponent<RectTransform>().anchoredPosition = GameObject.Find("CatalogObjectElement").GetComponent<RectTransform>().anchoredPosition;
        cell.transform.rotation = GameObject.Find("CatalogObjectElement").transform.rotation;

        cell.GetComponent<RectTransform>().Translate(0, 200, 0);
    }


    public GameObject getCatalog()
    {
        return catalog;
    }
}
