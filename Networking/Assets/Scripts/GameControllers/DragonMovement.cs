using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonMovement : MonoBehaviour
{

    NavMeshAgent agent;
    Animator anim1;
    public Animator anim;

    public GameObject flame;

    private void Start()
    {
        anim1 = GetComponent<Animator>();
        flame.SetActive(false);
    }

    public void StartMovement()
    {
        anim1.SetTrigger("Walk");
        flame.SetActive(true);
    }

    public void StopMovement()
    {
        flame.SetActive(false);
    }
}
