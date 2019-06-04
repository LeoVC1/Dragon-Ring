using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerItemPickUp : MonoBehaviour
{
    public float offset;
    public LayerMask mask;
    PhotonView PV;
    Inventario inventario;
    AvatarSetup avatarSetup;
    public Item pickUpItem;
    bool _lock;
    float pickUpTime = 1f;
    public float pickUpValue = 0;


    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
            return;
        avatarSetup = GetComponent<AvatarSetup>();
        inventario = GetComponent<Inventario>();
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;

        if(pickUpItem != null)
        {
            Locker();
        }
        else
        {
            _lock = false;
        }
    }

    void PickUpTimer()
    {
        if (Input.GetKeyUp(KeyCode.E))
            pickUpValue = 0;
        if (Input.GetKey(KeyCode.E))
            pickUpValue += 1 * Time.deltaTime;
    }

    void OnItem()
    {
        if (_lock)
        {
            if (pickUpItem != null)
                pickUpItem.timerPickUp.fillAmount = pickUpValue;
            PickUpTimer();
            if(pickUpValue >= pickUpTime)
            {
                GetComponentInChildren<ItemCollider>().nearItens.Remove(pickUpItem.GetComponent<Collider>());
                pickUpItem.PickUp(PV);
                switch (pickUpItem.itemType)
                {
                    case Itens.ARMOR:
                        inventario.ChangeArmor(pickUpItem.level);
                        break;
                    case Itens.HELMET:
                        inventario.ChangeHelmet(pickUpItem.level);
                        break;

                }
                pickUpItem = null;
                pickUpValue = 0;
            }

        }
        else
        {
            pickUpValue = 0;
        }
    }
    public void Locker()
    {
        _lock = true;
        OnItem();
    }

    [PunRPC]
    void RPC_DestroyItem(int viewId)
    {
        Destroy(PhotonView.Find(viewId).gameObject);
    }

}
