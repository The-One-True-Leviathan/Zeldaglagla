using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShooter_SMBRelocate : StateMachineBehaviour
{
    public BaseShooterBehavior shooter;
    [SerializeField]
    float maxRelocateRate = 0.3f;
    [SerializeField]
    int relocateDice = 2;
    [SerializeField]
    float relocateDiceValue = 1, relocateDiceModifier = 0, relocateLength, deviationAngle, maxAvoidAngle = 45f;
    float timeSinceLastRelocate, maxRelocateTime, relocateTime;
    bool direction;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shooter.baseShooterSMBState = BaseShooterBehavior.BaseShooterSMBState.RELOCATE;
        shooter.destinationSetter.target = null;
        shooter.destinationSetter.enabled = false;
        shooter.pather.maxSpeed = shooter.runSpeed;

        maxRelocateTime = 0;
        for (int i = 0; i < relocateDice; i ++)
        {
            maxRelocateTime += Random.Range(0, relocateDiceValue);
        }
        maxRelocateTime += relocateDiceModifier;

        relocateTime = timeSinceLastRelocate = 0;
        float rng = Random.Range(0f, 1f);
        if (rng < 0.5f)
        {
            direction = true;
        } else
        {
            direction = false;
        }
        Vector3 destination = FleeTarget();
        if (destination == Vector3.zero)
        {
            Debug.Log("No relocation found, going back to attack");
            animator.Play("Attack");
        }
        shooter.pather.destination = FleeTarget();
        shooter.pather.SearchPath();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        relocateTime += Time.deltaTime;
        timeSinceLastRelocate += Time.deltaTime;
        if (timeSinceLastRelocate >= maxRelocateRate)
        {
            Vector3 destination = FleeTarget();
            if (destination == Vector3.zero)
            {
                animator.Play("Wander");
            }
            timeSinceLastRelocate = 0;
            shooter.pather.destination = FleeTarget();
            shooter.pather.SearchPath();
        }
        if (relocateTime >= maxRelocateTime)
        {
            animator.Play("Wander");
        }
    }

    Vector3 FleeTarget()
    {
        Vector3 result = Vector3.zero;
        Vector3 angle = shooter.ToPlayer().normalized;
        Vector3 result1 = FindAngle(angle);
        if (result1 == Vector3.zero)
        {
            direction = !direction;
            Vector3 result2 = FindAngle(angle);
            if (result2!= Vector3.zero)
            {
                return result2;
            }
        } else
        {
            return result1;
        }
        return result;
    }

    Vector3 FindAngle(Vector3 angle)
    {
        if (direction)
        {
            angle = Quaternion.AngleAxis(90, Vector3.forward) * angle;
        }
        else
        {
            angle = Quaternion.AngleAxis(-90, Vector3.forward) * angle;
        }
        Vector3 result = Vector3.zero;
        RaycastHit2D hit2D;
        float hitLenght = relocateLength;
        for (int i = 0; i * deviationAngle < maxAvoidAngle; i++)
        {
            angle = Quaternion.AngleAxis(deviationAngle * i, Vector3.forward) * angle;
            hit2D = Physics2D.Raycast(shooter.transform.position, angle, hitLenght, shooter.blocksLOS);
            if (hit2D.collider)
            {
                angle = Quaternion.AngleAxis(-deviationAngle * i, Vector3.forward) * angle;
                hit2D = Physics2D.Raycast(shooter.transform.position, angle, hitLenght, shooter.blocksLOS);
                if (!hit2D.collider)
                {
                    result = (shooter.transform.position + angle * relocateLength);
                    break;
                }
            }
            else
            {
                result = shooter.transform.position + angle * relocateLength;
                break;
            }

        }

        return result;
    }
}