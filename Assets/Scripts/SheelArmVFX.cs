using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.VFX;

public class SheelArmVFX : MonoBehaviour
{
    [SerializeField] VisualEffect zapVFX;
    [SerializeField] Transform zapStart;
    public void PlayEffect()
    {
        zapVFX.SetVector3("Start", zapStart.position);
        zapVFX.SetVector3("End", zapStart.position + zapStart.forward * 2f);
        zapVFX.Play();
    }
}
