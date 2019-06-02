using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EquipedItens : MonoBehaviour
{
    public int level;

    private void Start()
    {
        if (!GetComponentInParent<PhotonView>().IsMine)
            return;
        switch (level)
        {
            case 0:
                gameObject.SetActive(true);
                GetComponentInParent<ChangeItem>().previousArmor.Add(gameObject);
                break;
            case 1:
                GetComponentInParent<ChangeItem>().armor1.Add(gameObject);
                gameObject.SetActive(false);
                break;
            case 2:
                GetComponentInParent<ChangeItem>().armor2.Add(gameObject);
                gameObject.SetActive(false);
                break;
        }
    }
}
