using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public GameObject pickupImage;
    Transform playerTarget;
    bool _lock;

    private void Update()
    {
        if (_lock)
        {
            pickupImage.transform.LookAt(playerTarget);
            pickupImage.transform.Rotate(0, 180, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickupImage.SetActive(true);
            playerTarget = other.GetComponent<AvatarSetup>().myCamera.transform;
            _lock = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickupImage.SetActive(false);
            playerTarget = null;
            _lock = false;
        }
    }


}
