using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using Combat;
using Pathfinding;

public class PCO_BaseOrcaBehaviour : PCO_OrcaRoot
{
    public enum BaseOrcaSMBState { WANDER, APPROACH, CIRCLE, ATTACK, RELOCATE };
    public BaseOrcaSMBState baseShooterSMBState;

    public float walkSpeed, runSpeed, circleRotateSpeed;
    int animationIndex; //0 = Idle, 1 = Idle up, 2 = Walk, 3 = Walk up, 4 = Buildup, 5 = Buildup up, 6 = Shoot, 7 = Shoot up, 8 = Recover, 9 = Recover up
    bool isUp, isRight;
    public GameObject circleCenter;
    PCO_OrcaCircleCenterBehaviour circleCenterBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        circleCenterBehaviour = circleCenter.GetComponent<PCO_OrcaCircleCenterBehaviour>();
        AwakeDuPauvre();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
