using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
public class SocialBatteryManager : MonoBehaviour
{
    [SerializeField] private float socialBattery;
    
    private InMemoryVariableStorage variableStorage;

    private void Start()
    {
        variableStorage = FindObjectOfType<InMemoryVariableStorage>();
    }

    [ContextMenu("Get SB")]
    public float GetSocialBattery()
    {
        if(socialBattery>100) SetSocialBattery(100);
        if(socialBattery<0) SetSocialBattery(0);
        variableStorage.TryGetValue("$social_battery", out socialBattery);
        return socialBattery;
    }
    
    
    public void SetSocialBattery(float value)
    {
        variableStorage.SetValue("$social_battery",value);
    }

}
