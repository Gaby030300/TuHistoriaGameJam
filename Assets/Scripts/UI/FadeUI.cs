using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private Image fader;
    [SerializeField] private float time;
    private void Start()
    {
        fader.DOFade(0, time).OnComplete(() => 
            fader.DOFade(1,time+1).OnComplete(ChangeScene)
            );
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
