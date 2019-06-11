using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;

    public Transform[] spawnPoints;

    public Image playerHealthBar;
    public Image playerManaBar;
    public GameObject potionImage;
    public TextMeshProUGUI potionsCount;

    public TextMeshProUGUI playerHealthValue;
    public TextMeshProUGUI playerMaxHealthValue;

    public GameObject escButtons;

    //LOSER LABEL
    public GameObject loserLabel;
    public TextMeshProUGUI killerNickname;
    public TextMeshProUGUI kills;
    public TextMeshProUGUI finalKills;


    public TextMeshProUGUI playersCountText;
    public RawImage minimapImage;

    public float safeZoneRadius = 250f;
    private float decreaseFlux = 1f;
    public Transform safeZoneCenter;
    private bool readyToCount;
    private float safeZoneTimer = 45;
    private float safeTimer = 45;
    public int waves = 1;
    public TextMeshProUGUI safeZoneTimerText;
    public Transform circle;
    Vector3 startScale;

    public DragonMovement dragonMove;

    bool decrease = false;
    bool lockDragon = false;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(safeZoneCenter.position, safeZoneRadius);
    }


    private void OnEnable()
    {
        if (GS == null)
        {
            GS = this;
        }
        startScale = circle.localScale;
        circle.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if(safeZoneRadius > 30)
        {
            if (readyToCount)
            {
                circle.gameObject.SetActive(true);
                if (safeZoneTimer >= 0)
                {
                    safeZoneRadius -= decreaseFlux * Time.deltaTime;
                    circle.localScale = new Vector3(safeZoneRadius * startScale.x / 365, startScale.y, safeZoneRadius * startScale.z / 365);
                    safeZoneTimer -= Time.deltaTime;
                    if (lockDragon == false)
                    {
                        dragonMove.StartMovement();
                        lockDragon = true;
                    }
                }
                else
                {
                    dragonMove.StopMovement();
                    safeZoneTimer = 45;
                    safeZoneTimerText.color = Color.green;
                    readyToCount = false;
                    if (decrease)
                        if (waves > 2)
                            waves--;
                        else
                            decrease = false;
                    else if (waves < 6)
                        waves++;
                    else
                        decrease = true;
                    decreaseFlux = waves * 1f;
                }
                safeZoneTimerText.text = IntToSeconds(System.Convert.ToInt32(safeZoneTimer));
            }
            else
            {
                if (safeTimer <= 0)
                {
                    lockDragon = false;
                    readyToCount = true;
                    safeZoneTimerText.color = Color.red;
                    safeTimer = 45;
                }
                else
                {
                    safeTimer -= Time.deltaTime;
                }
                safeZoneTimerText.text = IntToSeconds(System.Convert.ToInt32(safeTimer));
            }
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escButtons.activeSelf == false)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                escButtons.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                escButtons.SetActive(false);
            }
        }
    }

    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        PhotonRoom.room.PlayerDied();
        SceneManager.LoadScene(MultiplayerSettings.multiplayerSettings.menuScene);
    }

    public void OnButtonQuit()
    {
        Application.Quit();
    }

    string IntToSeconds(int value)
    {
        var minutes = Mathf.FloorToInt(value / 60);
        var seconds = value % 60;

        return string.Format($"{minutes.ToString("00")}:{seconds.ToString("00")}");
    }
}
