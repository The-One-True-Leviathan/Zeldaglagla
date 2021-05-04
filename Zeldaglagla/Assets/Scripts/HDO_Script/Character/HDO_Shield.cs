using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

public class HDO_Shield : MonoBehaviour
{
    [Header("Shield")]
    [SerializeField]
    GameObject shield;
    [SerializeField]
    float perfectShieldTiming, shieldDecreaseRate, shieldRegrowRate, shieldMin;
    float elapsedTime;

    
}
