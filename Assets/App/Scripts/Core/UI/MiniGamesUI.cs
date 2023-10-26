using CodeForAll.APP.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeForAll.APP.UI 
{
    public class MiniGamesUI : MonoBehaviour
    {
        public Button miniGameBuildingBlocks;
        public Button miniGameMemory;
        public Button miniGameBlocksProgramming;
        public Button miniGameSuperMarket;
        void Start()
        {
            miniGameBlocksProgramming.onClick.AddListener(() => AppManager.Instance.ChangeCurrentStageScenes(3));
            miniGameMemory.onClick.AddListener(() => AppManager.Instance.ChangeCurrentStageScenes(2));
            miniGameBuildingBlocks.onClick.AddListener(() => AppManager.Instance.ChangeCurrentStageScenes(1));
            miniGameSuperMarket.onClick.AddListener(() => AppManager.Instance.ChangeCurrentStageScenes(4));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
