using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFixer : MonoBehaviour
{
    void Update()
    {
        if(transform.position.y <= -50f)
        {
            transform.position = new Vector3(0f, 50f, 0f);
        }
    }
}
