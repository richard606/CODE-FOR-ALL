using CodeForAll.APP.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeForAll.APP.MiniGames.BuildingBlocks.Scripts.UI
{
    public class GameStartMenuUI : MonoBehaviour
    {        
        [SerializeField] Button m_exitButton;

        private void Start()
        {           
            m_exitButton.onClick.AddListener(() => AppManager.Instance.LoadScene("Main Menu Scene"));
          
        }

       
    }

}
