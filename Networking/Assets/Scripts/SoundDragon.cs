using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDragon : MonoBehaviour
{

    public AudioClip[] clips;
    public AudioSource aSource;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    public void PlayGrowl()
    {
        aSource.clip = clips[0];
        aSource.Play();
        Invoke("PlayBreath", 5);
    }

    public void PlayBreath()
    {
        aSource.clip = clips[1];
        aSource.loop = true;
        aSource.Play();
    }

}
