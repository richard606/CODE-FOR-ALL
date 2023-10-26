using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObject : MonoBehaviour
{
    public Animation animationObject;
    public string doorOpen = "Door_open";
    public string doorClose = "Door_close";  

    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private LayerMask _collisionLayer;
    public Color gizmosColor;
    public bool debug;

    private bool isOpen;



    private void Update()
    {
        
        if (Physics.CheckBox(_boxCollider.bounds.center, _boxCollider.bounds.size / 2, transform.rotation, _collisionLayer) && !isOpen)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        animationObject.Play(doorOpen);
        StartCoroutine(OpenDoorRoutine());
    }

    private IEnumerator OpenDoorRoutine()
    {
        yield return new WaitForSeconds(3);
        animationObject.Play(doorClose);
        isOpen = false;
    }

   
}
