using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public GameObject pickupImage;

    public void PickUp()
    {
        Destroy(gameObject);
    }
}
