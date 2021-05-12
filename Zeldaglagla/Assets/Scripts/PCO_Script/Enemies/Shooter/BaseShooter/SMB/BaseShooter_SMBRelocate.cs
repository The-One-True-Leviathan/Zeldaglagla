using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShooter_SMBRelocate : StateMachineBehaviour
{
    public BaseShooterBehavior shooter;
    [SerializeField]
    float maxRelocateRate = 0.3f, maxRelocateTime = 1, relocateLength, deviationAngle, maxAvoidAngle = 45f;
    float timeSinceLastRelocate, relocateTime;
    bool direction;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shooter.baseShooterSMBState = BaseShooterBehavior.BaseShooterSMBState.RELOCATE;
        shooter.destinationSetter.target = null;
        shooter.destinationSetter.enabled = false;
        shooter.pather.maxSpeed = shooter.runSpeed;
        relocateTime = timeSinceLastRelocate = 0;
        float rng = Random.Range(0, 1);
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
        }
        shooter.pather.destination = FleeTarget();
        shooter.pather.SearchPath();
        if (relocateTime >= maxRelocateTime)
        {
            animator.Play("Wander");
        }
    }

    Vector3 FleeTarget()
    {
        Vector3 result = Vector3.zero;
        Vector3 angle = -shooter.ToPlayer().normalized;
        if (FindAngle(angle) == Vector3.zero)
        {
            direction = !direction;
            FindAngle(angle);
        }
        return result;
    }

    Vector3 FindAngle(Vector3 angle)
    {
        if (direction)
        {
            float x = angle.x;
            float y = angle.y;
            angle = new Vector2(-y, x);
        }
        else
        {

            float x = angle.x;
            float y = angle.y;
            angle = new Vector2(y, -x);
        }
        Vector3 result = Vector3.zero;
        RaycastHit2D hit2D;
        float hitLenght = relocateLength;
        for (int i = 0; i * deviationAngle < maxAvoidAngle; i++)
        {
            angle = -shooter.ToPlayer().normalized;
            angle = Quaternion.AngleAxis(deviationAngle * i, Vector3.forward) * angle;
            hit2D = Physics2D.Raycast(shooter.transform.position, angle, hitLenght, shooter.blocksLOS);
            if (hit2D.collider)
            {
                angle = -shooter.ToPlayer().normalized;
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