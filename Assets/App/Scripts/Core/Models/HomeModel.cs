using CodeForAll.APP.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeModel : MonoBehaviour
{
    public void Play()
    {
        AppManager.Instance.LoadScene("Building blocks Scene 1", true);
        AppManager.Instance.ChangeCurrentStageScenes(0);
    }

    public void Play(string nameScene)
    {
        AppManager.Instance.LoadScene(nameScene, true);
    }
}
