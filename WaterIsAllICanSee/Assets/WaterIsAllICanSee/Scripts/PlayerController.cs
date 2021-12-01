using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("GameObjectsToRefer")]
    [SerializeField] GameObject ArmWithSomething1;
    [SerializeField] GameObject ArmWithSomething2;

    void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            ArmWithSomething1.SetActive(true);
            ArmWithSomething2.SetActive(false);
        }
        if (Input.GetKeyDown("2"))
        {
            ArmWithSomething1.SetActive(false);
            ArmWithSomething2.SetActive(true);
        }
    }
}
