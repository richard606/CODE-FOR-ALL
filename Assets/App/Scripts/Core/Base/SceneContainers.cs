using DevLocker.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New SceneContainer", menuName = "CodeForAll/SceneContainer",order = 2)]
public class SceneContainers : ScriptableObject
{
    public SceneReference[] scenes;

    public bool IsLastSceneStage(int currentSceneIndex) 
    {
        return currentSceneIndex + 1 == scenes.Length;
    }   

}
