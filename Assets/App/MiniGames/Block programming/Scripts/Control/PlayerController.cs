using CodeForAll.APP.MiniGames.BlocksProgramming.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]float rayDistance = 1f;
    [SerializeField]LayerMask collisionLayer;
    public bool isDebug = false;
    private void Start()
    {
        for (int i = 0; i < FindObjectOfType<DragDropManager>().AllPanels.Length; i++)
        {            
            CommandManager.Instance._commandBuffer.Add(new MoveCommand(this, Vector2.zero));
        }
    }
    public void AddCommand(Vector2 direction) 
    {
        CommandManager.Instance.AddCommand(new MoveCommand(this, direction));
    }

    public void AddCommand(Vector2 direction, int index)
    {
        CommandManager.Instance.AddCommand(new MoveCommand(this, direction), index);
    }

    public void RemoveCommand(int index)
    {
        CommandManager.Instance.RemoveCommand(index);
    }

    public bool IsValidMovement(Vector2 direction)
    {
        RaycastHit2D col = Physics2D.Raycast(transform.position,direction, rayDistance, collisionLayer);        
        return !col;
    }

    private void OnDrawGizmos()
    {
        if (isDebug)
        {             
            Gizmos.DrawRay(new Ray(transform.position,Vector2.up * rayDistance)); 
            Gizmos.DrawRay(new Ray(transform.position,Vector2.down * rayDistance)); 
            Gizmos.DrawRay(new Ray(transform.position,Vector2.left * rayDistance)); 
            Gizmos.DrawRay(new Ray(transform.position,Vector2.right * rayDistance)); 
        }
    }
}
