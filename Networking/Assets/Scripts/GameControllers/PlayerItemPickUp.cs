using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerItemPickUp : MonoBehaviour
{
    PhotonView PV;
    GameObject pickupImage;
    Transform playerTarget;
    bool _lock;


    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
            return;
        playerTarget = GetComponent<AvatarSetup>().myCamera.transform;
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;
        if (_lock)
        {
            pickupImage.transform.LookAt(playerTarget);
            pickupImage.transform.Rotate(0, 180, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!PV.IsMine)
            return;
        if (other.CompareTag("Item"))
        {
            print("Item!");
            pickupImage = other.GetComponent<Item>().pickupImage;
            pickupImage.SetActive(true);
            _lock = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!PV.IsMine)
            return;
        if (other.CompareTag("Item"))
        {
            pickupImage.SetActive(false);
            _lock = false;
        }
    }
}
