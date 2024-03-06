using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombatManager : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] BoxCollider leftHitbox;
    [SerializeField] BoxCollider rightHitbox;
    [SerializeField] float[] armDamages;
    [SerializeField] float[] armKnockbacks;
    [SerializeField] float[] armKnockbackTimes;

    public void Attack(bool isLeft)
    {
        if(isLeft)
        {
            Vector3 size = new Vector3(leftHitbox.size.x * leftHitbox.transform.localScale.x * 2, leftHitbox.size.y * leftHitbox.transform.localScale.y * 2, leftHitbox.size.z * leftHitbox.transform.localScale.z * 2);
            
            Collider[] hitColliders = Physics.OverlapBox(leftHitbox.transform.position, size, transform.rotation);
            if(hitColliders.Length > 0)
            {
                CheckHits(isLeft, hitColliders);
            }
        }
        else
        {
            Vector3 size = new Vector3(rightHitbox.size.x * rightHitbox.transform.localScale.x * 2, rightHitbox.size.y * rightHitbox.transform.localScale.y * 2, rightHitbox.size.z * rightHitbox.transform.localScale.z * 2);

            Collider[] hitColliders = Physics.OverlapBox(leftHitbox.transform.position, size, transform.rotation);
            if(hitColliders.Length > 0)
            {
                CheckHits(isLeft, hitColliders);
            }
        }
    }

    void CheckHits(bool isLeft, Collider[] hitColliders)
    {
        ArmsManager armsManager = GetComponent<ArmsManager>();

        bool loseDurability = false;

        foreach(Collider col in hitColliders)
        {
            if(col.transform.GetComponent<Pickup>() != null)
            {
                if((armsManager.equippedArmLeft != 0 && isLeft) || (armsManager.equippedArmRight != 0 && !isLeft))
                {
                    return;
                }
                armsManager.EquipArm(isLeft, col.transform.GetComponent<Pickup>().pickupIndex, col.GetComponent<Pickup>().durability);
                Destroy(col.gameObject);

                return;
            }
        }

        foreach(Collider col in hitColliders)
        {
            if(col.transform.GetComponent<Enemy>() != null)
            {
                loseDurability = true;

                if(isLeft)
                {
                    col.transform.GetComponent<Enemy>().TakeHit(armDamages[armsManager.equippedArmLeft], armKnockbacks[armsManager.equippedArmLeft], armKnockbackTimes[armsManager.equippedArmLeft], cam);
                }
                else
                {
                    col.transform.GetComponent<Enemy>().TakeHit(armDamages[armsManager.equippedArmRight], armKnockbacks[armsManager.equippedArmLeft], armKnockbackTimes[armsManager.equippedArmLeft], cam);
                }
            }
        }

        if(loseDurability)
        {
            if(isLeft && armsManager.spawnedArmLeft.GetComponent<ArmDurabilityManager>() != null) armsManager.spawnedArmLeft.GetComponent<ArmDurabilityManager>().LoseDurability(1f);
            else if(!isLeft && armsManager.spawnedArmRight.GetComponent<ArmDurabilityManager>() != null) armsManager.spawnedArmRight.GetComponent<ArmDurabilityManager>().LoseDurability(1f);
        }
    }
}
