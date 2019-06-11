using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonMovement : MonoBehaviour
{

    SoundDragon sound;
    NavMeshAgent agent;
    Animator anim1;
    public Animator anim;

    public GameObject flame;

    private void Start()
    {
        anim1 = GetComponent<Animator>();
        sound = GetComponent<SoundDragon>();
        flame.SetActive(false);
    }

    public void StartMovement()
    {
        sound.PlayGrowl();
        anim1.SetTrigger("Walk");
        flame.SetActive(true);
    }

    public void StopMovement()
    {
        sound.aSource.Stop();
        flame.SetActive(false);
    }
}
