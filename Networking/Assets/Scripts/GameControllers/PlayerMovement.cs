using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{

    PhotonView PV;
    public float maxSpeed = 2;
    public float runSpeed = 1;
    public float rotationSpeed = 50;
    bool isRunning = false;

    private AvatarSetup avatarSetup;
    AvatarCombat avatarCombat;
    public bool isAttacking;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
            return;
        Cursor.visible = false;
        avatarSetup = GetComponent<AvatarSetup>();
        avatarCombat = GetComponent<AvatarCombat>();
    }

    
    void Update()
    {
        if (PV.IsMine && ShopController.SC.isShopping == false)
        {
            if(isAttacking == false)
            {
                RotateToForward();
                float speed = BasicMovement();
                BasicRotation();
                RunningMovement(speed);
                Attack();
            }
        }
    }

    float BasicMovement()
    {
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        float speed = 0;

        if (ver != 0)
            speed = ver;
        else if (hor != 0)
            speed = hor;

        speed = Mathf.Abs(speed);
        transform.position += avatarSetup.myCharacter.transform.forward * (maxSpeed * speed * runSpeed) * Time.deltaTime;

        avatarSetup.animator.SetFloat("Speed", Mathf.Abs(speed));

        return speed;
    }

    void BasicRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed;
        transform.Rotate(new Vector3(0, mouseX, 0));
    }

    void RotateToForward()
    {
        Vector3 originEulerAngles = avatarSetup.myCharacter.transform.eulerAngles;

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            originEulerAngles.y = avatarSetup.myCamera.transform.eulerAngles.y + 45;
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            originEulerAngles.y = avatarSetup.myCamera.transform.eulerAngles.y - 45;
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            originEulerAngles.y = avatarSetup.myCamera.transform.eulerAngles.y + 135;
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            originEulerAngles.y = avatarSetup.myCamera.transform.eulerAngles.y + 225;
        else if (Input.GetKey(KeyCode.W))
            originEulerAngles.y = avatarSetup.myCamera.transform.eulerAngles.y;
        else if (Input.GetKey(KeyCode.A))
            originEulerAngles.y = avatarSetup.myCamera.transform.eulerAngles.y - 90;
        else if (Input.GetKey(KeyCode.S))
            originEulerAngles.y = avatarSetup.myCamera.transform.eulerAngles.y + 180;
        else if (Input.GetKey(KeyCode.D))
            originEulerAngles.y = avatarSetup.myCamera.transform.eulerAngles.y + 90;

        avatarSetup.myCharacter.transform.eulerAngles = originEulerAngles;

    }

    void RunningMovement(float speed)
    {
        if (Input.GetKey(KeyCode.LeftShift) && speed != 0)
        { 
            isRunning = true;
            runSpeed = 2f;
        }
        else
        {
            isRunning = false;
            runSpeed = 1;
        }
        avatarSetup.animator.SetBool("Run", isRunning);
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(Random.Range(0,2) == 0)
                avatarSetup.animator.SetTrigger("Attack1");
            else
                avatarSetup.animator.SetTrigger("Attack2");
            isAttacking = true;
            Invoke("EndAttack", 1.5f);
        }
    }

    void EndAttack()
    {
        avatarSetup.animator.ResetTrigger("Attack1");
        avatarSetup.animator.ResetTrigger("Attack2");
        isAttacking = false;
        avatarCombat._lock = false;
    }

}
