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
    GameObject pickupImage;
    Item pickUpItem;
    Transform playerTarget;
    bool _lock;
    float pickUpTime = 0.7f;
    public float pickUpValue = 0;


    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
            return;
        avatarSetup = GetComponent<AvatarSetup>();
        playerTarget = GetComponent<AvatarSetup>().myCamera.transform;
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;

        OnItem();
        SelectItem();
    }

    void ImageFollowCamera()
    {
        if (pickupImage == null)
            return;
        pickupImage.transform.LookAt(playerTarget);
        pickupImage.transform.Rotate(0, 180, 0);
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
            ImageFollowCamera();
            PickUpTimer();
            if(pickUpValue >= pickUpTime)
            {
                pickUpItem.PickUp();
                pickupImage = null;
                pickUpItem = null;
            }

        }
        else
        {
            pickUpValue = 0;
        }
    }

    void SelectItem()
    {
        Ray ray = new Ray(transform.position - new Vector3(0, offset, 0), avatarSetup.myCharacter.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        if(Physics.Raycast(ray, out RaycastHit hit, 1, mask, QueryTriggerInteraction.Collide))
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
    }

}
