using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticles : MonoBehaviour
{

    bool _lock = false;
    Transform safeZoneCenter;
    Vector2 positionCenterXZ;

    void Start()
    {
        safeZoneCenter = GameSetup.GS.safeZoneCenter;
        positionCenterXZ = new Vector2(safeZoneCenter.position.x, safeZoneCenter.position.z);
    }

    void Update()
    {
        VerifySafeZone();
    }

    void VerifySafeZone()
    {
        Vector2 positionXZ = new Vector2(transform.position.x, transform.position.z);
        float distanceToCenter = Vector2.Distance(positionXZ, positionCenterXZ);
        if (distanceToCenter > GameSetup.GS.safeZoneRadius && _lock == false)
        {
            ParticleSystem ps = GetComponent<ParticleSystem>();
            ps.Play(true);
            _lock = true;
        }
    }
}
