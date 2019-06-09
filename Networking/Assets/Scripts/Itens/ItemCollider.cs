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
        PV = GetComponentInParent<PhotonView>();
        if (!PV.IsMine)
            return;
        playerTarget = GetComponentInParent<AvatarSetup>().myCamera.transform;
        inventario = GetComponentInParent<Inventario>();
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!PV.IsMine)
            return;
        if (other.CompareTag("Item"))
        {
            Item otherItem = other.GetComponent<Item>();
            float itemLevel = otherItem.level;
            Itens item = otherItem.itemType;
            if ((!nearItens.Contains(other)) && ((inventario.armorLevel < itemLevel && item == Itens.ARMOR) 
                                             ||  (inventario.helmetLevel < itemLevel && item == Itens.HELMET)
                                             || (inventario.weaponLevel < itemLevel && item == Itens.WEAPON)))
            {
                nearItens.Add(other);
            } 
            for(int i = 0; i < nearItens.Count; i++)
            {
                if (nearItens[i] == null)
                    nearItens.Remove(nearItens[i]);
                else
                {
                    float distance = Vector3.Distance(transform.position, nearItens[i].transform.position);
                    if (distance < nearestDistance || i == 0)
                    {
                        nearestDistance = distance;
                        nearestItem = nearItens[i];
                    }
                    nearItens[i].GetComponent<Item>().pickupImage.gameObject.SetActive(false);
                }
                
            }
            if(nearestItem != null)
                ChangeItem(nearestItem);
        }
    }

    void ChangeItem(Collider coll)
    {
        Item collItem = coll.GetComponent<Item>();
        pickUpScript.pickUpItem = collItem;
        collItem.pickupImage.gameObject.SetActive(true);
        collItem.faceCameraScript.Target = playerTarget.gameObject;
        pickupImage = collItem.pickupImage;
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

}
