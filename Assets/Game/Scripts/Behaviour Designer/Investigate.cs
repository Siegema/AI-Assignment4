using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Custom")]
[TaskDescription("Investigate nearest noisey area")]
public class Investigate : MoveToGoal
{
    public SharedTransformList Areas;

    public override void OnStart()
    {
        base.OnStart();
        agent.SetDestination(Areas.Value[0].position);
    }

    public override TaskStatus OnUpdate()
    {
        TaskStatus baseReturn = base.OnUpdate();

        if (baseReturn == TaskStatus.Running)
        {
            return TaskStatus.Running;
        }
        else if (baseReturn == TaskStatus.Success && Areas.Value.Count > 0)
        {
            Areas.Value.Sort(
                (a, b)
                => Vector3.Distance(a.position, transform.position).CompareTo(Vector3.Distance(b.position, transform.position))
            );

            agent.SetDestination(Areas.Value[0].position);

            Areas.Value.RemoveAt(0);

            return TaskStatus.Success;
        }

        return TaskStatus.Success;
    }
}
