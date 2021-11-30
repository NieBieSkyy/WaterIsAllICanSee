using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakingLeader : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5);
        foreach (Collider collider in colliders)
        {
            collider.transform.parent = transform;
        }
    }
}
