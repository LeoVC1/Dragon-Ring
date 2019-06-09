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
    HPScript HPScript;

    public GameObject hitParticle;
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
        HPScript = GetComponent<HPScript>();
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
            rayOrigin = avatarSetup.myCharacter.GetComponent<CharacterScript>().swordLocation;
            RaycastHit[] hits;
            Ray ray = new Ray(rayOrigin.position, rayOrigin.TransformDirection(-Vector3.up));
            Debug.DrawRay(ray.origin, ray.direction, Color.yellow, 1f);
            hits = Physics.RaycastAll(ray, 1f);
            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    if (hit.transform.tag == "Player" && hit.collider != GetComponent<CapsuleCollider>())
                    {
                        int ID = hit.collider.GetComponent<PhotonView>().ViewID;
                        Vector3 direction = hit.point - hit.collider.transform.position;
                        PV.RPC("RPC_WarriorAttack", RpcTarget.All, 20f, ID, direction.x, direction.y, direction.z);
                        _lock = true;

                    }
                }
            }
        }
    }

    [PunRPC]
    void RPC_WarriorAttack(float damage, int ID, float x, float y, float z)
    {
        PhotonView.Find(ID).gameObject.GetComponent<HPScript>().ChangeHP(-damage, transform.position, Vector3.up, 100f);
        GameObject particle = Instantiate(hitParticle, new Vector3(x, y, z), Quaternion.identity);
        Destroy(particle, 0.5f);
    }
}
