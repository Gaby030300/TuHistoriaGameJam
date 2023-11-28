using UnityEngine;
using Yarn.Unity;

public class SocialBatteryChecker : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    private InMemoryVariableStorage variableStorage;
    private void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();

        if (dialogueRunner != null)
        {
            dialogueRunner.onDialogueComplete.AddListener(CheckSocialBattery);
        }
        else
        {
            Debug.LogError("DialogueRunner not found!");
        }
        variableStorage = FindObjectOfType<InMemoryVariableStorage>();
    }

    private void CheckSocialBattery()
    {
        float socialBattery;
        variableStorage.TryGetValue("$social_battery", out socialBattery);

        if (socialBattery <= 0)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        SceneController.Instance.GoToNextScene();
    }
}