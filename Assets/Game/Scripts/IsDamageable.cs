using System.Collections;
using BehaviorDesigner.Runtime;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BehaviorTree))]
public class IsDamageable : MonoBehaviour
{
    private BehaviorTree behaviorTree;
    private SharedFloat hp;

    void Start()
    {
        behaviorTree = GetComponent<BehaviorTree>();
        hp = behaviorTree.GetVariable("HP") as SharedFloat;
    }

    public void Damage(float dmg)
    {
        hp.Value -= dmg;
    }
}
