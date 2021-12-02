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
    [Tooltip("Distance from where you can scare this smol fish")] [SerializeField] float scareRadius;
    [SerializeField] float timeFishIsScared;
    [SerializeField] bool fishIsScared;


    [Header("Leadership Variables")]
    [SerializeField] bool isLeader;
    public bool isLeaded;
    [SerializeField] List<GameObject> leadedfish;

    [Header("Useful For Fish Placement")]
    [SerializeField] float placementChange;
    [Tooltip("Don't change this value")] [SerializeField] float xFish;
    [Tooltip("Don't change this value")] [SerializeField] float yFish;
    [Tooltip("Don't change this value")] [SerializeField] float zFish;

    [Header("IDK but it's important I guess")]
    [SerializeField] GameObject target;

    bool isSetToProperPosition;

    private void Start()
    {
        target = mainTarget;
        isLeaded = false;
        isSetToProperPosition = false;
        fishIsScared = false;
    }

    void Update()
    {
        if (isLeader)
        {
            AddingLeadedFish();
            SpottingDanger();
        }

        if (fishIsScared)
        {
            StartCoroutine(FishRun());
            fishIsScared = false;
        }

        if (transform.parent)
        {
            isLeaded = true;
            if (!isSetToProperPosition)
            {
                isSetToProperPosition = true;
                JibaFirstConfiguration();
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
    private void AddingLeadedFish()
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

    private void SpottingDanger()
    {
        Collider[] scaryColliders = Physics.OverlapSphere(transform.position, scareRadius);
        foreach (Collider potentialDanger in scaryColliders)
        {
            if (potentialDanger.tag != transform.tag || potentialDanger.tag != "glony")
            {
                foreach (GameObject fish in leadedfish)
                {
                    Vector3 relativePos = transform.position - transform.parent.GetComponent<Jiba>().GiveTarget().transform.position;
                    Quaternion quaternion = Quaternion.Inverse(Quaternion.LookRotation(relativePos));
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, 135 * Time.deltaTime);
                    fishIsScared = true;
                    fish.transform.parent = null;
                }
            }
        }
    }

    private void JibaFirstConfiguration()
    {
        XFishConfig();
        YFishConfig();
        ZFishConfig();
        transform.parent.GetComponent<Jiba>().FishAdded();
        transform.position = transform.parent.position - SetFishPosition();
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

    public void FishAdded()
    {
        leadedfish.Add(gameObject);
    }

    private Vector3 SetFishPosition()
    {
        float axis = Random.Range(0, 3);
        if (axis < 1)
        {
            xFish += placementChange;
        }
        else if (axis < 2)
        {
            yFish += placementChange;
        }
        else
        {
            zFish += placementChange;
        }

        return new Vector3(xFish, yFish, zFish);
    }

    public float XFishConfig()
    {
        return xFish;
    }
    public float YFishConfig()
    {
        return yFish;
    }
    public float ZFishConfig()
    {
        return zFish;
    }

    private void RunFishRun()
    {
        transform.position = Vector3.forward * Time.deltaTime;
    }
    IEnumerator FishRun()
    {
        InvokeRepeating("RunFishRun", 0, timeFishIsScared);
        yield return new WaitForSeconds(timeFishIsScared);
    }
}
