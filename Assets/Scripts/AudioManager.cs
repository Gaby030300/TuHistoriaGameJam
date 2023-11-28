using System;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioMixerSnapshot snapshotIn;
    public AudioMixerSnapshot snapshotOut;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SetSnapshot(snapshotIn);
    }

    private void Start()
    {
        SetSnapshotOut();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0;
        audioSource.DOFade(0.5f, 1);
    }

    [ContextMenu("IN")]
    [Yarn.Unity.YarnCommand("setsnapshotIN")]
    public void SetSnapshotIn()
    {
        SetSnapshot(snapshotIn);
    }

    [ContextMenu("OUT")]
    public void SetSnapshotOut()
    {
        SetSnapshot(snapshotOut);
    }

    private void SetSnapshot(AudioMixerSnapshot snapshot)
    {
        if (snapshot != null)
        {
            snapshot.TransitionTo(2f);
        }
    }
}
