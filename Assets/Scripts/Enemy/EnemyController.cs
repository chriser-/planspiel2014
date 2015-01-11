using UnityEngine;
using System.Collections;

public class EnemyBamgoo : MonoBehaviour
{

    public Transform[] patrolWayPoints;
    public float patrolSpeed = 2f;

    private NavMeshAgent nav;
    private int wayPointIndex = 0;

    // Use this for initialization
    void Start()
    {
        //nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //nav.speed = patrolSpeed;

        //Debug.Log(nav.remainingDistance + " " + nav.stoppingDistance);
        //if (nav.remainingDistance <= nav.stoppingDistance)
        //{
        //    if (wayPointIndex == patrolWayPoints.Length - 1)
        //        wayPointIndex = 0;
        //    else
        //        wayPointIndex++;
        //}

        //nav.destination = patrolWayPoints[wayPointIndex].position;
    }
}
