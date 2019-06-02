using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EquipedItens : MonoBehaviour
{
    public int levelItem;
    PhotonView PV;
    ChangeItem changeItem;

    private void Start()
    { 
        //PV = GetComponent<PhotonView>();
        //PV.RPC("RPC_AddItensToEquiped", RpcTarget.AllBuffered, PV.ViewID, levelItem);
        /*
        switch (levelItem)
        {
            case 0:
                gameObject.SetActive(true);
                if (!changeItem.previousArmor.Contains(gameObject))
                    changeItem.previousArmor.Add(gameObject);
                break;
            case 1:
                if (!changeItem.previousArmor.Contains(gameObject))
                    changeItem.armor1.Add(gameObject);
                gameObject.SetActive(false);
                break;
            case 2:
                if (!changeItem.previousArmor.Contains(gameObject))
                    changeItem.armor2.Add(gameObject);
                gameObject.SetActive(false);
                break;
        }*/
    }
}
/*
    [PunRPC]
    void RPC_AddItensToEquiped(int ID, int level)
    {
        ChangeItem changeItem = PhotonView.Find(ID).gameObject.GetComponentInChildren<ChangeItem>();
        switch (level)
        {
            case 0:
                gameObject.SetActive(true);
                if (!changeItem.previousArmor.Contains(gameObject))
                    changeItem.previousArmor.Add(gameObject);
                break;
            case 1:
                if (!changeItem.previousArmor.Contains(gameObject))
                    changeItem.armor1.Add(gameObject);
                gameObject.SetActive(false);
                break;
            case 2:
                if (!changeItem.previousArmor.Contains(gameObject))
                    changeItem.armor2.Add(gameObject);
                gameObject.SetActive(false);
                break;
        }
    }*/
