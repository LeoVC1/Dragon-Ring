using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Armor
{
    public string name;
    public GameObject _objeto;
    public Material _matBronze;
    public Material _matFerro;
    public Material _matOuro;
}
public class ChangeItem : MonoBehaviour
{
    [Header("Objetos da Armadura:")]
    public Armor[] armor;
   
}
