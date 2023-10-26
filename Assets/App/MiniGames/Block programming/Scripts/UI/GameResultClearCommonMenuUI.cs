
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeForAll.APP.Managers;
using CodeForAll.APP.MiniGames.Shared;

public class GameResultClearCommonMenuUI : MonoBehaviour
{  
    [SerializeField] Slider m_pointsSldr;    
    [SerializeField] TextMeshProUGUI m_sliderTxt;

    [SerializeField] Button m_buttonNextLvl;
    [SerializeField] Button m_buttonGoHome;
    [SerializeField] UIElement m_starImg1;
    [SerializeField] UIElement m_starImg2;
    [SerializeField] UIElement m_starImg3;
    [SerializeField] GameObject m_fireworkPrefab;

    //******* Private Fields **********

    float percentComplete = 0;
    float maxPercentComplete = 100.0f;    

    private void OnEnable()
    {
        m_pointsSldr.maxValue = maxPercentComplete;
        StartCoroutine(SliderMoveSmoothToValue(percentComplete, 40.0f));
    }

    private void OnDisable()
    {
        m_pointsSldr.value = 0.0f;
        m_fireworkPrefab.GetComponent<ParticleSystem>().Stop();
        m_starImg1.ChangeVisibilityImmediate(false);
        m_starImg2.ChangeVisibilityImmediate(false);
        m_starImg3.ChangeVisibilityImmediate(false);
        StopAllCoroutines();
    }

    private BaseMiniGameBehaviour gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<BaseMiniGameBehaviour>();
        if (gameManager == null)
        {
            Debug.Log("GameManager Not Found");
            return;
        }

        gameManager.onGameFinish.AddListener(OnGameFinish);

    }

    private void OnGameFinish()
    {
        float percent = ((float)gameManager.CurrentScore / (float)gameManager.MaxScoreRequired) * 100.0f;
        SetMaxPercentageLevelCompleted(100.0f).SetPercentageLevelCompleted(percent);
    }

    private void Start()
    {
        //TODO al ser independiente debe ser llamado por cualquier GameManager que lo necesite
        //GameManager.OnLevelFinish += OnLevelFinish;
        m_buttonNextLvl.onClick.AddListener(() => AppManager.Instance.LoadNextStageScene());
        m_buttonGoHome.onClick.AddListener(() => AppManager.Instance.LoadScene("Main Menu Scene"));
        
    }

    public GameResultClearCommonMenuUI SetPercentageLevelCompleted(float percentComplete)
    {   if (percentComplete > maxPercentComplete) 
            percentComplete = maxPercentComplete;
    
        this.percentComplete = (int)percentComplete;
        return this;       
        
    }

    public GameResultClearCommonMenuUI SetMaxPercentageLevelCompleted(float maxPercentComplete)
    {
        this.maxPercentComplete = maxPercentComplete;
        return this;      

    }
    public GameResultClearCommonMenuUI Show()
    {
        gameObject.SetActive(true);
        return this;
    }

    
    private IEnumerator SliderMoveSmoothToValue(float toValue, float convergenceSpeed)
    {
        m_pointsSldr.value = 0;
        yield return new WaitForSeconds(2.0f);
        float smoothValue = 0.0f;
        while (smoothValue < toValue)
        {
            if (smoothValue > (toValue - 0.8f))
            {
                break;
            }
            smoothValue = Mathf.MoveTowards(m_pointsSldr.value, percentComplete, Time.deltaTime * 40.0f);
            m_sliderTxt.text = $"{(int)smoothValue} <#9aa5d1>/ {m_pointsSldr.maxValue}";
            m_pointsSldr.value = smoothValue;
            if (smoothValue <= 45 && !m_starImg1.Visible)
                m_starImg1.ChangeVisibility(true);
            if (smoothValue > 45 && !m_starImg2.Visible)
                m_starImg2.ChangeVisibility(true);
            if (smoothValue > 85 && !m_starImg3.Visible)
                m_starImg3.ChangeVisibility(true);
            

            yield return null;
        }

        m_pointsSldr.value = toValue;
        m_sliderTxt.text = $"{(int)toValue} <#9aa5d1>/ {m_pointsSldr.maxValue}";
        StartCoroutine(FireworkParticles());
    }

    IEnumerator FireworkParticles()
    {
        GameObject firework = Instantiate(m_fireworkPrefab, UnityEngine.Random.insideUnitCircle * 1, Quaternion.identity) as GameObject;
        GameObject firework1 = Instantiate(m_fireworkPrefab, UnityEngine.Random.insideUnitCircle * 1, Quaternion.identity) as GameObject;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.5f);
            firework.transform.position = UnityEngine.Random.insideUnitCircle * 2;
            firework.GetComponent<ParticleSystem>().Stop();
            firework.GetComponent<ParticleSystem>().Play();
            firework.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.5f);
            firework1.transform.position = UnityEngine.Random.insideUnitCircle * 2;
            firework1.GetComponent<ParticleSystem>().Stop();
            firework1.GetComponent<ParticleSystem>().Play();
            firework1.GetComponent<AudioSource>().Play();


        }
        Destroy(firework, 1);
        Destroy(firework1, 1);
    }

    private void OnDestroy()
    {
        //GameManager.OnLevelFinish -= OnLevelFinish;
    }

}
