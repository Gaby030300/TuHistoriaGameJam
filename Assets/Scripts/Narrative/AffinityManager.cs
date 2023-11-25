using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class AffinityManager : MonoBehaviour
{
    private Dictionary<string, float> affinityEntries = new Dictionary<string, float>();
    private InMemoryVariableStorage variableStorage;

    private void Awake()
    {
        DialogueRunner dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler<string,float>("affinity",NewAffinityEntry);
    }

    private void Start()
    {
        variableStorage = FindObjectOfType<InMemoryVariableStorage>();
    }


    public void NewAffinityEntry(string name, float value)
    {
        affinityEntries.Add(name,value);
        Debug.Log($"Name: {name} value: {value}");
    }
    
}
