using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Custom")]
[TaskDescription("Terrible but removes the first element in a Shared transform list")]
public class RemoveFromSharedList : Action
{
    public SharedTransformList list;

    public override void OnStart()
    { 
        if(list.Value.Count < 0)
        {
            Debug.LogWarning("List is empty");
        }
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        list.Value.RemoveAt(0);
        return TaskStatus.Success;
    }
}
