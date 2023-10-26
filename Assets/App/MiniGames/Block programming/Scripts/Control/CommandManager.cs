using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace CodeForAll.APP.MiniGames.BlocksProgramming.Managers
{
    public class CommandManager : MonoBehaviour
    {
        public List<ICommand> _commandBuffer = new List<ICommand>();

        private static CommandManager _instance;
        public static CommandManager Instance
        {
            get 
            {
                if (_instance == null)
                {
                    Debug.LogError("Command Manager is Null");
                }

                return _instance;
            }
        }

        public int totalCommandsExecuted;

        public event Action OnStartCommands;
        public event Action OnFinishCommands;
        public event Action OnStartRewind;
        public event Action OnFinishRewind;

        private void Awake()
        {
            _instance = this;
        }
        

        public void AddCommand(ICommand command) 
        {
            _commandBuffer.Add(command);
        }
        public void AddCommand(ICommand command,int indexPos)
        {
            print("CCommand addesd in  index pos : " + indexPos);
            _commandBuffer[indexPos] = command;
        }

        public void Play()
        {
            if (GameManager.Instance.curGameState == GameState.Playing || GameManager.Instance.curGameState == GameState.Returning) return;
            StartCoroutine(PlayRoutine());
        }

        IEnumerator PlayRoutine() 
        {
            Debug.Log("Playig...");
            OnStartCommands?.Invoke();

            foreach (ICommand command in _commandBuffer)
            {
                if (!command.ExecuteBool()) continue;
                totalCommandsExecuted += 1;
                yield return new WaitForSeconds(0.5f);
            }

            OnFinishCommands?.Invoke();
            Debug.Log("Finished...");
        }

        public void Rewind()
        {
            if (GameManager.Instance.curGameState == GameState.Playing) return;

            StartCoroutine(RewindRoutine());
        }

        IEnumerator RewindRoutine()
        {
            OnStartRewind?.Invoke();

            foreach (ICommand command in Enumerable.Reverse(_commandBuffer))
            {
                if (!command.UndueBool()) continue;
                totalCommandsExecuted -= 1;
                yield return new WaitForSeconds(0.07f);
            }

            OnFinishRewind?.Invoke();
            
        }

        public void ResetCommands()
        {
            _commandBuffer.Clear();
        }

        public void RemoveCommand(int index)
        {
            Debug.Log("Command removed");
            _commandBuffer.RemoveAt(index);
            _commandBuffer[index] = new MoveCommand(null,Vector3.zero);
        }
        

    }


    public class MoveCommand : ICommand
    {
        public PlayerController controller;
        public Vector3 _prevPosition;
        public Vector3 _moveToPosition;
        public int _commandID;

        public MoveCommand(PlayerController controller, Vector3 moveToPosition)
        {
            this.controller = controller;
            _moveToPosition = moveToPosition;
            _commandID = CommandManager.Instance._commandBuffer.Count;
        }
        public void Execute()
        {            
            if (!controller.IsValidMovement(_moveToPosition)) return;

            _prevPosition = controller.transform.position;           
            
            Vector3 destPos = GameManager.Instance.GetGrid().GetXY(controller.transform.position + (_moveToPosition));
            destPos.x += 0.5f;
            destPos.y += 0.5f;
            
            controller.transform.position = destPos;

            Debug.Log("Se movio.." + destPos);
        }

        public bool ExecuteBool()
        {
            
            if (controller == null) return false;
            _prevPosition = controller.transform.position;

            if (!controller.IsValidMovement(_moveToPosition)) return true;
            if (_moveToPosition == Vector3.zero) return false;

            

            Vector3 destPos = GameManager.Instance.GetGrid().GetXY(controller.transform.position + (_moveToPosition));
            destPos.x += 0.5f;
            destPos.y += 0.5f;

            controller.transform.position = destPos;

            return true;
        }

        public void Undue()
        {
            controller.transform.position = _prevPosition;
        }

        public bool UndueBool()
        {
            if (_moveToPosition == Vector3.zero) return false;

            controller.transform.position = _prevPosition;

            return true;
        }
    }

}
