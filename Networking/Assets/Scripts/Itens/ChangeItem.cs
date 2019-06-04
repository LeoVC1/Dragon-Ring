using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class ChangeItem : MonoBehaviour
{
    [Header("Objetos da Armadura:")]
    public GameObject helmet1;
    public GameObject helmet2;
    public GameObject previousHelmet;
    public List<GameObject> armor1 = new List<GameObject>();
    public List<GameObject> armor2 = new List<GameObject>();
    public List<GameObject> previousArmor = new List<GameObject>();
    public AvatarSetup mySetup;
    public void ChangeArmor(int level)
    {
        int armorLevel = mySetup.myInventario.armorLevel;
        switch (level)
        {
            case 1:
                if (armorLevel < 1)
                {
                    for (int i = armor1.Count - 1; i >= 0; i--)
                    {
                        previousArmor[i].SetActive(false);
                        previousArmor.Remove(previousArmor[i]);
                        armor1[i].SetActive(true);
                        previousArmor.Add(armor1[i]);
                    }
                    armorLevel = 1;
                }
                break;
            case 2:
                if (armorLevel < 2)
                {
                    for (int i = armor2.Count - 1; i >= 0; i--)
                    {
                        previousArmor[i].SetActive(false);
                        previousArmor.Remove(previousArmor[i]);
                        armor2[i].SetActive(true);
                        previousArmor.Add(armor2[i]);
                    }
                    if (armorLevel == 1)
                    {
                        GameObject item = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "PeitoralFerro"), transform.position + Vector3.up, transform.rotation);
                        item.GetComponent<Rigidbody>().AddForce(transform.forward * 300);
                    }
                    armorLevel = 2;
                }
                break;
        }
        mySetup.myInventario.armorLevel = armorLevel;
    }

    public void ChangeHelmet(int level)
    {
        int helmetLevel = mySetup.myInventario.helmetLevel;
        switch (level)
        {
            case 1:
                if (helmetLevel < 1)
                {
                    previousHelmet.SetActive(false);
                    helmet1.SetActive(true);
                    previousHelmet = helmet1;
                    helmetLevel = 1;
                }
                break;
            case 2:
                if (helmetLevel < 2)
                {
                    previousHelmet.SetActive(false);
                    helmet2.SetActive(true);
                    previousHelmet = helmet2;
                    if (helmetLevel == 1)
                    {
                        GameObject item = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Itens", "HelmetFerro"), transform.position + Vector3.up, transform.rotation);
                        item.GetComponent<Rigidbody>().AddForce(transform.forward * 300);
                    }
                    helmetLevel = 2;
                }
                break;
        }
        mySetup.myInventario.helmetLevel = helmetLevel;
    }

}
