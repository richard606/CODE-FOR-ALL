using CodeForAll.APP.MiniGames.Shared;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeForAll.APP.MiniGames.SuperMarket.Scripts.Managers
{
    public class GameManager : BaseMiniGameBehaviour
    {
        DragDropManager DDM;

        [SerializeField]MMF_Player feedBack;

        private void Awake()
        {
            DDM = GameObject.Find("DDM").GetComponent<DragDropManager>();
        }
        protected override void OnGameStart()
        {
            base.OnGameStart();

            MaxScoreRequired = DDM.AllObjects.Length;
            
            foreach (ObjectSettings objectSettings in DDM.AllObjects)
            {
                objectSettings.OnDroppedSuccessfully.AddListener(() => 
                {
                    base.CurrentScore++;                   
                });
            }

            feedBack.Events.OnComplete.AddListener(() => base.CurGameState = GameState.GAMEFINISH);
        }       

        protected override void OnGameFinish()
        {
            base.OnGameFinish();
            ZUIManager.Instance.OpenMenu("Play Result Clear common Menu  UI");
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        }
    }
}
