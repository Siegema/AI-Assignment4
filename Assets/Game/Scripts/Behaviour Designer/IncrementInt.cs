using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Custom")]
[TaskDescription("Increment scalars by a given value")]
public class IncrementInt : Action
{
    public SharedInt intVariable;
    public int intAmount;

    public override void OnStart()
    { 
        if(intVariable == null)
        { 
            Debug.LogError("No Integer is set to be incremeneted");
        }
    }


    // Update is called once per frame
    public override TaskStatus OnUpdate()
    { 
        intVariable.Value += intAmount;

        return TaskStatus.Success;
    }
}
