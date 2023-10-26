using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public GameObject needEmptyGO;
    public float hideAfterNeedEmptyTime;
    public Queue<Sprite> spritesToShow = new Queue<Sprite>();
    private Image icon;


    private void Start()
    {
        needEmptyGO.SetActive(false);
    }
    public void ShowEmptyNeedSprt(Sprite needSprt)
    {
        icon = needEmptyGO.transform.Find("Icon").GetComponent<Image>();
        spritesToShow.Enqueue(needSprt);

        if (icon != null) 
        { 
            icon.sprite = needSprt;
            StartCoroutine(ShowEmptyNeedSprtRoutine());
        }
    }

    private IEnumerator ShowEmptyNeedSprtRoutine()
    {
        while (spritesToShow.Count > 0)
        {
            icon.sprite = spritesToShow.Dequeue();
            needEmptyGO.SetActive(true);
            yield return new WaitForSeconds(hideAfterNeedEmptyTime);
            needEmptyGO.SetActive(false);
        }

    }

    
}
