using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class SpawnerItem : MonoBehaviour
{
    public GameObject instantiated;

    private string[] itensName = { "PeitoralFerro", "PeitoralOuro", "HelmetFerro", "HelmetOuro", "WeaponFerro", "WeaponOuro" , "Potion"};

    void Start()
    {
        SpawnRandomItem();
    }

    void SpawnRandomItem()
    {
        instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", itensName[Random.Range(0, itensName.Length)]), transform.position, Quaternion.identity);
        instantiated.transform.parent = transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
