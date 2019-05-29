using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.IO;

public class Item : MonoBehaviour
{
    public GameObject pickupImage;
    public Image timerPickUp;

    public void PickUp(PhotonView PV)
    {
        PV.RPC("RPC_DestroyItem", RpcTarget.All, GetComponent<PhotonView>().ViewID);
    }
}
