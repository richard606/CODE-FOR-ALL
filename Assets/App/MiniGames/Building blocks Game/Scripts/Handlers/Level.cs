using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CodeForAll.APP.MiniGames.BuildingBlocks.Scripts 
{
    [CreateAssetMenu(fileName = "Level", menuName = "CodeForAll/Add level Blocks ", order = 1)]
    public class Level : ScriptableObject
    {
        public int moves = 20;
        public Vector2Int size = Vector2Int.one * 6; 
    }
}

