using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavController : MonoBehaviour
{
    [SerializeField] NavMeshAgent _navMeshAgent;

    [SerializeField] Camera _camera;
    Ray _ray;
    RaycastHit _raycastHit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {


            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _raycastHit, Mathf.Infinity)) 
            {
                _navMeshAgent.destination = _raycastHit.point;
            }
        }
    }
}
