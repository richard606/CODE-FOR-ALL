using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour, ISupplyNeedObject
{
    public Transform playerTMove;
    public Transform playerTInteraction;    
    public AnimationClip animationClip;
    public InteractionObjectType interactionObjectType;
    
    public void EnterInteraction(AIController controller)
    {
        AnimatorOverrideController animatorOverride = new AnimatorOverrideController(controller.AnimatorRuntimeController);
        controller.Animator.runtimeAnimatorController = animatorOverride;
        animatorOverride["Interact"] = animationClip;
    }

    public void ExitInteraction(AIController controller)
    {
        controller.Animator.runtimeAnimatorController = controller.AnimatorRuntimeController;        
    }  

    public Vector3 GetPositionPlayerMove()
    {
        return playerTMove.position;
    }

    public Quaternion GetRotationPlayerInteraction()
    {
        return playerTMove.localRotation;
    }

    
}
