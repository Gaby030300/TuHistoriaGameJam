using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class CharacterInteraction : MonoBehaviour
{
    private DialogueRunner dialogueRunner;

    private void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    [ContextMenu("Launch Dialogue")]
    public void LaunchDialogue()
    {
        dialogueRunner.StartDialogue("OtherDialogue");
    }
}
