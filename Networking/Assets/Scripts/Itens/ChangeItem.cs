using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class ChangeItem : MonoBehaviour
{
    [Header("Helmet:")]
    public GameObject helmet1;
    public GameObject helmet2;
    public GameObject previousHelmet;
    [Header("Armor:")]
    public List<GameObject> armor1 = new List<GameObject>();
    public List<GameObject> armor2 = new List<GameObject>();
    public List<GameObject> previousArmor = new List<GameObject>();
    [Header("Arma:")]
    public GameObject[] weapons1;
    public GameObject[] weapons2;
    public GameObject[] previousWeapons;

    public int armorLevel = 0;
    public int helmetLevel = 0;
    public int weaponLevel = 0;
    public bool change = false;
    public int equipedItens = 0;
    private int previousEquipedItens = 0;

    public AvatarSetup mySetup;

    private void FixedUpdate()
    {
        //if(change == true && equipedItens == previousEquipedItens / 3)
        //{
        //    change = false;
        //}
    }

    public void ChangeArmor(int level)
    {
        if (armorLevel == 1 && level > 1)
        {
            GameObject item = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "PeitoralFerro"), transform.position + Vector3.up, transform.rotation);
            item.GetComponent<Rigidbody>().AddForce(transform.forward * 300);
        }

        armorLevel = level;
        previousEquipedItens = equipedItens;
        change = true;

        mySetup.myInventario.armorLevel = armorLevel;
    }

    public void ChangeHelmet(int level)
    {
        if (helmetLevel == 1 && level > 1)
        {
            GameObject item = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "HelmetFerro"), transform.position + Vector3.up, transform.rotation);
            item.GetComponent<Rigidbody>().AddForce(transform.forward * 300);
        }

        helmetLevel = level;
        previousEquipedItens = equipedItens;
        change = true;

        mySetup.myInventario.helmetLevel = helmetLevel;
    }

    public void ChangeWeapon(int level)
    {
        if (weaponLevel == 1 && level > 1)
        {
            GameObject item = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "WeaponFerro"), transform.position + Vector3.up, transform.rotation);
            item.GetComponent<Rigidbody>().AddForce(transform.forward * 300);
        }

        weaponLevel = level;
        previousEquipedItens = equipedItens;
        change = true;

        mySetup.myInventario.weaponLevel = weaponLevel;
    }
}
