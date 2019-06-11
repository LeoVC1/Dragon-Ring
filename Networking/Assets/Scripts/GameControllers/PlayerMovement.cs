using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{

    Image staminaBar;
    PhotonView PV;
    public float maxSpeed = 2;
    public float runSpeed = 1;
    public float rotationSpeed = 50;
    public float stamina;
    public bool isPickingItem = false;
    bool isRunning = false;
    bool isBreathing = false;

    private AvatarSetup avatarSetup;
    AvatarCombat avatarCombat;
    public bool isAttacking;

    Transform safeZoneCenter;
    Vector2 positionCenterXZ;
    HPScript myHPScript;

    public Transform camT;
    Vector3 camF;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
            return;
        Cursor.visible = false;
        avatarSetup = GetComponent<AvatarSetup>();
        avatarCombat = GetComponent<AvatarCombat>();
        myHPScript = GetComponent<HPScript>();
        staminaBar = GameSetup.GS.playerManaBar;

        safeZoneCenter = GameSetup.GS.safeZoneCenter;
        positionCenterXZ = new Vector2(safeZoneCenter.position.x, safeZoneCenter.position.z);
    }

    void Update()
    {
        if (avatarSetup.died)
            return;

        if (PV.IsMine)
        {
            if (isAttacking == false && isPickingItem == false && avatarSetup.myInventario.isDrinking == false)
            {
                RotateToForward();
                float speed = BasicMovement();
                BasicRotation();
                RunningMovement(speed);
                Attack();
            }
        }
    }

    private void FixedUpdate()
    {
        if (PV.IsMine && avatarSetup.died == false)
            VerifySafeZone();
    }

    float BasicMovement()
    {
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        float speed = 0;
        speed += Mathf.Clamp(Mathf.Abs(ver) + Mathf.Abs(hor), 0, 1);
        speed = Mathf.Abs(speed);
        transform.position += avatarSetup.myCharacter.transform.forward * (maxSpeed * speed * runSpeed) * Time.deltaTime;

        avatarSetup.animator.SetFloat("Speed", Mathf.Abs(speed));

        return speed;
    }

    void BasicRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed;
        //transform.Rotate(new Vector3(0, mouseX, 0));
    }

    void RotateToForward()
    {
        camF = Vector3.Scale(camT.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDir = camT.right * Input.GetAxis("Horizontal") + camF * Input.GetAxis("Vertical");
        if (moveDir.magnitude > 0)
            avatarSetup.myCharacter.transform.forward = Vector3.Lerp(avatarSetup.myCharacter.transform.forward, moveDir, 0.5f);


    }

    void RunningMovement(float speed)
    {
        if (Input.GetKey(KeyCode.LeftShift) && speed != 0 && stamina > 0 && isBreathing == false)
        {
            isRunning = true;
            runSpeed = 2.5f;
            stamina -= 15 * Time.deltaTime;
        }
        else
        {
            isRunning = false;
            runSpeed = 1;
            if (stamina < 100)
                stamina += 10 * Time.deltaTime;
            if (isBreathing == false && stamina < 0)
            {
                isBreathing = true;
                Invoke("TakeABreath", 2);
            }
        }
        avatarSetup.animator.SetBool("Run", isRunning);
        staminaBar.fillAmount = stamina / 100f;
    }

    void TakeABreath()
    {
        isBreathing = false;
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Random.Range(0, 2) == 0)
                avatarSetup.animator.SetTrigger("Attack1");
            else
                avatarSetup.animator.SetTrigger("Attack2");
            isAttacking = true;
            Invoke("EndAttack", 1.1f);
        }
    }

    void EndAttack()
    {
        avatarSetup.animator.ResetTrigger("Attack1");
        avatarSetup.animator.ResetTrigger("Attack2");
        isAttacking = false;
        avatarCombat._lock = false;
    }

    void VerifySafeZone()
    {
        Vector2 positionXZ = new Vector2(transform.position.x, transform.position.z);
        float distanceToCenter = Vector2.Distance(positionXZ, positionCenterXZ);
        
        if (distanceToCenter > GameSetup.GS.safeZoneRadius)
        {
            PV.RPC("RPC_TakeDamage", RpcTarget.All, PV.ViewID, -GameSetup.GS.waves * Time.deltaTime);
        }
    }

    [PunRPC]
    void RPC_TakeDamage(int ID, float damage)
    {
        PhotonView.Find(ID).gameObject.GetComponent<AvatarSetup>().myHPScript.ChangeHPValue(damage);
        PhotonView.Find(ID).gameObject.GetComponent<AvatarSetup>().myHPScript.SetNickname("fogo!");
    }

}
