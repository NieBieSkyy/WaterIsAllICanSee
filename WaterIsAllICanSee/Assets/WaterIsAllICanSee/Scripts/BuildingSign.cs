using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSign : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera[] cameras;

    [Header("Building Menu etc")]
    [SerializeField] GameObject buildingMenu;


    GameObject building;
    bool buildingMenuOpen;
    [SerializeField] int cameraIndex;


    void Start()
    {
        mainCamera = Camera.main;
        buildingMenu.SetActive(false);
        cameraIndex = 0;
        foreach (Camera camera in cameras)
        {
            camera.gameObject.SetActive(false);
        }    
    }

    private void Update()
    {
        if (buildingMenuOpen)
        {
            foreach (Camera camera in cameras)
            {
                camera.gameObject.SetActive(false);
            }
            cameras[cameraIndex].gameObject.SetActive(true);
            if (Input.GetKeyDown("escape"))
            {
                CloseBuildingMenu();
            }
        }
        
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == "Building Sign")
            {
                OpenBuildingMenu();
            }
        }
    }

    private void OpenBuildingMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        buildingMenu.SetActive(true);
        buildingMenuOpen = true;
        mainCamera.gameObject.SetActive(false);
        cameras[cameraIndex].gameObject.SetActive(true);
    }

    private void CloseBuildingMenu()
    {
        mainCamera.gameObject.SetActive(true);
        cameras[cameraIndex].gameObject.SetActive(false);
        buildingMenuOpen = false;
        Cursor.visible = false;
    }

    public void IncreaseCameraIndex()
    {
        if (cameraIndex == cameras.Length - 1)
        {
            cameraIndex = 0;
        }
        else
        {
            cameraIndex += 1;
        }
    }

    public void DecreaseCameraIndex()
    {
        if (cameraIndex == 0)
        {
            cameraIndex = cameras.Length - 1;
        }
        else
        {
            cameraIndex -= 1;
        }
    }

    public void BuildSth()
    {
        building = Camera.current.transform.parent.gameObject;
    }

//zrób listê z budynkami a nie z kamerami - lepszy pomys³ i mo¿esz je potem dodaæ, wtedy ³atwiej itd

}
