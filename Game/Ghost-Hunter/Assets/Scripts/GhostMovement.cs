using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Deals with Ghost Movement
/// </summary>
public class GhostMovement : MonoBehaviour
{  
    public Vector3 areaOfSphere;
    public Vector3 randomPoints;
    public Vector3 NewPoint;
    public GameObject[] Waypoints;

    private float _randomRate;
    private bool _stuck;
    //private float _wanderRadius;
    private NavMeshAgent _agent;
    private float _speed;

    public bool Stuck { get => _stuck; set => _stuck = value; }
    //public float WanderRadius { get => _wanderRadius; set => _wanderRadius = value; }
    public float RandomRate { get => _randomRate; set => _randomRate = value; }
    public float Speed { get => _speed; set => _speed = value; }

    void Awake()
    {
        Waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        _agent = GetComponent<NavMeshAgent>();
    }
    /// <summary>
    /// Moves the Agent to a random location at random Time
    /// </summary>
    /// <param name="moveAgainInSeconds"></param>
    /// <returns></returns>
    public IEnumerator MoveAgent(float moveAgainInSeconds)
    {
        //areaOfSphere = RandomNavSphere(this.transform.position, WanderRadius, -1);
        //_agent.destination = areaOfSphere;
        _agent.destination = Waypoints[Random.Range(0,(Waypoints.Length-1))].transform.position;
        _agent.speed = _speed;
        NavMeshPath path = new NavMeshPath();
        _agent.CalculatePath(NewPoint, path);
        if (path.status == NavMeshPathStatus.PathInvalid)
        {
            if (!_stuck)
            {
                _stuck = true;
            }
        }
        yield return new WaitForSeconds(moveAgainInSeconds);
        moveAgainInSeconds = Random.Range(1f, _randomRate);
        StartCoroutine(MoveAgent(moveAgainInSeconds));
    }
    /// <summary>
    /// Gets the random position to move to in a sphere
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="distance"></param>
    /// <param name="layermask"></param>
    /// <returns></returns>
    //Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    //{
    //    randomPoints =  Random.insideUnitSphere * distance;

    //    randomPoints += origin;

    //    NewPoint.x = randomPoints.x;
    //    NewPoint.z = randomPoints.z;

    //    NavMeshHit navHit;

    //    NavMesh.SamplePosition(this.NewPoint, out navHit, distance, layermask);

    //    return navHit.position;
    //}
}
