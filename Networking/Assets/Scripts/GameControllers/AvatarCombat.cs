using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AvatarCombat : MonoBehaviour
{
    PhotonView PV;
    AvatarSetup avatarSetup;

    public Transform rayOrigin;

    public Image healthBarDisplay;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
        {
            return;
        }
        healthBarDisplay = GameSetup.GS.playerHealthBar;
        avatarSetup = GetComponent<AvatarSetup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        healthBarDisplay.fillAmount = avatarSetup.health / 100f;
    }

    [PunRPC]
    void RPC_Shooting(float damage)
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward), out hit, 1000))
        {
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.transform.tag == "Player")
            {
                hit.transform.gameObject.GetComponent<AvatarSetup>().health -= damage;
            }
        }
        else
        {
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
    }
}
