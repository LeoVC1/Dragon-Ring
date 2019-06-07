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
        int i = Random.Range(0, 4);
        switch (i)
        {
            case 0:
                instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "PeitoralFerro"), transform.position, transform.rotation);
                break;
            case 1:
                instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "PeitoralOuro"), transform.position, transform.rotation);
                break;
            case 2:
                instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "HelmetFerro"), transform.position, transform.rotation);
                break;
            case 3:
                instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "HelmetOuro"), transform.position, transform.rotation);
                break;
        }
        instantiated.transform.parent = transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
