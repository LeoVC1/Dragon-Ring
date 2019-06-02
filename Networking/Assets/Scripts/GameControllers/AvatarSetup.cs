using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class AvatarSetup : MonoBehaviour
{
    PhotonView PV;
    public int characterValue;
    public GameObject myCharacter;
    public ChangeItem myChangeItem;
    public Inventario myInventario;
    public int myCharacterID;

    public float health;

    public Camera myCamera;
    public AudioListener myAL;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            AddCharacter(0, "");
        }
        else
        {
            Destroy(myCamera);
            Destroy(myAL);
        }
    }

    void AddCharacter(int whichCharacter, string myNick)
    {
        characterValue = whichCharacter;
        myCharacter = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Characters", "WarriorPrefab"), transform.position - new Vector3(0, 0.5f, 0), transform.rotation);
        myCharacter.transform.parent = transform;
        animator = myCharacter.GetComponent<Animator>();
        myCamera.GetComponentInParent<CameraFollow>().CameraFollowObj = myCharacter.GetComponent<CharacterScript>().neckLocation.gameObject;
        PV.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, PV.ViewID, myCharacter.GetComponent<PhotonView>().ViewID);
    }

    [PunRPC]
    void RPC_AddCharacter(int myID, int myCharacterID)
    {
        AvatarSetup aSetup = PhotonView.Find(myID).GetComponent<AvatarSetup>();
        aSetup.myInventario = aSetup.GetComponent<Inventario>();
        aSetup.myChangeItem = PhotonView.Find(myCharacterID).GetComponent<ChangeItem>();
    }

}
