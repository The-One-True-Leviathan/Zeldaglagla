using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDO_UniversalEnemy : MonoBehaviour
{
    [SerializeField]
    float willPower;
    public float currentWillPower;

    [SerializeField]
    int healthPoints;
    public int currentHealthPoints;


    private void Start()
    {
        currentHealthPoints = healthPoints;
        currentWillPower = willPower;
    }
}
