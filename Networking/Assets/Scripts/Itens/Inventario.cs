using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public int armorLevel;
    public int helmetLevel;

    ChangeItem changeItem;
    AvatarSetup avatarSetup;

    private void Start()
    {
        avatarSetup = GetComponent<AvatarSetup>();
        armorLevel = 0;
    }

    public void ChangeArmor(int level)
    {
        if (changeItem == null)
            changeItem = avatarSetup.myCharacter.GetComponent<ChangeItem>();

        foreach(Armor a in changeItem.armor)
        {
            if(a._objeto != null)
            {
                switch (level)
                {
                    case 1:
                        a._objeto.GetComponent<MeshRenderer>().material = a._matFerro;
                        armorLevel = 1;
                        break;
                    case 2:
                        a._objeto.GetComponent<MeshRenderer>().material = a._matOuro;
                        armorLevel = 2;
                        break;
                }
            }
        }
    }
}
