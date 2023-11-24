using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SliderController : MonoBehaviour
{
    public RectTransform sliderImage;
    public RectTransform sliderContainer;
    [SerializeField] private float time = 2;
    [SerializeField] private Ease ease;
    public void MoveSlider(float percentage)
    {
        float containerWidth = sliderContainer.rect.width;
        float imageWidth = sliderImage.rect.width/2;
        float maxX = containerWidth/2 - imageWidth;

        float targetX = Mathf.Lerp(-containerWidth/2 + imageWidth, maxX, percentage);
        Vector2 targetPosition = new Vector2(targetX, sliderImage.anchoredPosition.y);

        sliderImage.DOAnchorPos(targetPosition, time).SetEase(ease);
    }

    [ContextMenu("Move Slider to 0%")]
    public void MoveSliderToLeftBoundary()
    {
        MoveSlider(0f);
    }

    [ContextMenu("Move Slider to 100%")]
    public void MoveSliderToRightBoundary()
    {
        MoveSlider(1f);
    }

   public void OnSliderValueChanged(float value)
    {
        float normalizedValue = Mathf.Clamp01(value / 100f);
        MoveSlider(normalizedValue);
    }
}