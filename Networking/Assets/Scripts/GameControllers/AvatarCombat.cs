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
    PlayerMovement movementScript;

    public Transform rayOrigin;

    public Image healthBarDisplay;

    public bool _lock;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
        {
            return;
        }
        healthBarDisplay = GameSetup.GS.playerHealthBar;
        avatarSetup = GetComponent<AvatarSetup>();
        movementScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        healthBarDisplay.fillAmount = avatarSetup.health / 100f;
        if (movementScript.isAttacking && _lock == false)
        {
            PV.RPC("RPC_WarriorAttack", RpcTarget.All, 20f);
        }
        rayOrigin = avatarSetup.myCharacter.GetComponent<CharacterScript>().swordLocation;
    }

    [PunRPC]
    void RPC_WarriorAttack(float damage)
    {
        RaycastHit hit;
        Ray ray = new Ray(rayOrigin.position, rayOrigin.TransformDirection(-Vector3.up));
        if (Physics.Raycast(ray, out hit, 0.5f))
        {
            if (hit.transform.tag == "Player")
            {
                hit.transform.gameObject.GetComponent<AvatarSetup>().health -= damage;
                _lock = true;
            }
        }
    }
}
