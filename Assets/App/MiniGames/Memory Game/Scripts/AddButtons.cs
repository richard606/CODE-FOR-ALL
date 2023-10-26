using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddButtons : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject btn;

    [SerializeField]
    int cantCartas = 0;

    void Awake()
    {

        if (SceneManager.GetActiveScene().name == "Etp1_MemoryLvl1")
        {
            cantCartas = 4;
        }
        else
       if (SceneManager.GetActiveScene().name == "Etp1_MemoryLvl2")
        {
            cantCartas = 6;
        }
        else
       if (SceneManager.GetActiveScene().name == "Etp1_MemoryLvl3")
        {
            cantCartas = 12;
        }
        for (int i = 0; i < cantCartas; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(puzzleField,false);
        }

    }
        
}
