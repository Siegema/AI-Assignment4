using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

[RequireComponent(typeof(BehaviorTree))]
public class ClickToInvestigate : MonoBehaviour
{
    [SerializeField]
    private int limit;

    private BehaviorTree behaviorTree;
    private SharedBool mouseBtnClicked;
    private SharedVector3 destination;
    private SharedTransformList areas;

    void Start()
    {
        behaviorTree = GetComponent<BehaviorTree>();
        mouseBtnClicked = behaviorTree.GetVariable("mouseBtnClicked") as SharedBool;
        destination = behaviorTree.GetVariable("destination") as SharedVector3;
        areas = behaviorTree.GetVariable("noiseyAreas") as SharedTransformList;
    }

    void Update()
    {
        if (areas.Value.Count < limit)
        {
            mouseBtnClicked.Value = false;
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    mouseBtnClicked.Value = true;
                    destination.Value = hit.point;
                    areas.Value.Add(hit.transform);
                }
            }
        }
    }
}
