using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Custom")]
[TaskDescription("Patrol forever!")]
public class Patrol : MoveToGoal
{
    public SharedGameObjectList WayPoints;
    public SharedInt counter;
    private int index = 0;

    public override void OnStart()
    {
        base.OnStart();

        //agent.SetDestination(WayPoints.Value[index].transform.position);
    }

    public override TaskStatus OnUpdate()
    {
        TaskStatus baseReturn = base.OnUpdate();

        if (baseReturn == TaskStatus.Running)
        {
            return TaskStatus.Running;
        }
        else if (baseReturn == TaskStatus.Success && index < WayPoints.Value.Count)
        {
            if (index < WayPoints.Value.Count)
            {
                agent.SetDestination(WayPoints.Value[index].transform.position);
                index++;
                counter.Value++;
                return TaskStatus.Running;
            }
        }

        return TaskStatus.Success;
    }
}
