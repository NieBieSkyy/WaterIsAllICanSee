using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSign : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera secondCamera;

    [Header("Put here all types")]
    [SerializeField] GameObject[] buildingTypes;

    [Header("This pretty important canvas here")]
    [SerializeField] GameObject buildingMenu;
    [SerializeField] GameObject crosshair;

    [Header("IDK but important af")]
    [SerializeField] int buildingIndex;

    bool buildingMenuOpen;

    private void Start()
    {
        mainCamera = Camera.main;
        secondCamera.gameObject.SetActive(false);

        buildingMenu.SetActive(false);
        buildingIndex = 0;

        foreach (GameObject buildingType in buildingTypes)
        {
            buildingType.gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (mainCamera.isActiveAndEnabled == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Building Sign")
                {
                    Collider[] colliders = Physics.OverlapSphere(transform.position, 5);
                    foreach (Collider collider in colliders)
                    {
                        if (collider.CompareTag("Player"))
                        {
                            OpenBuildingMenu();
                        }
                    }
                }
            }
        }  
    }

    private void OpenBuildingMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        buildingMenu.SetActive(true);
        buildingMenuOpen = true;
        if (crosshair != null)
        {
            crosshair.SetActive(false);
        }

        mainCamera.gameObject.SetActive(false);
        secondCamera.gameObject.SetActive(true);

    }

    private void Update()
    {
        if (buildingMenuOpen)
        {
            foreach (GameObject buildingType in buildingTypes)
            {
                buildingType.gameObject.SetActive(false);
            }
            buildingTypes[buildingIndex].gameObject.SetActive(true);
            if (Input.GetKeyDown("e"))
            {
                CloseBuildingMenu();
            } 
        }
    }

    private void CloseBuildingMenu()
    {
        mainCamera.gameObject.SetActive(true);
        secondCamera.gameObject.SetActive(false);

        buildingIndex = 0;

        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        buildingMenu.SetActive(false);
        buildingMenuOpen = false;
        foreach (GameObject buildingType in buildingTypes)
        {
            buildingType.gameObject.SetActive(false);
        }
        if (crosshair != null)
        {
            crosshair.SetActive(true);
        }

        Cursor.visible = false;
    }

    public void Build()
    {
        GameObject chosenObject = Instantiate(buildingTypes[buildingIndex],
            buildingTypes[buildingIndex].transform.position,
            buildingTypes[buildingIndex].transform.rotation);

        chosenObject.name = "The Chosen One";

        foreach (GameObject type in buildingTypes)
        {
            Destroy(type.gameObject);
        }
        CloseBuildingMenu();
        this.gameObject.SetActive(false);
    }

    public void IncreaseCameraIndex()
    {
        if (buildingIndex == buildingTypes.Length - 1)
        {
            buildingIndex = 0;
        }
        else
        {
            buildingIndex += 1;
        }
    }

    public void DecreaseCameraIndex()
    {
        if (buildingIndex == 0)
        {
            buildingIndex = buildingTypes.Length - 1;
        }
        else
        {
            buildingIndex -= 1;
        }
    }

}
