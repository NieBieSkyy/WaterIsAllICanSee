using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jiba : MonoBehaviour
{
    [SerializeField] GameObject jiba;
    [SerializeField] Transform waterLevel;
    [SerializeField] Transform depthLevel;
    [SerializeField] Transform mainTarget;
    [SerializeField] Vector3 startingPosition;

    [SerializeField] float yPositionTarget;
    [SerializeField] float progress;
    [SerializeField] Vector3 target;

    void Start()
    {
        progress = 0;
        target = mainTarget.transform.position;
        yPositionTarget = Random.Range(depthLevel.transform.position.y, waterLevel.transform.position.y);
        
    }

    void Update()
    {
        MovingOnY();
    }

    private void MovingOnY()
    {
        if (transform.position.y == yPositionTarget)
        {
            startingPosition = transform.position;
            FindNewTarget();
            progress = 0;
        }
        transform.localPosition = new Vector3(0, YTargetPosition(), 0);
    }

    private void FindNewTarget()
    {
        yPositionTarget = Random.Range(depthLevel.transform.position.y, waterLevel.transform.position.y);
    }

    private float YTargetPosition()
    {  
        if (progress >= 1)
        {
            progress += 0.001f;
        }
        return Mathf.Lerp(startingPosition.y, yPositionTarget, progress);
    }

    private float MovementSpeed()
    {
        return Mathf.Abs(((yPositionTarget - transform.position.y) / 10));
    }
}
