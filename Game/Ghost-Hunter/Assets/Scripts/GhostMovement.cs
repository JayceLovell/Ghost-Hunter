using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    public NavMeshAgent Agent;
    public float wanderRadius;
    public Vector3 areaOfSphere;
    public Vector3 randomPoints;
    public Vector3 NewPoint;

    private bool stuck;

    public bool Stuck { get => stuck; set => stuck = value; }

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        StartCoroutine(MoveAgent(10f));
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) {
        //        RaycastHit hit;

        //        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
        //            Agent.destination = hit.point;
        //        }
        //}
        //if(Agent.pathStatus==NavMeshPathStatus.PathInvalid)
        //{
        //    Stuck = true;
        //}
        NavMeshPath path = new NavMeshPath();
        Agent.CalculatePath(NewPoint, path);
        if (path.status == NavMeshPathStatus.PathInvalid)
        {
            Stuck = true;
        }
    }
    IEnumerator MoveAgent(float waitTime)
    {
        areaOfSphere = RandomNavSphere(this.transform.position, wanderRadius, -1);
        Agent.destination = areaOfSphere;
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(MoveAgent(2f));
    }
    Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        randomPoints =  Random.insideUnitSphere * distance;

        randomPoints += origin;

        NewPoint.x = randomPoints.x;
        NewPoint.z = randomPoints.z;

        NavMeshHit navHit;

        NavMesh.SamplePosition(this.NewPoint, out navHit, distance, layermask);

        return navHit.position;
    }
}
