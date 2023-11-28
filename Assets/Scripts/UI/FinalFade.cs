using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Yarn.Unity;

public class FinalFade : MonoBehaviour
{
    public Image fader;
    public GameObject otherCanvas;
    public UIController uiController;
    private DialogueRunner dialogueRunner;

    public static FinalFade Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        uiController = FindObjectOfType<UIController>();
    }

    private void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(uiController.GoBackMenu);
    }

    public void StartEndDialogue()
    {
        fader.DOFade(1, 1).OnComplete(()=>otherCanvas.SetActive(false));
    }
}
