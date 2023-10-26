using CodeForAll.APP.MiniGames.BlocksProgramming.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(ObjectSettings))]
public class MoveCommandObject : MonoBehaviour
{
    public enum MoveCommandType 
    { Up,Down,Left,Right }

    public MoveCommandType commandType;

    private ObjectSettings m_ObjectSettings;

    private PlayerController playerController;

    private DragDropManager DDM;    

    private void Awake()
    {
        m_ObjectSettings = GetComponent<ObjectSettings>();
        playerController = FindObjectOfType<PlayerController>();
        DDM = GameObject.Find("DDM").GetComponent<DragDropManager>();
          
    }
    void Start()
    {
        m_ObjectSettings.OnObjectSwiched.AddListener(() => ChangeParentT());        
    }

    private void ChangeParentT()
    {
       
        //Invoke("ChangeParent",1f);
        
    }

    

    public void ChangeParent()
    { Debug.Log("Swiched objet ajajajaja");
        GetComponent<RectTransform>().SetParent(DDM.FirstCanvas.GetComponent<RectTransform>());
    }

    public void AddCommand() {

        switch (commandType)
        { 
            case MoveCommandType.Up:
                playerController.AddCommand(Vector2.up);
                break;
            case MoveCommandType.Down:
                playerController.AddCommand(Vector2.down);
                break;
            case MoveCommandType.Right:
                playerController.AddCommand(Vector2.right);
                break;
            case MoveCommandType.Left:
                playerController.AddCommand(Vector2.left);
                break;
        }
        
    }
    public void AddCommand(int index)
    {

        switch (commandType)
        {
            case MoveCommandType.Up:
                playerController.AddCommand(Vector2.up, index);
                break;
            case MoveCommandType.Down:
                playerController.AddCommand(Vector2.down, index);
                break;
            case MoveCommandType.Right:
                playerController.AddCommand(Vector2.right, index);
                break;
            case MoveCommandType.Left:
                playerController.AddCommand(Vector2.left, index);
                break;
        }

    }

    public void RemoveCommand(int index)
    {

        playerController.RemoveCommand(index);
        

    }


}
