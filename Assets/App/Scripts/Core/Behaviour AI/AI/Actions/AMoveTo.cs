using AI.BT;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AMoveTo : Behavior
{
    private AIController _controller;

    private Vector3 _destPos;

    InteractionObject supplyNeedObject;
    NeedsData currentNeedSupliying;
    public AMoveTo(AIController controller)
    {
        _controller = controller;
    }

    public override Status Update()
    {

        currentNeedSupliying = _controller.NeedsToSupplies.First();
        supplyNeedObject = InteractorObjectController.Instance.GetInteractionObject(_controller.NeedsToSupplies.First().interactionObjectType);
        if (supplyNeedObject == null)
        {
            Utilities.wrErr($" SupplyObjectNotFound for '{_controller.NeedsToSupplies.First().name}' Need ");
            _controller.PlayerHUD.ShowEmptyNeedSprt(currentNeedSupliying.interactionObjectNullSprt);
            _controller.NeedsToSupplies.Remove(currentNeedSupliying);
            return Status.Invalid;
        }
        else
        {
            _destPos = supplyNeedObject.GetPositionPlayerMove();
            _controller.Agent.SetDestination(_destPos);
        }

        if (_controller.Agent.pathPending) return Status.Running;


        Utilities.wr($"Going to {supplyNeedObject.name} for '{_controller.NeedsToSupplies.First().interactionObjectType}' Need ");
        
        float dist = _controller.Agent.remainingDistance;
        
        if (dist != _controller.Agent.stoppingDistance)
        {
            _controller.Animator.Play(Utilities.NAME_STATE_ANIM_WALK);
            return Status.Running;
        }        


        return Status.Success;
        
    }
    
}
