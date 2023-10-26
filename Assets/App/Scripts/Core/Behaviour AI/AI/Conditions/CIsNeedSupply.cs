using AI.BT;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CIsNeedSupply : Behavior
{
    private AIController controller;
    public CIsNeedSupply(AIController controller)
    {
        this.controller = controller;
    }


    public override Status Update()
    {
        
        if (controller.NeedsToSupplies.Count > 0)         
        {  
            return Status.Success;
        }       

        return Status.Running;
    }
   

}
