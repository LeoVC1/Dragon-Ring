using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.IO;

public enum Itens
{
    HELMET,
    ARMOR
}

public class Item : MonoBehaviour
{
    public int level;
    public Itens itemType;
    public GameObject pickupImage;
    public Image timerPickUp;

    Vector3 startPosition;
    Vector3 startRotation;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
    }
}
