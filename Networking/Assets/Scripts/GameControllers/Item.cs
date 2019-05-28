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
    PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        SpawnRandomItem();

    }

    public void PickUp()
    {
        Destroy(gameObject);
        Destroy(instantiated);
    }

    void SpawnRandomItem()
    {
        instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "Item"), transform.position, transform.rotation);
    }


}
