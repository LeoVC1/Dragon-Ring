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
                //StartCoroutine(EnableNewArmor(changeItem.armor1));
                armorLevel = 1;
                break;
            case 2:
                for (int i = changeItem.armor2.Count - 1; i >= 0; i--)
                {
                    changeItem.previousArmor[i].SetActive(false);
                    changeItem.previousArmor.Remove(changeItem.previousArmor[i]);
                    changeItem.armor2[i].SetActive(true);
                    changeItem.previousArmor.Add(changeItem.armor2[i]);
                }
                //StartCoroutine(EnableNewArmor(changeItem.armor2));
                armorLevel = 2;
                break;
        }
    }

    IEnumerator EnableNewArmor(List<GameObject> objects)
    {
        for(int i = objects.Count - 1; i >= 0; i--)
        {
            changeItem.previousArmor[i].SetActive(false);
            changeItem.previousArmor.Remove(changeItem.previousArmor[i]);
            objects[i].SetActive(true);
            changeItem.previousArmor.Add(objects[i]);
            yield return null;
        }
    }
}
