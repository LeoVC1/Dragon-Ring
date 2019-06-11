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
    public HPScript myHPScript;
    public AvatarCombat myAvatarCombat;
    public int myCharacterID;

    public float health;
    public float maxHealth;

    public Camera myCamera;
    public AudioListener myAL;

    public Animator animator;
    private float timer = 5;
    public bool died = false;
    public bool locker = false;

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

    private void Update()
    {
        if (!PV.IsMine)
            return;
        if (died)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else
                GameSetup.GS.DisconnectPlayer();
        }
        if (PhotonRoom.room.winner && !locker)
        {
            Win();
            locker = true;
        }
    }

    public void ChangeHP(float value)
    {
        health = value;
        if (PV.IsMine)
        {
            GameSetup.GS.playerHealthBar.fillAmount = health / maxHealth;
            GameSetup.GS.playerHealthValue.text = System.Convert.ToInt32(health).ToString();

        }
    }

    public void ChangeMaxHP(float value)
    {
        maxHealth = value;
        if (PV.IsMine)
        {
            GameSetup.GS.playerHealthBar.fillAmount = health / maxHealth;
            GameSetup.GS.playerMaxHealthValue.text = System.Convert.ToInt32(maxHealth).ToString();
        }
    }

    public void Die(string nick, int ID)
    {
        if (!PV.IsMine)
            return;
        died = true;
        GameSetup.GS.loserLabel.SetActive(true);
        GameSetup.GS.killerNickname.text = "Killed by " + nick;
        GameSetup.GS.finalKills.text = "You killed " + GetComponent<AvatarCombat>().kills.ToString() + " players!!!";
        animator.SetBool("Die", died);
        AS.animator.GetComponent<BoxCollider>().enabled = true;
        AS.animator.GetComponent<Rigidbody>().isKinematic = false;
        AS.GetComponent<Rigidbody>().useGravity = false;
        AS.GetComponent<CapsuleCollider>().enabled = false;
        if (ID != -1)
            PV.RPC("RPC_AddKillToKiller", RpcTarget.All, ID);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Win()
    {
        died = true;
        GameSetup.GS.loserLabel.SetActive(true);
        GameSetup.GS.killerNickname.text = "You win!";
        GameSetup.GS.finalKills.text = "You killed " + GetComponent<AvatarCombat>().kills.ToString() + " players!!!";
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
        aSetup.myChangeItem.mySetup = this;
    }

    [PunRPC]
    void RPC_AddKillToKiller(int ID, int myID)
    {
        AvatarSetup AS = PhotonView.Find(myID).gameObject.GetComponent<AvatarSetup>();
        AS.animator.GetComponent<BoxCollider>().enabled = true;
        AS.animator.GetComponent<Rigidbody>().isKinematic = false;
        AS.GetComponent<Rigidbody>().useGravity = false;
        AS.GetComponent<CapsuleCollider>().enabled = false;
        PhotonView.Find(ID).gameObject.GetComponent<AvatarCombat>().Kill();
        PhotonRoom.room.PlayerDied();
    }

}
