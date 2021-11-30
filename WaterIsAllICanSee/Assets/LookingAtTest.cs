using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAtTest : MonoBehaviour
{
    [SerializeField] GameObject target;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos = transform.position - target.transform.position;
        Quaternion quaternion = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, 150 * Time.deltaTime);
    }
}
