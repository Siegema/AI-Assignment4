using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[TaskCategory("Custom")]
[TaskDescription("Randomly uses one of many idles added")]
public class RandomIdle : Action 
{
    protected Animator animator;

    protected NavMeshAgent agent;

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
        agent.velocity = Vector3.zero;
    }

    public override TaskStatus OnUpdate()
    { 
        animator.SetTrigger("Idle");
        animator.SetInteger("randInt", Random.Range(0, 2));
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        animationListener.RemoveAnimatorMoveListener(onAnimatorMoveCallback);
    }
}
