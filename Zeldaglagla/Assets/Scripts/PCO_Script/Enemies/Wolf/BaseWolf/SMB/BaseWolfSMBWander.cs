using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseWolfSMBWander : StateMachineBehaviour
{
    public BaseWolf baseWolf;
    public bool isAlpha = false;
    public float wanderSize;
    public Transform wanderCenter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseWolf = animator.GetComponent<BaseWolf>();
        baseWolf.pack = animator.transform.parent.GetComponent<PackManager>();
        baseWolf.destinationSetter.enabled = true;
        baseWolf.baseWolfSMBState = BaseWolf.BaseWolfSMBState.WANDER;
        baseWolf.pather.maxSpeed = baseWolf.wanderSpeed;
        if (baseWolf.isPackLeader)
        {
            Repath();
        } else
        {
            if (baseWolf.pack.leader != null)
            {
                baseWolf.destinationSetter.target = baseWolf.pack.leader.transform;
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(baseWolf.pather.velocity.magnitude > 0.05)
        {
            baseWolf.SetAnim("Walk", baseWolf.pather.velocity);
        } else
        {
            baseWolf.SetAnim("Idle", Vector3.down);
        }

        if (baseWolf.isPackLeader)
        {
            if (baseWolf.pather.remainingDistance < 2)
            {
                Repath();
            }
        } 
        else
        {
            bool tooclose = false;
            
            foreach (WolfRoot other in baseWolf.pack.wolves)
            {
                if (other != this.baseWolf)
                {
                    if (Vector2.Distance(other.transform.position, baseWolf.transform.position) < 1)
                    {
                        baseWolf.transform.position += (this.baseWolf.transform.position - other.transform.position) * 1/ Vector2.Distance(other.transform.position, this.baseWolf.transform.position) * Time.deltaTime;
                        //baseWolf.destinationSetter.enabled = false;
                        tooclose = true;
                    }
                }
            }
            if (!tooclose && !baseWolf.destinationSetter.enabled)
            {
                baseWolf.destinationSetter.enabled = true;
                baseWolf.destinationSetter.target = baseWolf.pack.leader.transform;
            }
        }
        if(baseWolf.ToPlayer().magnitude < baseWolf.viewDistance)
        {
            if (baseWolf.pack.leader.GetComponent<PCO_AlphaWolfBehavior>())
            {
                if (baseWolf.packCircle.pack != null && baseWolf.packCircle.pack != baseWolf.pack)
                {
                    baseWolf.packCircle.pack.AllGoToFlee();
                    baseWolf.pack.AllGoToApproach();
                }
                else
                {
                    baseWolf.pack.AllGoToApproach();
                }
            }
            else
            {
                if (baseWolf.packCircle.pack == null)
                {
                    baseWolf.pack.AllGoToApproach();
                }
                else if (baseWolf.packCircle.pack != baseWolf)
                {
                    baseWolf.pack.AllGoToFlee();
                }
            }
        }
    }

    void Repath()
    {
        float rngx, rngy;
        if (isAlpha)
        {
            rngx = Random.Range(-1, 1);
            rngy = Random.Range(-1, 1);
            Vector3 rngVect = new Vector3(rngx, rngy, 0).normalized;
            rngVect *= Random.Range(wanderSize * 0.2f, wanderSize);

            baseWolf.pather.destination = wanderCenter.position + rngVect;
        }
        else
        {
            rngx = (Random.Range(10f, 20f) + Random.Range(10f, 20f)) / 2;
            if (Random.Range(0f, 1f) < 0.5)
            {
                rngx *= -1;
            }
            rngy = (Random.Range(10f, 20f) + Random.Range(10f, 20f)) / 2;
            if (Random.Range(0f, 1f) < 0.5)
            {
                rngy *= -1;
            }
            baseWolf.pather.destination = new Vector2(baseWolf.transform.position.x + rngx, baseWolf.transform.position.y + rngy);
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
