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

    public GameObject hitMarkPrefab;
    public GameObject hitParticle;
    public GameObject hitPropWood;
    public GameObject hitPropStone;
    public Transform rayOrigin;
    public float damage = 20;

    public bool _lock;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
        {
            return;
        }
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
                        GameObject hitmark = Instantiate(hitMarkPrefab, hit.collider.transform.position + Vector3.up, Quaternion.identity);
                        Destroy(hitmark, 0.5f);
                        PV.RPC("RPC_WarriorAttack", RpcTarget.All, damage, ID, hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z);
                        _lock = true;
                    }
                    else if(hit.transform.tag == "WoodProps" || hit.transform.tag == "StoneProps")
                    {
                        PV.RPC("RPC_WarriorAttackProp", RpcTarget.All, hit.point.x, hit.point.y, hit.point.z, hit.transform.tag);
                        _lock = true;
                    }
                }
            }
        }
    }

    public void ChangeDamage(float value)
    {
        damage += value;
    }

    [PunRPC]
    void RPC_WarriorAttack(float damage, int ID, float x, float y, float z)
    {
        PhotonView.Find(ID).gameObject.GetComponent<HPScript>().ChangeHP(-damage, new Vector3(x, y + 1, z), Vector3.up, 100f);
        GameObject particle = Instantiate(hitParticle, new Vector3(x, y + 1, z), Quaternion.identity);
        Destroy(particle, 0.5f);
    }

    [PunRPC]
    void RPC_WarriorAttackProp(float x, float y, float z, string type)
    {
        GameObject particle;
        switch (type)
        {
            case "WoodProps":
                particle = Instantiate(hitPropWood, new Vector3(x, y, z), Quaternion.identity);
                Destroy(particle, 0.5f);
                break;
            case "StoneProps":
                particle = Instantiate(hitPropStone, new Vector3(x, y, z), Quaternion.identity);
                Destroy(particle, 0.5f);
                break;
        }
        
    }

}
