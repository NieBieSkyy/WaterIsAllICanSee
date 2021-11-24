using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jiba : MonoBehaviour
{
    [Header("Starting Objects")]
    [SerializeField] GameObject jiba;
    [Tooltip("Don't worry, it should disappear")] [SerializeField] GameObject mainTarget;

    [Header("X Axis")]
    [SerializeField] float minX;
    [SerializeField] float maxX;

    [Header("Y Axis")]
    [Tooltip("Just a max Y value")] [SerializeField] Transform waterLevel;
    [Tooltip("Just a min Y value")] [SerializeField] Transform depthLevel;

    [Header("Z Axis")]
    [SerializeField] float minZ;
    [SerializeField] float maxZ;

    [Header("Parameters")]
    [Tooltip("Really?")] [SerializeField] float jibaSpeed;

    private GameObject target;

    private void Start()
    {
        target = mainTarget;
    }

    void Update()
    {
        jiba.transform.position = Vector3.MoveTowards(jiba.transform.position, target.transform.position, jibaSpeed * Time.deltaTime);
        jiba.transform.LookAt(target.transform.position);

        if (Mathf.Round(transform.position.y) == Mathf.Round(target.transform.position.y))
        {
            
            GameObject newTarget = Instantiate(target, FindNewTarget(), Quaternion.identity);
            Destroy(target);
            newTarget.name = "Target, BITCH";
            target = newTarget;
        }
    }

    private Vector3 FindNewTarget()
    {
        float xPositionTarget = Random.Range(minX, maxX);
        float yPositionTarget = Random.Range(depthLevel.transform.position.y, waterLevel.transform.position.y);
        float zPositionTarget = Random.Range(minZ, maxZ);

        return new Vector3(xPositionTarget, yPositionTarget, zPositionTarget);
    }
}
