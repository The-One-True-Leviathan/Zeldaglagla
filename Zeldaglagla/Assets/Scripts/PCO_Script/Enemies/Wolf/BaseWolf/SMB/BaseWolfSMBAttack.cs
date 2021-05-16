using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseWolfSMBAttack : StateMachineBehaviour
{
    enum CmbtState { NONE, BUILDUP, HITSPAN, RECOVER };
    public BaseWolf baseWolf;
    public float buildupTime, recoverTime, numberDiceNextAtkTime, valueDiceNextAtkTime, modifierDiceNextAtkTime;
    float buildup, recover;
    CmbtState cmbtState;
    Vector3 target;

    public Vector3 vectorToSprint;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        buildup = recover = 0;
        baseWolf.baseWolfSMBState = BaseWolf.BaseWolfSMBState.ATTACK;
        baseWolf.pather.maxSpeed = baseWolf.observeSpeed;
        Debug.LogWarning("HEOOOO");
        baseWolf.destinationSetter.target = GameObject.FindGameObjectWithTag("Player").transform;
        cmbtState = CmbtState.NONE;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (cmbtState)
        {
            case CmbtState.NONE:
                if (Vector2.Distance(baseWolf.transform.position, baseWolf.player.transform.position) <= baseWolf.pounceLength/2)
                {
                    cmbtState = CmbtState.BUILDUP;
                    buildup = 0;
                    baseWolf.pather.enabled = false;
                    target = baseWolf.player.transform.position;
                }
                break;
            case CmbtState.BUILDUP:
                buildup += Time.deltaTime;
                if (buildup >= buildupTime)
                {
                    baseWolf.pather.enabled = true;
                    cmbtState = CmbtState.HITSPAN;
                    if (baseWolf.pack.wolfInAttack == baseWolf)
                    {
                        float time = 0;
                        for (int i = 0; i < numberDiceNextAtkTime; i++)
                        {
                            time += Random.Range(0, valueDiceNextAtkTime);
                        }
                        time += modifierDiceNextAtkTime;
                        baseWolf.pack.SetNextAttack(time);
                        RunCast();
                    }
                }
                break;
            case CmbtState.HITSPAN:
                baseWolf.isInAttack = true;
                baseWolf.Attack();
                if (baseWolf.pather.remainingDistance <= 0.5)
                {
                    baseWolf.isInAttack = false;
                    baseWolf.destinationSetter.enabled = true;
                    cmbtState = CmbtState.RECOVER;
                    recover = 0;
                }
                break;
            case CmbtState.RECOVER:
                baseWolf.pather.maxSpeed = baseWolf.observeSpeed;
                recover += Time.deltaTime;
                if (recover >= recoverTime)
                {
                    animator.Play("Harass");
                }
                break;
        }
        
    }

    void RunCast()
    {
        baseWolf.destinationSetter.enabled = false;
        baseWolf.pather.maxSpeed = baseWolf.pounceSpeed;
        RaycastHit2D hit2D;
        float hitLength = baseWolf.pounceLength;

        hit2D = Physics2D.Raycast(baseWolf.transform.position, target - baseWolf.transform.position, hitLength, baseWolf.blocksLOS);
        if (hit2D.collider)
        {
            hitLength = hit2D.distance;
        }


        Vector2 nik = target - baseWolf.transform.position;
        nik.Normalize();
        vectorToSprint = nik * hitLength;


        baseWolf.pather.destination = baseWolf.transform.position + vectorToSprint;
        baseWolf.pather.SearchPath();
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
