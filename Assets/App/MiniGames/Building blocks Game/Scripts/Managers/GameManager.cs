using CodeForAll.APP.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CodeForAll.APP.MiniGames.BuildingBlocks.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] List<ObjectSettings> objectSettings = new List<ObjectSettings>();
        
        public static event Action<int> onLevelFinish;

        [SerializeField] private GameResultClearCommonMenuUI m_resultClearCommonMenuUI;

        int currentCoins;
        [SerializeField] private int minGuesses;
        private int countGuesses;      

      
        private void Start()
        {
            foreach (ObjectSettings item in objectSettings)
            {
                item?.OnDroppedSuccessfully.AddListener(() => OnDroppedSuccessfully());
            }
        }

        private void OnDestroy()
        {
            foreach (ObjectSettings item in objectSettings)
            {
                item?.OnDroppedSuccessfully.RemoveAllListeners();
            }
        }

        public void OnDroppedSuccessfully() 
        {
            currentCoins += 1;
            countGuesses++;           

            if (currentCoins >= objectSettings.Count)
            {
                float percent = ((float)minGuesses / (float)countGuesses) * 100.0f;
                m_resultClearCommonMenuUI.SetMaxPercentageLevelCompleted(100.0f).SetPercentageLevelCompleted(percent);
                ZUIManager.Instance.OpenMenu("Play Result Clear common Menu  UI");
                onLevelFinish?.Invoke(0);                
                
            }
        }
    }

}
