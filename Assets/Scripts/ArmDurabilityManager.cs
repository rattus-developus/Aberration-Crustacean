using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmDurabilityManager : MonoBehaviour
{
    [SerializeField] bool isLeft;
    [SerializeField] Renderer model;
    [SerializeField] Pickup pickup;
    [SerializeField] Material[] mats;
    public Material currentMat;

    public void LoseDurability(float amount)
    {
        pickup.durability -= amount;
    }

    public void SetDurability(float amount)
    {
        pickup.durability = amount;
    }

    public void Update()
    {
        if(pickup.durability / pickup.maxDurability == 1f)
        {
            model.material = mats[0];
            currentMat = mats[0];
        }
        if(pickup.durability / pickup.maxDurability <= 0.5f)
        {
            model.material = mats[1];
            currentMat = mats[1];
        }
        if(pickup.durability / pickup.maxDurability <= 0.2f)
        {
            model.material = mats[2];
            currentMat = mats[2];
        }
        if(pickup.durability <= 0f)
        {
            transform.parent.parent.GetComponent<ArmsManager>().EquipArm(isLeft, 0, -1);
            //Debug.Log("break");
        }
    }
}
