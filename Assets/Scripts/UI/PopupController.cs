using UnityEngine;
using DG.Tweening;

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

    void Start()
    {
        targetPosition = new Vector3(0, Screen.height / 2, 0);
        offScreenPosition=new Vector3(Screen.width, Screen.height/2, 0);
        popup.anchoredPosition3D = offScreenPosition;

        AnimatePopup();
    }

    
    void AnimatePopup()
    {
        popup.DOAnchorPos3D(targetPosition, duration)
            .SetEase(Ease.OutExpo)
            .OnComplete(() => HidePopup());
    }

    
    void HidePopup()
    {
        popup.DOAnchorPos3D(offScreenPosition, duration)
            .SetEase(Ease.InExpo)
            .SetDelay(delay);
    }
}