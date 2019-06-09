using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class SpawnerItem : MonoBehaviour
{
    public GameObject instantiated;

    private string[] itensName = { "PeitoralFerro", "PeitoralOuro", "HelmetFerro", "HelmetOuro", "WeaponFerro", "WeaponOuro" };

    void Start()
    {
        SpawnRandomItem();
    }

    void SpawnRandomItem()
    {
        instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", itensName[Random.Range(0, itensName.Length)]), transform.position, Quaternion.identity);
        //switch (i)
        //{
        //    case 0:
        //        instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "PeitoralFerro"), transform.position, transform.rotation);
        //        break;
        //    case 1:
        //        instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "PeitoralOuro"), transform.position, transform.rotation);
        //        break;
        //    case 2:
        //        instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "HelmetFerro"), transform.position, transform.rotation);
        //        break;
        //    case 3:
        //        instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "HelmetOuro"), transform.position, transform.rotation);
        //        break;
        //    case 4:
        //        instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "WeaponFerro"), transform.position, transform.rotation);
        //        break;
        //    case 5:
        //        instantiated = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "WeaponOuro"), transform.position, transform.rotation);
        //        break;
        //}
        instantiated.transform.parent = transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
