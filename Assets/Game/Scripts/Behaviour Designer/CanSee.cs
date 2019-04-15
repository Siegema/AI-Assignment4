using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Custom")]
[TaskDescription("Returns success if we can see the game object")]
public class CanSee : Conditional
{
    public SharedGameObject Target;
    public float distance = 10.0f;
    public float ViewAngle = 35.0f;

    public override TaskStatus OnUpdate()
    {
        Vector3 distanceVector = Target.Value.transform.position - transform.position;
        Vector3 toTarget = distanceVector.normalized;
        float angle = Vector3.Angle(transform.forward, toTarget);
        if (Mathf.Abs(angle) <= ViewAngle && distanceVector.magnitude <= distance)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }

    public override void OnDrawGizmos()
    {
#if UNITY_EDITOR
        float halfViewAngle = ViewAngle * 0.5f;
        Vector3 targetDirection = Quaternion.AngleAxis(-halfViewAngle, Owner.transform.up) * Owner.transform.forward;
        UnityEditor.Handles.DrawSolidArc(Owner.transform.position, Owner.transform.up, targetDirection, ViewAngle, distance);

#endif
    }
}
