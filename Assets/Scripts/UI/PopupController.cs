using System;
using UnityEngine;
using DG.Tweening;
using Yarn.Unity;

public class PopupController : MonoBehaviour
{
    [Tooltip("Reference to popup UI element")]
    public RectTransform popup;
    [Tooltip("Reference to final destination for the popup")]
    public Vector3 targetPosition;
    [Tooltip("Reference to hide the popup")]
    public Vector3 offScreenPosition;
    [Tooltip("Duration for the popup animation")]
    public float duration = 1.5f; // 
    [Tooltip("Time the popup stays on the screen before leaving")]
    public float delay = 1.0f; // 

    [SerializeField] private Ease easeIn;
    [SerializeField] private Ease easeOut;

    private SliderController sliderController;
    private SocialBatteryManager socialBatteryManager;
    private void Awake()
    {
        DialogueRunner dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler("popup",CallPopup);

        socialBatteryManager = FindObjectOfType<SocialBatteryManager>();
        sliderController = GetComponent<SliderController>();
    }

    void Start()
    {
        float popupSize = popup.rect.height / 2;
        targetPosition = new Vector3(0, Screen.height / 2 - popupSize, 0);
        offScreenPosition=new Vector3(Screen.width, Screen.height/2 - popupSize, 0);
        popup.anchoredPosition3D = offScreenPosition;

        //AnimatePopup();
    }

    public void CallPopup()
    {
        float sb = socialBatteryManager.GetSocialBattery();
        sliderController.OnSliderValueChanged(sb);
        AnimatePopup();
    }
    
    [ContextMenu("popup")]
    public void AnimatePopup()
    {
        popup.DOAnchorPos3D(targetPosition, duration)
            .SetEase(easeOut)
            .OnComplete(() => HidePopup());
    }

    
    void HidePopup()
    {
        popup.DOAnchorPos3D(offScreenPosition, duration)
            .SetEase(easeIn)
            .SetDelay(delay);
    }
}