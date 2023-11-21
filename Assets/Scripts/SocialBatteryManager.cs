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
    public void GetSocialBattery()
    {
        variableStorage.TryGetValue("$social_battery", out socialBattery);
        Debug.Log(socialBattery);
    }
    
    
    public void SetSocialBattery(float value)
    {
        variableStorage.SetValue("$social_battery",value);
    }

}
