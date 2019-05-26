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
    private bool isAttacking;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        Cursor.visible = false;
        avatarSetup = GetComponent<AvatarSetup>();
    }

    
    void Update()
    {
        if (PV.IsMine && ShopController.SC.isShopping == false)
        {
            if(isAttacking == false)
            {
                BasicMovement();
                BasicRotation();
                RunningMovement();
                Attack();
            }
        }
    }

    void BasicMovement()
    {
        float speed = Input.GetAxisRaw("Vertical");
        transform.position += transform.forward * (maxSpeed * speed * runSpeed) * Time.deltaTime;

        float rotation = Input.GetAxisRaw("Horizontal") * Time.deltaTime * rotationSpeed;
        transform.Rotate(new Vector3(0, rotation, 0));

        avatarSetup.animator.SetFloat("Speed", Mathf.Abs(speed));
    }

    void BasicRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed;
        transform.Rotate(new Vector3(0, mouseX, 0));
    }

    void RunningMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
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

    public void EndAttack()
    {
        avatarSetup.animator.ResetTrigger("Attack1");
        avatarSetup.animator.ResetTrigger("Attack2");
        isAttacking = false;
    }

}
