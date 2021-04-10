using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseWolfSMBApproach : StateMachineBehaviour
{
    public BaseWolf baseWolf;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseWolf.baseWolfSMBState = BaseWolf.BaseWolfSMBState.APPROACH;
        baseWolf.destinationSetter.enabled = true;
        baseWolf.pather.maxSpeed = baseWolf.approachSpeed;
        Debug.LogWarning("HEOOOO");
        foreach (GameObject holder in baseWolf.player.GetComponent<PlayerPackCircle>().holders)
        {
            if (holder.GetComponent<WolfHolder>().wolf == baseWolf.gameObject)
            {
                baseWolf.destinationSetter.target = holder.transform;
                Debug.LogWarning("Attached");
                break;
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (baseWolf.destinationSetter.target == null)
        {
            baseWolf.destinationSetter.enabled = true;
            foreach (GameObject holder in baseWolf.player.GetComponent<PlayerPackCircle>().holders)
            {
                if (holder.GetComponent<WolfHolder>().wolf == baseWolf.gameObject)
                {
                    baseWolf.destinationSetter.target = holder.transform;
                    Debug.LogWarning("Attached");
                    break;
                }
            }
        }
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        if (baseWolf.ToPlayer().magnitude < baseWolf.pack.avoidDistance)
        {
            Vector2 wolfToPlayer = baseWolf.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
            float adjust = baseWolf.pack.avoidDistance / Vector2.Distance(player.position, baseWolf.transform.position);
            Vector2 displace = (wolfToPlayer) * (adjust * 1.2f) * Time.deltaTime;
            baseWolf.transform.position += new Vector3(displace.x, displace.y);
            Debug.LogWarning("Trying to avoid");
            //(baseWolf.pack.circleDistance / Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, this.baseWolf.transform.position)) 
            //* 
            //baseWolf.destinationSetter.enabled = false;
        }
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
