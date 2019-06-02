using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemCollider : MonoBehaviour
{
    public PlayerItemPickUp pickUpScript;

    public List<Collider> nearItens = new List<Collider>();

    public Collider nearestItem;

    PhotonView PV;
    Transform playerTarget;
    Inventario inventario;
    GameObject pickupImage;

    float nearestDistance;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
            return;
        playerTarget = GetComponentInParent<AvatarSetup>().myCamera.transform;
        inventario = GetComponentInParent<Inventario>();
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;
        ImageFollowCamera();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!PV.IsMine)
            return;
        if (other.CompareTag("Item"))
        {
            if(!nearItens.Contains(other) && inventario.armorLevel < other.GetComponent<Item>().level)
                nearItens.Add(other);
            for(int i = 0; i < nearItens.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, nearItens[i].transform.position);
                if (distance < nearestDistance || i == 0)
                {
                    nearestDistance = distance;
                    nearestItem = nearItens[i];
                }
                nearItens[i].GetComponent<Item>().pickupImage.gameObject.SetActive(false);
            }
            if(nearestItem != null)
                ChangeItem(nearestItem);
        }
    }

    void ChangeItem(Collider coll)
    {
        print(coll);
        pickUpScript.pickUpItem = coll.GetComponent<Item>();
        coll.GetComponent<Item>().pickupImage.gameObject.SetActive(true);
        pickupImage = coll.GetComponent<Item>().pickupImage;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!PV.IsMine)
            return;
        if (other.CompareTag("Item") && nearItens.Contains(other))
        {
            other.GetComponent<Item>().pickupImage.gameObject.SetActive(false);
            nearItens.Remove(other);
        }
        if(nearItens.Count == 0)
        {
            pickupImage = null;
            nearestItem = null;
            pickUpScript.pickUpItem = null;
        }
    }

    void ImageFollowCamera()
    {
        if (pickupImage == null)
            return;
        pickupImage.transform.LookAt(playerTarget);
        pickupImage.transform.Rotate(0, 180, 0);
    }

}
