using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Renderer renderer;
    public int pickupIndex;
    public float maxDurability = 100;
    public float durability = 100;


    public bool isStoreItem;
    public int price;

    void Update()
    {
        if(transform.position.y <= -50f)
        {
            transform.position = new Vector3(0f, 50f, 0f);
        }
    }
}
