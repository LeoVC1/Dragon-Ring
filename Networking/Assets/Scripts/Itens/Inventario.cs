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
        AvatarSetup destinyAvatar;
        ChangeItem changeItem;
        Inventario destinyInventario;
        destinyAvatar = PhotonView.Find(ID).gameObject.GetComponent<AvatarSetup>();
        changeItem = destinyAvatar.myChangeItem;
        destinyInventario = destinyAvatar.GetComponent<Inventario>();

        switch (level)
        {
            case 1:
                for (int i = changeItem.armor1.Count - 1; i >= 0; i--)
                {
                    changeItem.previousArmor[i].SetActive(false);
                    changeItem.previousArmor.Remove(changeItem.previousArmor[i]);
                    changeItem.armor1[i].SetActive(true);
                    changeItem.previousArmor.Add(changeItem.armor1[i]);
                }
                destinyInventario.armorLevel = 1;
                break;
            case 2:
                for (int i = changeItem.armor2.Count - 1; i >= 0; i--)
                {
                    changeItem.previousArmor[i].SetActive(false);
                    changeItem.previousArmor.Remove(changeItem.previousArmor[i]);
                    changeItem.armor2[i].SetActive(true);
                    changeItem.previousArmor.Add(changeItem.armor2[i]);
                }
                if (destinyInventario.armorLevel == 1)
                {
                    GameObject item = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "PeitoralFerro"), transform.position, transform.rotation);
                    item.GetComponent<Rigidbody>().AddForce(changeItem.transform.forward * 250);
                }
                destinyInventario.armorLevel = 2;
                
                break;
        }

    }

    
}
