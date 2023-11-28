using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;

    [SerializeField] private ScrollButton topButton;
    [SerializeField] private ScrollButton bottomButton;

    [SerializeField] private float scrollSpeed = 0.01f;
    
    void Update()
    {
        if(bottomButton != null)
        {
            if(bottomButton.isDown)
            {
                ScrollDown();
            }
        }
        if (topButton != null)
        {
            if (topButton.isDown)
            {
                ScrollUp();
            }
            
        }
    }

    private void ScrollDown()
    {
        if (scrollRect != null)
        {
            if (scrollRect.verticalNormalizedPosition >= 0)
            {
                scrollRect.verticalNormalizedPosition -= scrollSpeed;
            }
        }
    }

    private void ScrollUp()
    {
        if (scrollRect != null)
        {
            if (scrollRect.verticalNormalizedPosition <= 1)
            {
                scrollRect.verticalNormalizedPosition += scrollSpeed;
            }
        }
    }
}
