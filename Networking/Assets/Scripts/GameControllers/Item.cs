using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.IO;

public class Item : MonoBehaviour
{
    public GameObject pickupImage;
    public GameObject instantiated;

    private void Start()
    {
        SpawnRandomItem();
    }

    public void PickUp(PhotonView PV)
    {
        PV.RPC("RPC_DestroyItem", RpcTarget.All, instantiated.GetComponent<PhotonView>().ViewID);
    }

    void SpawnRandomItem()
    {
        instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "Item"), transform.position, transform.rotation);
        instantiated.transform.parent = transform;
    }
}
