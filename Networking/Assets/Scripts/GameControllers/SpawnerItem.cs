using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class SpawnerItem : MonoBehaviour
{
    public GameObject instantiated;

    private string[] itensName = { "PeitoralFerro", "Potion", "HelmetFerro", "WeaponFerro", "HelmetOuro", "WeaponOuro" , "PeitoralOuro" };

    void Start()
    {
        SpawnRandomItem();
    }

    void SpawnRandomItem()
    {
        int n = Random.Range(0, 11);
        if (n > 3)
            n = Random.Range(0, 4);
        else
            n = Random.Range(0, itensName.Length);

        instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", itensName[n]), transform.position, Quaternion.identity);
        instantiated.transform.parent = transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
