using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class SpawnerItem : MonoBehaviour
{
    public GameObject instantiated;

    void Start()
    {
        SpawnRandomItem();
    }

    void SpawnRandomItem()
    {
        instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "Item"), transform.position, transform.rotation);
        instantiated.transform.parent = transform;
    }
}
