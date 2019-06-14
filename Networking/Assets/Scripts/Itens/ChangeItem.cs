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

    public GameObject handPotion;

    public int armorLevel = 0;
    public int helmetLevel = 0;
    public int weaponLevel = 0;
    public bool change = false;
    public int equipedItens = 0;
    private int previousEquipedItens = 0;
    public float healthPerEquip = 10;
    public float damagePerEquip = 8;

    private int aux = 0;

    public AvatarSetup mySetup;

    public void ChangeArmor(int level)
    {
        armorLevel = level;

        mySetup.myHPScript.ChangeMaxHPValue(healthPerEquip * armorLevel);
        mySetup.myHPScript.ChangeHPValue(healthPerEquip * helmetLevel);
        mySetup.myInventario.armorLevel = armorLevel;

        //if (armorLevel == 1 && level > 1)
        //{
        //    GameObject item = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "PeitoralFerro"), transform.position + Vector3.up, transform.rotation);
        //    Rigidbody rb = item.GetComponent<Rigidbody>();
        //    if (rb != null)
        //        rb.AddForce(transform.forward * 300);
        //}
    }

    public void ChangeHelmet(int level)
    {
        helmetLevel = level;

        
        mySetup.myHPScript.ChangeMaxHPValue(healthPerEquip * helmetLevel);
        mySetup.myHPScript.ChangeHPValue(healthPerEquip * helmetLevel);

        mySetup.myInventario.helmetLevel = helmetLevel;

        //if (helmetLevel == 1 && level > 1)
        //{
        //    GameObject item = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "HelmetFerro"), transform.position + Vector3.up, transform.rotation);
        //    Rigidbody rb = item.GetComponent<Rigidbody>();
        //    if (rb != null)
        //        rb.AddForce(transform.forward * 300);
        //}

    }

    public void ChangeWeapon(int level)
    {
        weaponLevel = level;

        mySetup.myAvatarCombat.ChangeDamage(damagePerEquip * weaponLevel);

        mySetup.myInventario.weaponLevel = weaponLevel;

        //if (weaponLevel == 1 && level > 1)
        //{
        //    GameObject item = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "WeaponFerro"), transform.position + Vector3.up, transform.rotation);
        //    Rigidbody rb = item.GetComponent<Rigidbody>();
        //    if (rb != null)
        //        rb.AddForce(transform.forward * 300);
        //}

    }

    public void ShowPotion()
    {
        aux = weaponLevel;

        weaponLevel = -1;

        handPotion.SetActive(true);
        Invoke("HidePotion", 3f);
    }

    public void HidePotion()
    {
        mySetup.myInventario.isDrinking = false;
        mySetup.myHPScript.ChangeHPValue(50);
        weaponLevel = aux;
        handPotion.SetActive(false);
    }
}
