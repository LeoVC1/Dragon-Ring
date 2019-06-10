using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PhotonLobby : MonoBehaviourPunCallbacks
{

    public static PhotonLobby lobby;

    public TextMeshProUGUI serverStatus;
    public GameObject battleButton;
    public GameObject cancelButton;
    public GameObject model;
    public TMP_InputField nickName;
    bool online;
    bool searching;

    //WAITING LABEL:
    public GameObject waitingLabel;
    public TextMeshProUGUI waitingText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI playersCountText;
    public TextMeshProUGUI maxPlayersText;
    public Image fadeOut;


    private void Awake()
    {
        lobby = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Player has connected to the Photon Server");
        serverStatus.text = "Online";
        PhotonNetwork.AutomaticallySyncScene = true;
        online = true;
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        battleButton.SetActive(false);
        serverStatus.text = "Offline";
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("Tried to join a random game but failed. There must be no open games available");
        CreateRoom();
    }

    void CreateRoom()
    {
        print("Trying to create a new Room");
        int randomRoomName = Random.Range(0, 1);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSettings.multiplayerSettings.maxPlayers };
        PhotonNetwork.CreateRoom("Room: " + randomRoomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Tried to create a new room but failed, there must already be a room with the same name");
        CreateRoom();
    }

    public void OnBattleButtonClicked()
    {
        waitingLabel.SetActive(true);
        print("Battle Button was click");
        nickName.gameObject.SetActive(false);
        model.SetActive(false);
        searching = true;
        battleButton.SetActive(false);
        cancelButton.SetActive(true);

        PhotonNetwork.JoinRandomRoom();
    }

    public void OnCancelButtonClicked()
    {
        waitingLabel.SetActive(false);
        searching = false;
        model.SetActive(true);
        nickName.gameObject.SetActive(true);
        cancelButton.SetActive(false);
        battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

    private void Update()
    {
        if (online == true && nickName.text.Length > 3 && searching == false)
        {
            battleButton.SetActive(true);
        }
        else
        {
            battleButton.SetActive(false);
        }

    }



}
