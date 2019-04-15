using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationListener : MonoBehaviour, IAnimationCompleted
{
    #region Animation Move Event
    private UnityEvent onAnimatorMoveEvent = new UnityEvent();

    public void AddAnimatorMoveListener(UnityAction callback)
    {
        onAnimatorMoveEvent.AddListener(callback);
    }
    public void RemoveAnimatorMoveListener(UnityAction callback)
    {
        onAnimatorMoveEvent.RemoveListener(callback);
    }

    private void OnAnimatorMove()
    {
        onAnimatorMoveEvent.Invoke();
    }
    #endregion

    #region Animation Completed Event

    [System.Serializable]
    public class AnimationCompletedEvent : UnityEvent<int> { }

    private Dictionary<int, AnimationCompletedEvent> animationCompletedEvents = new Dictionary<int, AnimationCompletedEvent>();

    // Add Listener for Animation Completed
    public void AddAnimationCompletedListener(int shortHashName, UnityAction<int> callback)
    {
        AnimationCompletedEvent eventCallback;
        if (animationCompletedEvents.TryGetValue(shortHashName, out eventCallback))
        {
            eventCallback.AddListener(callback);
        }
        else
        {
            eventCallback = new AnimationCompletedEvent();
            eventCallback.AddListener(callback);
            animationCompletedEvents.Add(shortHashName, eventCallback);
        }
    }

    // Remove Listener for Animation Completed
    public void RemoveAnimationCompletedListener(int shortHashName, UnityAction<int> callback)
    {
        AnimationCompletedEvent eventCallback;
        if (animationCompletedEvents.TryGetValue(shortHashName, out eventCallback))
        {
            eventCallback.RemoveListener(callback);
        }
    }

    public void AnimationCompleted(int shortHashName)
    {
        AnimationCompletedEvent eventCallback;
        if (animationCompletedEvents.TryGetValue(shortHashName, out eventCallback))
        {
            eventCallback.Invoke(shortHashName);
        }
    }
    #endregion
}
