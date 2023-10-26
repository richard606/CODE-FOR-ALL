using CodeForAll.APP.MiniGames.BuildingBlocks.Scripts.Managers;
using CodeForAll.APP.MiniGames.BuildingBlocks.Scripts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CodeForAll.APP.MiniGames.BlocksProgramming.Scripts.UI
{
    public class GameStartMenuUI : MonoBehaviour
    {
        [SerializeField] GameStartModel gameStartModel;
        [SerializeField] Button m_homeBtn;
        [SerializeField] Button m_startBtn;
        [SerializeField] Button m_deleteAllBtn;
        [SerializeField] TextMeshProUGUI m_coinsTxt;

        public Sprite startSprt;
        public Sprite startSprtBtnBackground;
        public Sprite stopSprt;
        public Sprite stopSprtBtnBackground;      
        
        

        private void OnFinishCommands()
        {
            m_deleteAllBtn.interactable = true;
            m_startBtn.transform.GetChild(0).GetComponent<Image>().sprite = startSprt;
            m_startBtn.GetComponent<Image>().sprite = startSprtBtnBackground;

        }

        private void OnStartCommands()
        {
            m_deleteAllBtn.interactable = false;
            m_startBtn.transform.GetChild(0).GetComponent<Image>().sprite = stopSprt;
            m_startBtn.GetComponent<Image>().sprite = stopSprtBtnBackground;
        }

       


        private void OnAddCoins(int coins)
        {
            m_coinsTxt.text = coins.ToString();
        }

        private void Start()
        {
           m_homeBtn.onClick.AddListener(() => gameStartModel?.GoMenu());
           //m_deleteAllBtn.onClick.AddListener(() => gameStartModel.RewindCommand());
           gameStartModel.commandManager.OnStartCommands += OnStartCommands;
           gameStartModel.commandManager.OnFinishCommands += OnFinishCommands;
        }

        private void OnDestroy()
        {
            gameStartModel.commandManager.OnStartCommands -= OnStartCommands;
            gameStartModel.commandManager.OnFinishCommands -= OnFinishCommands;
        }
    }

}
