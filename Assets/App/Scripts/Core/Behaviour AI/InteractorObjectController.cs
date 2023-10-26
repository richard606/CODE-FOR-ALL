using CodeForAll.APP.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractorObjectController : Singleton<InteractorObjectController>
{
    [SerializeField] List<InteractionObject> interactionObjects;


    public override void Awake()
    {
        //Override just work in this scene

        interactionObjects = FindObjectsOfType<InteractionObject>().ToList();
    }
    public InteractionObject GetInteractionObject(InteractionObjectType interactionObjectType) 
    {
        InteractionObject interactionObject = interactionObjects.Find(x => x.interactionObjectType == interactionObjectType);
        return interactionObject;
    }
}
