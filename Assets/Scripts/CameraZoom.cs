using Cinemachine;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public static CameraZoom _instance;

    public Action OnDestinationReached;
    public Action OnDestinationLeft;

    [Header("Camera System")]
    [SerializeField] private CinemachineVirtualCamera zoomCamera;
    [SerializeField] private CinemachineVirtualCamera firstCamera;

    [Header("NPC Controller")]
    [SerializeField] private NPCController npcController;

    private void Awake()
    {
        _instance = this;        
    }

    void Start()
    {
        OnDestinationReached -= HandleDestinationReached;
        OnDestinationLeft -= HandleDestinationLeft;

        OnDestinationReached += HandleDestinationReached;
        OnDestinationLeft += HandleDestinationLeft;

        zoomCamera.Priority = 9;
        firstCamera.Priority = 10;
    }

    private void Update()
    {
        if (npcController.isReached)
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
        _instance = this;
    }

    private void HandleDestinationLeft()
    {
        if (_instance == this)
        {
            zoomCamera.Priority = 9;
            firstCamera.Priority = 10;
        }
    }
}
