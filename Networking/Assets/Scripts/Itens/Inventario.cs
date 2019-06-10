using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class Inventario : MonoBehaviour
{
    public int armorLevel;
    public int helmetLevel;
    public int weaponLevel;

    public int potions;
    public bool isDrinking = false;

    AvatarSetup avatarSetup;
    PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
            return;
        avatarSetup = GetComponent<AvatarSetup>();
        armorLevel = 0;
        helmetLevel = 0;
        weaponLevel = 0;
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

    public void ChangeWeapon(int level)
    {
        if (!PV.IsMine)
            return;
        PV.RPC("RPC_ChangeWeapon", RpcTarget.All, level, PV.ViewID);
    }

    [PunRPC]
    void RPC_ChangeWeapon(int level, int ID)
    {
        PhotonView.Find(ID).gameObject.GetComponent<AvatarSetup>().myChangeItem.ChangeWeapon(level);
    }

    private void Update()
    {
        DrinkPotion();
    }

    public void DrinkPotion()
    {
        if (!PV.IsMine)
            return;
        if(Input.GetKeyDown(KeyCode.Alpha1) && potions > 0 && isDrinking == false)
        {
            isDrinking = true;
            RemovePotion();
            avatarSetup.animator.SetTrigger("Potion");
            PV.RPC("RPC_DrinkPotion", RpcTarget.All, PV.ViewID);
        }
    }

    public void AddPotion()
    {
        GameSetup.GS.potionImage.SetActive(true);
        potions++;
        GameSetup.GS.potionsCount.text = potions.ToString() + "x";
    }

    public void RemovePotion()
    {
        potions--;
        if(potions == 0)
        {
            GameSetup.GS.potionImage.SetActive(false);
            GameSetup.GS.potionsCount.text = "";
        }
        else
        {
            GameSetup.GS.potionsCount.text = potions.ToString() + "x";
        }

        
    }

    [PunRPC]
    void RPC_DrinkPotion(int ID)
    {
        PhotonView.Find(ID).gameObject.GetComponent<AvatarSetup>().myChangeItem.ShowPotion();
    }
}
