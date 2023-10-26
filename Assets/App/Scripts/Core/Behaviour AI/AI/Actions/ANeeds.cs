using AI.BT;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ANeeds : Behavior
{
    private AIController controller;
    public ANeeds(AIController controller) 
    { 
        this.controller = controller;
    }
    public override void OnInitialize()
    {
        foreach (Need need in controller.Needs)
        {
            need.NeedsData.InitializeNeed();
            need.NeedsData.OnNeedAlarm += OnNeedAlarm;
        }
       
    }    

    public override Status Update()
    {
        foreach (Need need in controller.Needs) 
        {
            need.NeedsData.UpdateNeed();
        }
        return Status.Running;
    }

    private void OnNeedAlarm(NeedsData need)
    {
        if (controller.NeedsToSupplies.Contains(need)) return;

        controller.NeedsToSupplies.Add(need);
        ReorderNeedsByPriority();
    }

    private void ReorderNeedsByPriority()
    {        
        List<NeedsData> shortedNeeds = controller.NeedsToSupplies.OrderByDescending(x => x.needPriority).ToList();
        controller.NeedsToSupplies.Clear();
        controller.NeedsToSupplies = shortedNeeds;
    }
}
