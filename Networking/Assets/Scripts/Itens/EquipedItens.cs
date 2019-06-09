using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedItens : MonoBehaviour
{
    ChangeItem parentChangeItem;
    MeshRenderer myMeshRenderer;
    public Itens myItemType;
    public int myLevel;

    private void Start()
    {
        parentChangeItem = GetComponentInParent<ChangeItem>();
        myMeshRenderer = GetComponent<MeshRenderer>();
        myMeshRenderer.enabled = false;
        parentChangeItem.equipedItens++;
    }

    private void FixedUpdate()
    {
        switch (myItemType)
        {
            case Itens.ARMOR:
                Armor();
                break;
            case Itens.HELMET:
                Helmet();
                break;
            case Itens.WEAPON:
                Weapon();
                break;
        }
    }

    public void DestroyThis()
    {
        parentChangeItem.equipedItens--;
        Destroy(gameObject);
    }

    public void Helmet()
    {
        if (parentChangeItem.helmetLevel < myLevel)
            myMeshRenderer.enabled = false;
        else if (parentChangeItem.helmetLevel == myLevel)
            myMeshRenderer.enabled = true;
        else
            DestroyThis();
    }

    public void Armor()
    {
        if (parentChangeItem.armorLevel < myLevel)
            myMeshRenderer.enabled = false;
        else if (parentChangeItem.armorLevel == myLevel)
            myMeshRenderer.enabled = true;
        else
            DestroyThis();
    }

    public void Weapon()
    {
        if (parentChangeItem.weaponLevel < myLevel)
            myMeshRenderer.enabled = false;
        else if (parentChangeItem.weaponLevel == myLevel)
            myMeshRenderer.enabled = true;
        else
            DestroyThis();
    }
}
