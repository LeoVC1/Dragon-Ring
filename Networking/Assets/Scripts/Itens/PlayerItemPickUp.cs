using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerItemPickUp : MonoBehaviour
{
    public float offset;
    public LayerMask mask;
    PhotonView PV;
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
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;

        if(pickUpItem != null)
        {
            Teste();
        }
        else
        {
            _lock = false;
        }
        //OnItem();
        //SelectItem();
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
                pickUpItem.PickUp(PV);
                pickUpItem = null;
            }

        }
        else
        {
            pickUpValue = 0;
        }
    }
    /*
    void SelectItem()
    {
        Ray ray = new Ray(transform.position - new Vector3(0, offset, 0), avatarSetup.myCharacter.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        if(Physics.Raycast(ray, out RaycastHit hit, 1, mask))
        {
            pickUpItem = hit.collider.GetComponent<Item>();
            pickupImage = hit.collider.GetComponent<Item>().pickupImage;
            pickupImage.SetActive(true);
            _lock = true;
        }
        else if(_lock == true)
        {
            if (pickupImage != null)
                pickupImage.SetActive(false);
            pickupImage = null;
            _lock = false;
        }
    }*/

    public void Teste()
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
