using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CodeForAll.APP.MiniGames.Shared 
{
    public enum GameState
    {
        GAMESTART, GAMEFINISH
    }
    public class BaseMiniGameBehaviour : MonoBehaviour
    {
        [SerializeField] private int maxScoreRequired;
        

        private GameState curGameState;
        public GameState CurGameState 
        { 
            get => curGameState;
            set 
            {
                if (value == GameState.GAMEFINISH && curGameState != GameState.GAMEFINISH)
                {
                    curGameState = value;
                    OnGameFinish();
                    return;
                }

                curGameState = value;
                
            }
        }

        
        public int MaxScoreRequired 
        {   
            get => maxScoreRequired; 
            set => maxScoreRequired = value;
        }        

        private int currentScore;
        public int CurrentScore
        {
            get
            {
                return currentScore;
            }
            set
            {
                currentScore = value;
                if (currentScore >= maxScoreRequired)
                {
                    currentScore = maxScoreRequired;
                    CurGameState = GameState.GAMEFINISH;
                }

            }
        }

       

        public UnityEvent onGameFinish;
        public UnityEvent onGameStart;



        private void Start()
        {
            OnGameStart();
        }

        private void Update()
        {
            OnUpdate();
        }
        protected virtual void OnGameStart()
        {
            onGameStart?.Invoke();
            curGameState = GameState.GAMESTART;
        }
        protected virtual void OnGameFinish()
        {
            onGameFinish?.Invoke();
        }
        protected virtual void OnUpdate() { }

    }

}
