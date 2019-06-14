using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public PhotonLobby lobby;
    public static PhotonRoom room;
    private PhotonView PV;
    public int multiplayerScene;
    private int currentScene;
    public TextMeshProUGUI roomName;
    public bool isGameLoaded;

    //Players
    private Player[] photonPlayers;
    public int playersInRoom;
    public int myNumberInRoom;

    public int playersInGame;

    private bool readyToCount;
    private bool readyToStart;
    public float startingTime;
    private float lessThanMaxPlayers;
    private float atMaxPlayers;
    private float timeToStart;
    public int myNumber;
    public int playersAlive;
    public bool winner;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (room == null)
        {
            room = this;
        }
        else
        {
            if (room != this)
            {
                Destroy(room.gameObject);
                room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
        PV = GetComponent<PhotonView>();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinisedLoading;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinisedLoading;
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Has joined room");
        photonPlayers = PhotonNetwork.PlayerList;
        print("Player List : " + photonPlayers.Length);
        playersInRoom = photonPlayers.Length;
        myNumber = photonPlayers.Length;
        PhotonNetwork.NickName = lobby.nickName.text;
        if (MultiplayerSettings.multiplayerSettings.delayStart)
        {
            Debug.Log("Displayer players in room out of max players possible (" + playersInRoom + ":" + MultiplayerSettings.multiplayerSettings.maxPlayers + ")");
            if (playersInRoom > 1)
            {
                readyToCount = true;
            }
            if (playersInRoom == MultiplayerSettings.multiplayerSettings.maxPlayers)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient)
                    return;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
        else
        {
            if (!PhotonNetwork.IsMasterClient)
                return;
            StartGame();
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("A new players has joined the room");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;
        if (MultiplayerSettings.multiplayerSettings.delayStart)
        {
            Debug.Log("Displayer players in room out of max players possible (" + playersInRoom + ":" + MultiplayerSettings.multiplayerSettings.maxPlayers + ")");
            if (playersInRoom > 1)
            {
                readyToCount = true;
            }
            if (playersInRoom == MultiplayerSettings.multiplayerSettings.maxPlayers)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient)
                    return;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
    }

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        readyToCount = false;
        readyToStart = false;
        lessThanMaxPlayers = startingTime;
        atMaxPlayers = 6;
        timeToStart = startingTime;
    }

    [PunRPC]
    void RPC_UpdateTimer()
    {
        if (readyToStart)
        {
            atMaxPlayers -= Time.deltaTime;
            lessThanMaxPlayers = atMaxPlayers;
            timeToStart = atMaxPlayers;
        }
        else if (readyToCount)
        {
            lessThanMaxPlayers -= Time.deltaTime;
            timeToStart = lessThanMaxPlayers;
        }
    }

    private void Update()
    {
        if (MultiplayerSettings.multiplayerSettings.delayStart)
        {
            if (playersInRoom == 1)
            {
                RestartTimer();
            }
            if (!isGameLoaded)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    PV.RPC("RPC_UpdateTimer", RpcTarget.AllBuffered);
                }
                Debug.Log("Display time to start to the players - " + timeToStart);
                lobby.waitingText.text = "Waiting for players!";
                lobby.timerText.text = System.Convert.ToInt32(timeToStart).ToString() + "s";
                lobby.playersCountText.text = playersInRoom.ToString();
                lobby.maxPlayersText.text = MultiplayerSettings.multiplayerSettings.maxPlayers.ToString();
                if (timeToStart <= 0)
                {
                    lobby.waitingText.text = "Loading game...";
                    lobby.timerText.enabled = false;
                    lobby.playersCountText.enabled = false;
                    lobby.maxPlayersText.enabled = false;
                    lobby.cancelButton.SetActive(false);
                    Color a = lobby.fadeOut.color;
                    a.a = 1;
                    lobby.fadeOut.color = a;
                    playersAlive = playersInRoom;
                    StartGame();
                    timeToStart = startingTime;
                }
            }
        }
        if (currentScene == MultiplayerSettings.multiplayerSettings.multiplayerScene)
        {
            GameSetup.GS.playersCountText.text = playersAlive.ToString();
            if (playersAlive == 1)
            {
                winner = true;
            }
        }
    }

    void StartGame()
    {
        Debug.Log("Loading Level");
        isGameLoaded = true;
        if (!PhotonNetwork.IsMasterClient)
            return;
        if (MultiplayerSettings.multiplayerSettings.delayStart)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        PhotonNetwork.LoadLevel(MultiplayerSettings.multiplayerSettings.multiplayerScene);
    }

    void RestartTimer()
    {
        lessThanMaxPlayers = startingTime;
        timeToStart = startingTime;
        atMaxPlayers = 6;
        readyToCount = false;
        readyToStart = false;
    }

    void OnSceneFinisedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if (currentScene == MultiplayerSettings.multiplayerSettings.multiplayerScene)
        {
            isGameLoaded = true;
            playersAlive = playersInGame;
            if (MultiplayerSettings.multiplayerSettings.delayStart)
            {
                PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
            }
            else
            {
                CreatePlayer();
            }
        }
    }

    [PunRPC]
    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
        PhotonRoom.room.playersAlive = playersInRoom;
        GameSetup.GS.playersCountText.text = playersAlive.ToString();
    }

    [PunRPC]
    void RPC_LoadedGameScene()
    {
        playersInGame++;
        if (playersInGame == PhotonNetwork.PlayerList.Length)
        {
            PV.RPC("CreatePlayer", RpcTarget.All);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        playersInGame--;
    }

    
    public void PlayerDied()
    {
        PV.RPC("Morri", RpcTarget.All);
    }

    [PunRPC]
    public void Morri()
    {
        playersAlive--;
        GameSetup.GS.playersCountText.text = playersAlive.ToString();
    }
}