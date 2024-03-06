using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsManager : MonoBehaviour
{
    [SerializeField] GameObject[] armPrefabsLeft;
    [SerializeField] GameObject[] armPrefabsRight;
    public int equippedArmLeft;
    public int equippedArmRight;
    public GameObject spawnedArmLeft;
    public GameObject spawnedArmRight;
    [SerializeField] Transform cam;
    
    public void EquipArm(bool left, int newArmIndex, float durability)
    {
        if(left)
        {
            equippedArmLeft = newArmIndex;
            if(spawnedArmLeft != null) Destroy(spawnedArmLeft);
            spawnedArmLeft = Instantiate(armPrefabsLeft[newArmIndex]);
            spawnedArmLeft.transform.SetParent(cam, false);

            if(durability != -1 && spawnedArmLeft.GetComponent<ArmDurabilityManager>() != null) spawnedArmLeft.GetComponent<ArmDurabilityManager>().SetDurability(durability);
        }
        else
        {
            equippedArmRight = newArmIndex;
            if(spawnedArmRight != null) Destroy(spawnedArmRight);
            spawnedArmRight = Instantiate(armPrefabsRight[newArmIndex]);
            spawnedArmRight.transform.SetParent(cam, false);

            if(durability != -1 && spawnedArmRight.GetComponent<ArmDurabilityManager>() != null) spawnedArmRight.GetComponent<ArmDurabilityManager>().SetDurability(durability);
        }
    }

    void Awake()
    {
        EquipArm(true, equippedArmLeft, -1);
        EquipArm(false, equippedArmRight, -1);
    }

    void Update()
    {
        //Punch
        spawnedArmLeft.GetComponent<Animator>().SetBool("punching", Input.GetKey(KeyCode.Mouse0));
        spawnedArmRight.GetComponent<Animator>().SetBool("punching", Input.GetKey(KeyCode.Mouse1));

        //Drop
        AnimatorClipInfo[] animatorinfoLeft = spawnedArmLeft.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        AnimatorClipInfo[] animatorinfoRight = spawnedArmRight.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);

        if(Input.GetKeyDown(KeyCode.Q) && animatorinfoLeft[0].clip.name == "Idle") spawnedArmLeft.GetComponent<Animator>().SetTrigger("drop");
        if(Input.GetKeyDown(KeyCode.E) && animatorinfoRight[0].clip.name == "Idle") spawnedArmRight.GetComponent<Animator>().SetTrigger("drop");
    }

    public void DropArm(bool left, int armIndex)
    {
        if(left)
        {
            EquipArm(true, 0, -1);
        }
        else
        {
            EquipArm(false, 0, -1);
        }
    }
}
