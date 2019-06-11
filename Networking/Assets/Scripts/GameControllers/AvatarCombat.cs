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

    public AudioSource aSource;
    public AudioClip[] clips;
    public GameObject hitMarkPrefab;
    public GameObject hitParticle;
    public GameObject hitPropWood;
    public GameObject hitPropStone;
    public Transform rayOrigin;
    public float damage = 20;
    public int kills = 0;

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
        GameSetup.GS.kills.text = kills.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine || avatarSetup.died)
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
                        PlaySound();
                        PV.RPC("RPC_WarriorAttack", RpcTarget.All, damage, ID, hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z, PhotonNetwork.NickName, PV.ViewID);
                        _lock = true;
                    }
                    else if (hit.transform.tag == "WoodProps" || hit.transform.tag == "StoneProps")
                    {
                        PlaySound(hit.transform.tag);
                        PV.RPC("RPC_WarriorAttackProp", RpcTarget.All, hit.point.x, hit.point.y, hit.point.z, hit.transform.tag);
                        _lock = true;
                    }
                }
            }
        }
    }

    public void PlaySound()
    {
        aSource.volume = 0.6f;
        aSource.clip = clips[2];
        aSource.Play();
    }

    public void PlaySound(string tag)
    {
        switch(tag)
        {
            case "WoodProps":
                aSource.volume = 0.7f;
                aSource.clip = clips[0];
                break;
            case "StoneProps":
                aSource.volume = 0.3f;
                aSource.clip = clips[1];
                break;
        }
        aSource.Play();
    }

    public void Kill()
    {
        if (!PV.IsMine)
            return;
        kills++;
        GameSetup.GS.kills.text = kills.ToString();
        GameSetup.GS.finalKills.text = "Voce obteve um total de " + kills.ToString() + " abates!";
    }

    public void ChangeDamage(float value)
    {
        if (!PV.IsMine)
            return;
        damage += value;
    }

    [PunRPC]
    void RPC_WarriorAttack(float damage, int ID, float x, float y, float z, string nick, int myID)
    {
        PhotonView.Find(ID).gameObject.GetComponent<HPScript>().ChangeHP(-damage, new Vector3(x, y + 1, z), Vector3.up, 100f);
        PhotonView.Find(ID).gameObject.GetComponent<HPScript>().SetNickname(nick, myID);
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
