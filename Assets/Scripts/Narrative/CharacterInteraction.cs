using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class CharacterInteraction : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private string dialogueNode = "OtherDialogue";
    
    private void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    [ContextMenu("Launch Dialogue")]
    public void LaunchDialogue(string dialogue)
    {
        dialogueRunner.StartDialogue(dialogue);
    }
}
