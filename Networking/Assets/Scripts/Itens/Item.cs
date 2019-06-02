using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.IO;

public class Item : MonoBehaviour
{
    public int level;
    public GameObject pickupImage;
    public Image timerPickUp;

    Vector3 startPosition;
    Vector3 startRotation;

    private void Start()
    {
        startPosition = pickupImage.GetComponent<RectTransform>().position;
        startRotation = pickupImage.GetComponent<RectTransform>().localEulerAngles;
    }

    private void Update()
    {
        pickupImage.GetComponent<RectTransform>().position = startPosition;
        pickupImage.GetComponent<RectTransform>().localEulerAngles = startRotation;
    }

    public void PickUp(PhotonView PV)
    {
        PV.RPC("RPC_DestroyItem", RpcTarget.All, GetComponent<PhotonView>().ViewID);
    }
}
