using AI.BT;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ASupplyNeed : Behavior
{

    private AIController controller;

    float timeToSuply = 10;
    float currentTimeToSuply = 0;

    InteractionObject supplyNeedObject;
    NeedsData currentNeedSupliying;
    public ASupplyNeed(AIController controller)
    {
        this.controller = controller;
    }

    public override void OnInitialize()
    {
        currentTimeToSuply = 0;
        currentNeedSupliying = controller.NeedsToSupplies.First();
    }


    public override Status Update()
    {
        supplyNeedObject = InteractorObjectController.Instance.GetInteractionObject(controller.NeedsToSupplies.First().interactionObjectType);

        if (currentNeedSupliying != controller.NeedsToSupplies.First())
        {
            ExitNeed();
            return Status.Success;
        }

        currentTimeToSuply += Time.deltaTime;

       
        if (currentTimeToSuply <= timeToSuply)
        {
            supplyNeedObject.EnterInteraction(controller);           
            controller.playerRoot.position = supplyNeedObject.playerTInteraction.position;
            controller.playerRoot.rotation = supplyNeedObject.playerTInteraction.rotation;                 
            controller.Animator.Play(Utilities.NAME_STATE_ANIM_INTERACT);
            controller.IsWorkingOnNeed = true;
            return Status.Running;

        }

        ExitNeed();
        currentNeedSupliying.EnterSupplyNeed(100);         

        return Status.Success;

    }

    private void ExitNeed()
    {
        currentTimeToSuply = 0;
        controller.playerRoot.localPosition = Vector3.zero;
        controller.playerRoot.localRotation = Quaternion.identity;
        currentNeedSupliying.isNeedAlarm = false;
        controller.NeedsToSupplies.Remove(currentNeedSupliying);
        controller.IsWorkingOnNeed = false;
        controller.Animator.Play(Utilities.NAME_STATE_ANIM_SADIDLE);
        supplyNeedObject?.ExitInteraction(controller);
    }
}
