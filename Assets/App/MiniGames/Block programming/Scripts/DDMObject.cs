using MoreMountains.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectSettings))]
public class DDMObject : MonoBehaviour
{
    [Header("------------Effects--------------")]
    public bool returnToParentTransform;
    [MMCondition("returnToParentTransform", true)]
    public Transform parentT;
   

    private ObjectSettings objectSettings;
    private void Awake()
    {
        objectSettings = GetComponent<ObjectSettings>();
    }

    private void Start()
    {
        objectSettings.OnDragDropFailed.AddListener(OnDragDropFailed);
    }

    private void OnDragDropFailed()
    {

        StartCoroutine("OnDragDropFailedRoutine");
        
    }

    public IEnumerator OnDragDropFailedRoutine()     
    {
        yield return new WaitForSeconds(0.1f);

        if (!returnToParentTransform) yield return null;

        objectSettings.transform.SetParent(parentT);

    }
}
