using UnityEngine;
using AI.BT;

public class CIsTargetVisible : Behavior
{
    private AIController controller;

    public CIsTargetVisible(AIController controller)
    {
        this.controller = controller;
    }

    public override Status Update()
    {        
            return Status.Failure;        
    }
}
