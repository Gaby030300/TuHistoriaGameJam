using Cinemachine;
using Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Action OnDestinationReached;
    public Action OnDestinationLeft;

    [Header("Camera System")]
    [SerializeField] private CinemachineVirtualCamera zoomCamera;
    [SerializeField] private CinemachineVirtualCamera firstCamera;

    public static CameraManager Instance { get; private set; }
    private List<NPCController> npcControllers = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        OnDestinationReached += HandleDestinationReached;
        OnDestinationLeft += HandleDestinationLeft;

        GetAllNPCs();

        zoomCamera.Priority = 9;
        firstCamera.Priority = 10;
    }

    private void Update()
    {
        if (npcControllers.Any(npc => npc.isReached))
        {
            OnDestinationReached?.Invoke();
        }
        else
        {
            OnDestinationLeft?.Invoke();
        }
    }

    private void HandleDestinationReached()
    {
        zoomCamera.Priority = 10;
        firstCamera.Priority = 9;
        Instance = this;
    }

    private void HandleDestinationLeft()
    {
        if (Instance == this)
        {
            zoomCamera.Priority = 9;
            firstCamera.Priority = 10;
        }
    }

    private void GetAllNPCs()
    {
        NPCController[] npcs = FindObjectsOfType<NPCController>();
        npcControllers.AddRange(npcs);
    }
}
