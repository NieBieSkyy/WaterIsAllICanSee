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

    [SerializeField] bool isLeader;
    public bool isLeaded;
    [SerializeField] GameObject target;

    private void Start()
    {
        target = mainTarget;
        isLeaded = false;
    }

    void Update()
    {
        if (isLeader)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 6);
            foreach (Collider collider in colliders)
            {
                if (collider.tag == transform.tag)
                {
                    collider.transform.parent = transform;
                    
                }
            }
        }

        if (transform.parent)
        {
            isLeaded = true;
            bool isSetToProperPosition = false;
            if (!isSetToProperPosition)
            {
                isSetToProperPosition = true;
                transform.position = transform.parent.position - new Vector3(0, 0, 1);
            }
            Vector3 relativePos = transform.position - transform.parent.GetComponent<Jiba>().GiveTarget().transform.position;
            Quaternion quaternion = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, 20 * Time.deltaTime);
        }

        if (isLeaded == false)
        {
            jiba.transform.position = Vector3.MoveTowards(jiba.transform.position, target.transform.position, jibaSpeed * Time.deltaTime);
            RotatingToTarget();

            if (Mathf.Round(transform.position.y) == Mathf.Round(target.transform.position.y))
            {

                GameObject newTarget = Instantiate(target, FindNewTarget(), Quaternion.identity);
                Destroy(target);
                newTarget.name = "Target, BITCH";
                target = newTarget;
                
            }
        }
    }

    private void RotatingToTarget()
    {
        Vector3 relativePos = transform.position - target.transform.position;
        Quaternion quaternion = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, 80 * Time.deltaTime);
    }

    private Vector3 FindNewTarget()
    {
        float xPositionTarget = Random.Range(minX, maxX);
        float yPositionTarget = Random.Range(depthLevel.transform.position.y, waterLevel.transform.position.y);
        float zPositionTarget = Random.Range(minZ, maxZ);

        return new Vector3(xPositionTarget, yPositionTarget, zPositionTarget);
    }

    public GameObject GiveTarget()
    {
        return target;
    }
}
