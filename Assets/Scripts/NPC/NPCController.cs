using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using Unity.VisualScripting;
using Cinemachine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private GameObject alertIcon;

    [Header("AI System")]
    [SerializeField] private AIPath aiPath;

    [Header("Camera System")]
    [SerializeField] private CinemachineVirtualCamera zoomCamera;
    [SerializeField] private CinemachineVirtualCamera firstCamera;

    public Action OnDestinationReached;
    public Action OnDestinationLeft;

    void Start()
    {
        alertIcon.SetActive(false);
        OnDestinationReached += HandleDestinationReached;
        OnDestinationLeft += HandleDestinationLeft;

        zoomCamera.enabled = false;
        firstCamera.enabled = true;
    }

    private void Update()
    {
        if (aiPath.reachedDestination)
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
        zoomCamera.enabled = true;
        firstCamera.enabled = false;
    }    
    
    private void HandleDestinationLeft()
    {
        zoomCamera.enabled = false;
        firstCamera.enabled = true;
    }
}
