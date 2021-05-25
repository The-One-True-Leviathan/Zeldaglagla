using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCO_BaseOrca_SMB_Attack : StateMachineBehaviour
{
    public PCO_BaseOrcaBehaviour baseOrca;
    enum State { BUILDUP, HITSPAN, RECOVER };
    State state;
    float maxBuildup, buildup, maxHitspan, hitspan, maxRecover, recover;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseOrca.isInAttack = true;
        buildup = hitspan = recover;
        maxBuildup = baseOrca.atkBuildup;
        maxHitspan = baseOrca.atkHitspan;
        maxRecover = baseOrca.atkRecover;
        baseOrca.destinationSetter.enabled = false;
        baseOrca.pather.enabled = false;
        state = State.BUILDUP;
        baseOrca.baseOrcaSMBState = PCO_BaseOrcaBehaviour.BaseOrcaSMBState.ATTACK;
        baseOrca.pather.maxSpeed = baseOrca.runSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (state)
        {
            case State.BUILDUP:
                buildup += Time.deltaTime;
                if (buildup > maxBuildup)
                {
                    state = State.HITSPAN;
                    Debug.LogWarning("Orca is Going into Hitspan");
                }
                break;
            case State.HITSPAN:
                hitspan += Time.deltaTime;
                Debug.LogWarning("Hitspan time so far : " + hitspan);
                baseOrca.Attack();
                if (hitspan > maxHitspan)
                {
                    state = State.RECOVER;
                    Debug.LogWarning("Orca is Going into Recover");
                }
                break;
            case State.RECOVER:
                recover += Time.deltaTime;
                if (recover > maxRecover)
                {
                    Debug.LogWarning("Orca is Going into Circle");
                    animator.Play("Circle");
                }
                break;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseOrca.isInAttack = false;
    }
}
