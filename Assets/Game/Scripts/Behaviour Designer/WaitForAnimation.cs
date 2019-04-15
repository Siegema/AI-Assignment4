using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[TaskCategory("Custom")]
[TaskDescription("Waits for an animation to finish")]
public class WaitForAnimation : Action
{
    public SharedString animationName;

    private AnimationListener animationListener;
    private UnityAction<int> onAnimationCompletedCallback;

    private bool animationCompleted;
    private int ANIM_name;

    public override void OnStart()
    {
        ANIM_name = Animator.StringToHash(animationName.Value);
        animationCompleted = false;

        animationListener = gameObject.GetComponent<AnimationListener>();
        onAnimationCompletedCallback = new UnityAction<int>(OnAnimationCompleted);
        animationListener.AddAnimationCompletedListener(ANIM_name, onAnimationCompletedCallback);
    }

    private void OnAnimationCompleted(int hashCode)
    {
        if (ANIM_name == hashCode)
        {
            animationCompleted = true;
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (animationCompleted == true)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        animationListener.RemoveAnimationCompletedListener(ANIM_name, onAnimationCompletedCallback);
    }
}
