using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private Image fader;
    [SerializeField] private float time;
    [SerializeField] private bool isChanging;
    [SerializeField] private bool isFadingOut;
    private void Start()
    {
        fader.DOFade(0, time).OnComplete(() =>
            {
                if (isFadingOut)
                {
                    fader.DOFade(1, time + 1).OnComplete(() =>
                        {
                            if (isChanging)
                            {
                                SceneController.Instance.GoToNextScene();
                            }
                        }
                    );
                }
            }
            );
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}
