using CodeForAll.APP.Managers;
using CodeForAll.APP.MiniGames.BlocksProgramming.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeForAll.APP.MiniGames.BuildingBlocks.Scripts.Models
{
    public class GameStartModel : MonoBehaviour
    {
        public CommandManager commandManager;

        private void Awake()
        {
            commandManager = FindObjectOfType<CommandManager>();
        }

       
        public void GoMenu()
        {
            AppManager.Instance.LoadScene("Main Menu Scene",false);
        }

        public void RewindCommand() 
        {
            commandManager.Rewind();
        }


    }
}
