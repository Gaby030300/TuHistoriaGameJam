using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private void Awake()
    {
        if (Instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged+=GetDialogueRunner;
    }

    public void GoToNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    void GetDialogueRunner(Scene current, Scene next)
    {
        DialogueRunner dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner?.AddCommandHandler("sceneChange",GoToNextScene);
    }
}
