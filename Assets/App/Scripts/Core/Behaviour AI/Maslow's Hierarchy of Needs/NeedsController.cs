using MoreMountains.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsController : MonoBehaviour
{
    [SerializeField]
    private List<Need> _lstNeedsData = new List<Need>();

    public List<Need> LstNeedsData 
    { 
        get => _lstNeedsData; 
        set => _lstNeedsData = value; 
    }


    private void Update()
    {
        //foreach (Need need in _lstNeedsData)
        //{
        //    need.CurrentNeedLevel = Mathf.Lerp(need.CurrentNeedLevel, need.TargetNeed, Time.deltaTime * need.needModifier);

        //    if (need.CurrentNeedLevel < need.percentageOnHighNeedLevel && need.alarmOnHighLevel && !need.isNeedAlarm)
        //    {
        //        need.OnNeedAlarm.Invoke(need);
        //        need.isNeedAlarm = true;
        //    }
        //    if (need.CurrentNeedLevel < need.percentageOnMediumNeedLevel && need.alarmOnMediumLevel && !need.isNeedAlarm)
        //    {
        //        need.OnNeedAlarm?.Invoke(need);
        //        need.isNeedAlarm = true;
        //    }

        //    if (need.CurrentNeedLevel < need.percentageOnLowNeedLevel && need.alarmOnLowLevel && !need.isNeedAlarm)
        //    {
        //        need.OnNeedAlarm?.Invoke(need);
        //        need.isNeedAlarm = true;
        //    }
        //}
    }

}

