using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jiba : MonoBehaviour
{
    [SerializeField] GameObject jiba;
    [SerializeField] Transform waterLevel;
    [SerializeField] Transform depthLevel;
    [SerializeField] Transform mainTarget;

    [SerializeField] float yPosition1;
    [SerializeField] Vector3 target;

    void Start()
    {
        target = mainTarget.transform.position;
        yPosition1 = Random.Range(depthLevel.transform.position.y, waterLevel.transform.position.y);
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
    private void Move()
    {
        target.y = yPosition1;
        jiba.transform.position = Vector3.MoveTowards(jiba.transform.position, target, 1 * Time.deltaTime);
        

        if (jiba.transform.position.y == target.y)
        {
            target = mainTarget.transform.position;
            yPosition1 = Random.Range(depthLevel.transform.position.y, waterLevel.transform.position.y);
            target.y = yPosition1;

        }
        
    }

}
