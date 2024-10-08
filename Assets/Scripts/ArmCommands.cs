using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmCommands : MonoBehaviour
{
    [SerializeField] GameObject dropArm;
    [SerializeField] bool isLeftArm;
    [SerializeField] int armIndex;

    public void DropArm()
    {
        if(armIndex == 0) return;
        transform.parent.parent.GetComponent<ArmsManager>().DropArm(isLeftArm, armIndex);
        dropArm.SetActive(true);
        dropArm.GetComponent<Pickup>().renderer.material = GetComponent<ArmDurabilityManager>().currentMat;
        dropArm.transform.SetParent(null);

        dropArm.GetComponent<Pickup>().dropRenderer.material = GetComponent<ArmDurabilityManager>().currentMat;


        dropArm.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void Attack()
    {
        transform.parent.parent.GetComponent<CharacterCombatManager>().Attack(isLeftArm);
    }

    public void PlaySwing()
    {
        transform.parent.parent.GetComponent<ArmsManager>().PlaySwing(isLeftArm);
    }
}
