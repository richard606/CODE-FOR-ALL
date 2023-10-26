using CodeForAll.APP.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeForAll.APP.Managers
{
    public class AppManager : Singleton<AppManager>
    {
        [HideInInspector]
        public AsyncOperation asyncLoad;
        [HideInInspector]
        public bool isSceneLoaded;

        public List<SceneContainers> stageScenes;
        public List<SceneContainers> miniGamesScenesProgramingBlocks;
        public List<SceneContainers> miniGamesScenesMemory;
        public List<SceneContainers> miniGamesScenesBuildingBlocks;
        public List<SceneContainers> miniGamesScenesSuperMarket;
        private List<SceneContainers> currentScenes;
        public static int currentStage = 0;

        public void LoadScene(string sceneName,bool isLoadMenuActive = false) 
        {
            StartCoroutine(LoadAsyncSceneRoutine(sceneName, isLoadMenuActive));
        }
                
        IEnumerator LoadAsyncSceneRoutine(string sceneName, bool isLoadMenuActive)
        {
            if (isLoadMenuActive) 
            {
                yield return new WaitForSeconds(3);
            }

            asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            asyncLoad.allowSceneActivation = false;
            //wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                //scene has loaded as much as possible,
                // the last 10% can't be multi-threaded
                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                }
                yield return null;
            }
            isSceneLoaded = asyncLoad.isDone;           

        }

        public void LoadNextStageScene()
        {
            //TODO Cargar la siguiente scena en modo Round Robin o En Random
            string sceneName = SceneManager.GetActiveScene().name;
            List<DevLocker.Utils.SceneReference> list = currentScenes[currentStage].scenes.ToList();

            int sceneIndex = list.FindIndex(x => x.SceneName.Equals(sceneName));

            if (currentScenes[currentStage].IsLastSceneStage(sceneIndex)) 
            {
                if (!IsLastStage(currentStage)) 
                {
                    currentStage++;
                    sceneIndex = - 1;
                }
                else 
                {
                    LoadScene("Main Menu Scene");
                    currentStage = 0;
                    return;
                }
                    
               
            }
            int nextSceneStageIndex = sceneIndex + 1;
            LoadScene(currentScenes[currentStage].scenes[nextSceneStageIndex].SceneName,false);
        }

        private bool IsLastStage(int currentStage)
        {
            return currentStage + 1 == currentScenes.Count;
        }

        public void ChangeCurrentStageScenes(int index)
        {
            
            switch (index)
            {
                case 0:
                    currentScenes = stageScenes;
                    break;
                case 1:
                    currentScenes = miniGamesScenesBuildingBlocks;
                    break;
                case 2:
                    currentScenes = miniGamesScenesMemory;
                    break;
                case 3:
                    currentScenes = miniGamesScenesProgramingBlocks;
                    break;
                case 4:
                    currentScenes = miniGamesScenesSuperMarket;
                    break;

                default:
                    break;
            }

            currentStage = 0;

        }


    }


}
