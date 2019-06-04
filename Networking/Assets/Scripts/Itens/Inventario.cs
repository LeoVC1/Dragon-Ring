using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class Inventario : MonoBehaviour
{
    public int armorLevel;
    public int helmetLevel;

    AvatarSetup avatarSetup;
    PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
            return;
        avatarSetup = GetComponent<AvatarSetup>();
        armorLevel = 0;
    }

    public void ChangeArmor(int level)
    {
        if (!PV.IsMine)
            return;
        PV.RPC("RPC_ChangeArmorItens", RpcTarget.All, level, PV.ViewID);
    }

    [PunRPC]
    void RPC_ChangeArmorItens(int level, int ID)
    {
        PhotonView.Find(ID).gameObject.GetComponent<AvatarSetup>().myChangeItem.ChangeArmor(level);
    }

    public void ChangeHelmet(int level)
    {
        if (!PV.IsMine)
            return;
        PV.RPC("RPC_ChangeHelmet", RpcTarget.All, level, PV.ViewID);
    }

    [PunRPC]
    void RPC_ChangeHelmet(int level, int ID)
    {
        PhotonView.Find(ID).gameObject.GetComponent<AvatarSetup>().myChangeItem.ChangeHelmet(level);
    }

    
}
