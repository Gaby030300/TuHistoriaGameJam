using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Yarn.Unity;

public class ContractButtonHandler : MonoBehaviour
{
    public Button contractButton;
    public Button backButton;
    public Contract contract;

    private void Start()
    {
        contractButton =  gameObject.AddComponent<Button>();
        contractButton.onClick.AddListener(OnButtonClicked);
        //Hope I get forgiveness for what I made, but I'm stupid and I forgot this case
        backButton = GameObject.Find("BackButton").GetComponent<Button>();
        backButton.onClick.AddListener(() =>
            {
                FinalFade.Instance.StartEndDialogue();
                FindObjectOfType<DialogueRunner>().StartDialogue("NoJob"); 
            }
        );
    }

    private void OnButtonClicked()
    {
        FinalFade.Instance.StartEndDialogue();
        Contract.ContractType type = GetComponent<ContractUI>().contractGot.Type;
        switch (type)
        {
            case Contract.ContractType.Bootcamp:
                FindObjectOfType<DialogueRunner>().StartDialogue("bootcamp");
                break;
            case Contract.ContractType.BadJob:
                FindObjectOfType<DialogueRunner>().StartDialogue("BadJob");
                break;
            case Contract.ContractType.MidJob:
                FindObjectOfType<DialogueRunner>().StartDialogue("MidJob");
                break;
            case Contract.ContractType.GoodJob:
                FindObjectOfType<DialogueRunner>().StartDialogue("GoodJob");
                break;
        }
    }

}