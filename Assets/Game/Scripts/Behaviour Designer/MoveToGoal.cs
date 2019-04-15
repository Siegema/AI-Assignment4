using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[TaskCategory("Custom")]
[TaskDescription("Moves the NavMeshAgent to a destination. Will return Success once it reaches destination. Otherwise it will return Runnin")]
public class MoveToGoal : Action
{
    public float angularDampeningTime = 5.0f;
    public float deadZone = 10.0f;

    protected NavMeshAgent agent;
    protected Animator animator;

    private AnimationListener animationListener;
    private UnityAction onAnimatorMoveCallback;

    public override void OnStart()
    {
        animator = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();

        animationListener = gameObject.GetComponent<AnimationListener>();
        onAnimatorMoveCallback = new UnityAction(OnAnimatorMove);
        animationListener.AddAnimatorMoveListener(onAnimatorMoveCallback);
    }

    private void OnAnimatorMove()
    {
        agent.velocity = animator.deltaPosition / Time.deltaTime;
    }

    public override TaskStatus OnUpdate()
    {
        if (agent.desiredVelocity != Vector3.zero)
        {
            float speed = Vector3.Project(agent.desiredVelocity, transform.forward).magnitude * agent.speed;
            animator.SetFloat("Speed", speed);

            float angle = Vector3.Angle(transform.forward, agent.desiredVelocity);
            if (Mathf.Abs(angle) <= deadZone)
            {
                transform.LookAt(transform.position + agent.desiredVelocity);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation,
                                                     Quaternion.LookRotation(agent.desiredVelocity),
                                                     Time.deltaTime * angularDampeningTime);
            }

            return TaskStatus.Running;
        }

        animator.SetFloat("Speed", 0.0f);
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        animationListener.RemoveAnimatorMoveListener(onAnimatorMoveCallback);
    }
}
