using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CodeForAll.APP.MiniGames.BlocksProgramming.Managers
{
    public enum GameState 
    { 
        None, 
        CommandCreation, 
        Playing, 
        Returning, 
        Stopped, 
        Unfinished, 
        Done 
    }
    public class GameManager : MonoBehaviour
    {
        private Core.Grid grid;

        [Header("----------- References -------------")]

        [SerializeField] private Transform goalT;
        [SerializeField] private PlayerController player;
        [SerializeField] private CommandManager commandManager;
        [SerializeField] private GameObject m_lockingCnv;
        [SerializeField] private GameResultClearCommonMenuUI m_resultClearCommonMenuUI;

        [Header("----------- Game Configuration -------------")]     
                
        [SerializeField] private Vector3 goalPosition;
        [SerializeField] int columns = 8;
        [SerializeField] int rows = 8;
        [SerializeField] float cellSize = 0.5f;        
        [SerializeField] int minMovements = 1;
        
        [Header("----------- Game State -------------")]
        public GameState curGameState = GameState.None;


       
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("Game Manager is Null");
                }

                return _instance;
            }
        }    
        public static event Action<float> OnLevelFinish;



        private void Awake()
        {
            _instance = this;
        }

        public Core.Grid GetGrid() 
        { 
            return grid;
        }
        private void Start()
        {
            curGameState = GameState.CommandCreation;

            grid = new Core.Grid(columns, rows, cellSize, new Vector3 (-columns * cellSize / 2, -0.5f, 0));
            commandManager.OnStartCommands += OnStarCommands;
            commandManager.OnFinishCommands += OnFinishCommands;
            commandManager.OnStartRewind += OnStartRewind;
            commandManager.OnFinishRewind += OnFinishRewind;            
            Vector3 gridGoalPos = grid.GetXY(goalT.position);
            gridGoalPos.x += 0.5f;
            gridGoalPos.y += 0.5f;
            goalPosition = gridGoalPos;

            m_lockingCnv.SetActive(false);

        }

        private void OnFinishRewind()
        {
            m_lockingCnv.SetActive(false);
            curGameState = GameState.CommandCreation;
            
        }

        private void OnStartRewind()
        {
            curGameState = GameState.Returning;
        }

        private void OnFinishCommands()
        {
            curGameState = GameState.Stopped;

            if (!IsPlayerOnGoal()) 
            {
                commandManager.Rewind();
                return;
            }
            
            curGameState = GameState.Done;


            float percent = ((float)minMovements / (float)commandManager.totalCommandsExecuted) * 100.0f;
            m_resultClearCommonMenuUI.SetMaxPercentageLevelCompleted(100.0f).SetPercentageLevelCompleted(percent);
            ZUIManager.Instance.OpenMenu("Play Result Clear common Menu  UI");

            OnLevelFinish?.Invoke(percent);

            m_lockingCnv.SetActive(false);

        }

        public bool IsPlayerOnGoal() {
            return player.transform.position == goalPosition;
        }

        private void OnStarCommands()
        {
            m_lockingCnv.SetActive(true);
            curGameState = GameState.Playing;
        }

        private void OnDestroy()
        {
            commandManager.OnStartCommands -= OnStarCommands;
            commandManager.OnFinishCommands -= OnFinishCommands;
            commandManager.OnStartRewind -= OnStartRewind;
            commandManager.OnFinishRewind -= OnFinishRewind;
        }
    }
}
