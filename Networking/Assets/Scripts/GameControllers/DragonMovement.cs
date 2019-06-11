using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonMovement : MonoBehaviour
{

    NavMeshAgent agent;
    Animator anim1;
    public Animator anim;

    public Transform child;
    public Transform startPos;
    public Transform[] ring1;
    public Transform[] ring2;

    public GameObject flame;
    bool run;
    bool home;

    public Transform actualWaypoint;
    Transform[] actualWaypointList;
    int waypoint;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim1 = GetComponent<Animator>();
        flame.SetActive(false);
    }

    void Update()
    {
        child.rotation = transform.rotation;
        print(agent.remainingDistance);
        if (run)
        {
            agent.SetDestination(actualWaypoint.position);
            if (agent.remainingDistance < 4f)
                FindNextWaypoint();
        }
        if (home)
        {
            anim.SetBool("Run", home);
            if (agent.remainingDistance < 4f)
                home = false;
        }
        else
            anim.SetBool("Run", run);
    }

    public void FindNextWaypoint()
    {
        waypoint = Random.Range(0, actualWaypointList.Length);
        actualWaypoint = actualWaypointList[waypoint];
    }

    public void StartMovement(int ring)
    {
        anim1.SetTrigger("Walk");
        flame.SetActive(true);
        //switch (ring)
        //{
        //    case 1:
        //        actualWaypointList = ring1;
        //        break;
        //    case 2:
        //        actualWaypointList = ring2;
        //        break;
        //}
        //FindNextWaypoint();
        //run = true;
    }

    public void StopMovement()
    {
        flame.SetActive(false);
        //agent.SetDestination(startPos.position);
        run = false;
        //home = true;
    }
}
