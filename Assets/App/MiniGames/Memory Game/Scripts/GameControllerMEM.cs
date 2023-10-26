using CodeForAll.APP.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace CodeForAll.APP.MiniGames.Memory.Scripts.Managers
{
    public class GameControllerMEM : MonoBehaviour
    {
        [SerializeField]
        private Sprite bgImage;

        public Sprite[] puzzles;

        [SerializeField]private GameResultClearCommonMenuUI m_resultClearCommonMenuUI;


        public List<Sprite> gamePuzzles = new List<Sprite>();

        public List<Button> btns = new List<Button>();

        private bool firstGuess, secondGuess;

        private int countGuesses;
        private int countCorrectGuesses;
        private int gameGuesses;
        public int minGuesses;

        private int firstGuessIndex, secondGuessIndex;

        private string firstGuessPuzzle, secondGuessPuzzle;

        public UnityEvent OnGameFinish;
        
        

        void Awake()
        {
            puzzles = Resources.LoadAll<Sprite>("Sprites/Candy-removebg");
        }

        void Start()
        { 
            GetButtons();
            AddListeners();
            AddGamePuzzles();
            gamePuzzles.Shuffle();
            gameGuesses = gamePuzzles.Count / 2;
        }

        void GetButtons()
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

            for (int i = 0; i < objects.Length; i++)
            {
                btns.Add(objects[i].GetComponent<Button>());
                btns[i].image.sprite = bgImage;
            }
        }

        void AddGamePuzzles()
        {
            int looper = btns.Count;
            int index = 0;
            for (int i = 0; i < looper; i++)
            {

                if (index == (looper / 2))
                {
                    index = 0;
                }
                gamePuzzles.Add(puzzles[index]);
                print(index);
                index++;
            }
        }


        void AddListeners()
        {
            foreach (Button btn in btns)
            {
                btn.onClick.AddListener(() => PickAPuzzle());
            }
        }

        public void PickAPuzzle()
        {
            string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

            if (!firstGuess)
            {
                firstGuess = true;

                firstGuessIndex = int.Parse(name);

                firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;

                btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];

                btns[firstGuessIndex].interactable = false;

            }
            else if (!secondGuess)
            {
                secondGuess = true;

                secondGuessIndex = int.Parse(name);

                secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

                btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

                btns[secondGuessIndex].interactable = false;

                countGuesses++;

                StartCoroutine(CheckIfThePuzzlesMatch());
            }


        }

        IEnumerator CheckIfThePuzzlesMatch()
        {
            yield return new WaitForSeconds(0.5f);

            if (firstGuessPuzzle == secondGuessPuzzle)
            {
                yield return new WaitForSeconds(0.5f);

                btns[firstGuessIndex].interactable = false;
                btns[secondGuessIndex].interactable = false;

                btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
                btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

                CheckIfTheGameIsFinished();
            }
            else
            {
                yield return new WaitForSeconds(.5f);

                btns[firstGuessIndex].image.sprite = bgImage;
                btns[secondGuessIndex].image.sprite = bgImage;

                btns[firstGuessIndex].interactable = true;
                btns[secondGuessIndex].interactable = true;
            }

            yield return new WaitForSeconds(.5f);

            firstGuess = secondGuess = false;

        }

        void CheckIfTheGameIsFinished()
        {
            countCorrectGuesses++;

            if (countCorrectGuesses == gameGuesses)
            {
                float percent = ((float)minGuesses / (float)countGuesses) * 100.0f;
                m_resultClearCommonMenuUI.SetMaxPercentageLevelCompleted(100.0f).SetPercentageLevelCompleted(percent);
                ZUIManager.Instance.OpenMenu("Play Result Clear common Menu  UI");

                OnGameFinish?.Invoke();

                Debug.Log("Game Finished");
                Debug.Log("Tomó  " + countGuesses + " intentos terminar el juego!");
            }
        }

        void Shuffle(List<Sprite> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Sprite temp = list[i];
                int randomIndex = Random.Range(i, list.Count);
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }
        void ShowMessageUser()
        {

            //MessagesFinishGame.instance.ShowInformationMessage(1);

        }
    }

}
