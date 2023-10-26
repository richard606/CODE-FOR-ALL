using AI.BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASelectActions : Behavior
{
    private AIController controller;

    // --------------------------------------------------
    public ASelectActions(AIController controller)
    {
        this.controller = controller;
    }

    // --------------------------------------------------
    public override void OnInitialize()
    {
        
    }

    // --------------------------------------------------
    public override Status Update()
    {
       

        return Status.Running;
    }
}
