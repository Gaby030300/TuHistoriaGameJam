using System;
using UnityEngine;
using DG.Tweening;
using Yarn.Unity;

public class PopupController : MonoBehaviour
{
    [Tooltip("Reference to popup UI element")]
    public RectTransform popup;
    [Tooltip("Duration for the popup animation")]
    public float duration = 1.5f;
    [Tooltip("Time the popup stays on the screen before leaving")]
    public float delay = 1.0f;
    [SerializeField] private Ease easeIn;
    [SerializeField] private Ease easeOut;

    private Vector3 targetPosition;
    private Vector3 offScreenPosition;

    private SliderController sliderController;
    private SocialBatteryManager socialBatteryManager;

    private void Awake()
    {
        DialogueRunner dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler("popup", CallPopup);

        socialBatteryManager = FindObjectOfType<SocialBatteryManager>();
        sliderController = GetComponent<SliderController>();
    }

    void Start()
    {
        CalculatePopupPositions();
        popup.anchoredPosition3D = offScreenPosition;
    }

    void CalculatePopupPositions()
    {
        float popupHeight = popup.rect.height / 2;
        float popupWidth = popup.rect.width / 2;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Calculate target and off-screen positions based on screen aspect ratio
        float aspectRatio = screenWidth / screenHeight;

        float targetX = 0;
        float targetY = (screenHeight / 2) - popupHeight*4;

        float offScreenX = screenWidth;
        float offScreenY = (screenHeight / 2) - popupHeight*4;

        targetPosition = new Vector3(targetX, targetY, 0);
        offScreenPosition = new Vector3(offScreenX, offScreenY, 0);
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
