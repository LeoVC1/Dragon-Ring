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
        int i = Random.Range(0, 2);
        switch (i)
        {
            case 0:
                instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "PeitoralFerro"), transform.position, transform.rotation);
                break;
            case 1:
                instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "PeitoralOuro"), transform.position, transform.rotation);
                break;
        }
        instantiated.transform.parent = transform;
    }
}
