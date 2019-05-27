using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.IO;

public class Minimap : MonoBehaviour
{

    public GameObject camPrefab;

    RenderTexture rt;

    Camera minimapCamera;

    PhotonView PV;

    AvatarSetup avatarSetup;

    RawImage minimapImage;

    public Image arrowImage;

    
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            avatarSetup = GetComponent<AvatarSetup>();
            minimapImage = GameSetup.GS.minimapImage;
            rt = new RenderTexture(128, 128, 32, RenderTextureFormat.ARGB32);
            rt.Create();
            minimapImage.texture = rt;
            minimapCamera = Instantiate(camPrefab, transform.position + new Vector3(0, 200, 0), Quaternion.identity).GetComponent<Camera>();
            minimapCamera.transform.Rotate(90, 0, 0);
            minimapCamera.targetTexture = rt;
            arrowImage = minimapCamera.GetComponentInChildren<Image>();
        }
    }

    void LateUpdate()
    {
        if (!PV.IsMine)
            return;

        Vector3 newPosition = transform.position;
        newPosition.y = minimapCamera.transform.position.y;
        minimapCamera.transform.position = newPosition;

        arrowImage.transform.rotation = Quaternion.Euler(90f, avatarSetup.myCharacter.transform.eulerAngles.y - 90, 0f);
    }
}
