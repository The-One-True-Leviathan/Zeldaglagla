using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using Combat;
using Pathfinding;

public class PCO_BaseOrcaBehaviour : PCO_OrcaRoot
{
    public enum BaseOrcaSMBState { WANDER, APPROACH, CIRCLE, ATTACK };
    public BaseOrcaSMBState baseOrcaSMBState;

    public float walkSpeed, runSpeed, circleRotateSpeed, viewDistance, circleDistance, attackDistance = 0.8f;
    int animationIndex; //0 = Idle, 1 = Idle up, 2 = Walk, 3 = Walk up, 4 = Buildup, 5 = Buildup up, 6 = Shoot, 7 = Shoot up, 8 = Recover, 9 = Recover up
    bool isUp, isRight;
    public GameObject circleCenter;
    [System.NonSerialized]
    public PCO_OrcaCircleCenterBehaviour circleCenterBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        circleCenterBehaviour = circleCenter.GetComponent<PCO_OrcaCircleCenterBehaviour>();
        circleCenterBehaviour.AssignOrca(transform, this);
        AwakeDuPauvre();
        SMB.GetBehaviour<PCO_BaseOrca_SMB_Wander>().baseOrca = this;
        SMB.GetBehaviour<PCO_BaseOrca_SMB_Circle>().baseOrca = this;
        SMB.GetBehaviour<PCO_BaseOrca_SMB_Approach>().baseOrca = this;
        SMB.GetBehaviour<PCO_BaseOrca_SMB_Attack>().baseOrca = this;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCircling()
    {
        circleCenterBehaviour.CenterOnPlayer();
        circleCenterBehaviour.Circle(runSpeed, circleDistance);
        SMB.Play("Circle");
    }

    public override bool Damage(DamageStruct damageTaken)
    {
        if (isInAttack || stunned)
        {
            return IsDamaged(damageTaken);
        }
        return false;
    }

    public override bool Damage(DamageStruct damageTaken, bool isThisATorchAtk)
    {
        if (isThisATorchAtk)
        {
            return IsDamaged(damageTaken);
        }
        return false;
    }

    bool IsDamaged(DamageStruct damageTaken)
    {
        if (!stunned)
        {
            currentHP -= damageTaken.dmg;

            if (damageTaken.stn.str > stunThreshold)
            {
                CombatEvents.monsterWasHit.Invoke(CombatEvents.hitStunned);
                accumulatedStun = 0;
                Stun(damageTaken.stn);
            }
            else
            {
                accumulatedStun += damageTaken.stn.str;
                if (accumulatedStun > stunThreshold)
                {
                    CombatEvents.monsterWasHit.Invoke(CombatEvents.hitStunned);
                    accumulatedStun = 0;
                    Stun(damageTaken.stn);
                }
                else
                {
                    CombatEvents.monsterWasHit.Invoke(CombatEvents.hitNotStunned);
                }
            }
        }
        else
        {
            currentHP -= stunMultiplier * damageTaken.dmg;
        }



        if (currentHP <= 0)
        {
            Death();
            return true;
        }
        else
        {
            return false;
        }
    }



    public void SetAnim(string anim, Vector3 direction)
    {
        if (direction.x > 0)
        {
            animator.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction.x < 0)
        {
            animator.transform.localScale = new Vector3(1, 1, 1);
        }

        if (anim == "Swim")
        {
            if (direction.y >= 0)
            {
                anim += "_Back";
            }
            else if (direction.y < 0)
            {
                anim += "_Front";
            }
        }
        Debug.LogWarning(anim);
        animator.Play(anim);
    }

}
