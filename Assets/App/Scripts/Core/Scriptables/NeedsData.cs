using MoreMountains.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NeedsData",menuName = "CodeForAll/NeedsData")]
public class NeedsData : ScriptableObject
{
    public InteractionObjectType interactionObjectType;

    public Sprite interactionObjectNullSprt;

    public float maxNeedRange = 100.0f;

    public float minNeedRange = 0.0f;  
    
    public NeedPriority needPriority = NeedPriority.None;

    private float _currentNeedLevel = 0.0f;
    public float CurrentNeedLevel
    {
        get => _currentNeedLevel;
    }
    private float _targetNeed = 0.0f;

    [Range(0.001f, 0.05f)] public float _needModifier = 0.01f;

    private float _defaultneedModifier = 0.01f;

    public bool alarmOnHighLevel;
    [MMCondition("alarmOnHighLevel", false)]
    [Range(0.0f, 100.0f)] public float percentageOnHighNeedLevel = 100.0f;

    public bool alarmOnMediumLevel = true;
    [MMCondition("alarmOnMediumLevel", false)]
    [Range(0.0f, 100.0f)] public float percentageOnMediumNeedLevel = 70.0f;

    public bool alarmOnLowLevel = true;
    [MMCondition("alarmOnLowLevel", false)]
    [Range(0.0f, 100.0f)] public float percentageOnLowNeedLevel = 40.0f;

    public Action<NeedsData> OnNeedAlarm;
    public bool isNeedAlarm;

    public void InitializeNeed()
    {
        _currentNeedLevel = maxNeedRange;
        _targetNeed = minNeedRange;
        _defaultneedModifier = _needModifier;
    }
    public void UpdateNeed()
    {
        _currentNeedLevel = Mathf.Lerp(_currentNeedLevel, _targetNeed, Time.deltaTime * _needModifier);

        if (_currentNeedLevel < percentageOnHighNeedLevel && alarmOnHighLevel && !isNeedAlarm)
        {
            OnNeedAlarm?.Invoke(this);
            isNeedAlarm = true;
        }
        if (_currentNeedLevel < percentageOnMediumNeedLevel && alarmOnMediumLevel && !isNeedAlarm)
        {
            OnNeedAlarm?.Invoke(this);
            isNeedAlarm = true;
        }

        if (_currentNeedLevel < percentageOnLowNeedLevel && alarmOnLowLevel && !isNeedAlarm)
        {
            OnNeedAlarm?.Invoke(this);
            isNeedAlarm = true;
        }


    }

    public void EnterSupplyNeed(float targetNeed, float needModifier = 0.01f)
    {
        _targetNeed = targetNeed;
        _needModifier = needModifier;
    }
    public void EnterSupplyNeed(float targetNeed)
    {
        _currentNeedLevel = targetNeed;
    }

    public void ExitSupplyNeed()
    {
        _targetNeed = minNeedRange;
        _needModifier = _defaultneedModifier;
    }


    public virtual bool GetNeedMustBeSupply()
    {
        float levelPercentage = _currentNeedLevel / 100;

        return levelPercentage < percentageOnMediumNeedLevel || levelPercentage < percentageOnLowNeedLevel;

    }
}
