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

    [Header("Player")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float detectionDistance;

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

        DistanceToFollowPlayer();
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

    private void DistanceToFollowPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < detectionDistance)
        {
            aiPath.enabled = true;
        }
        else
        {
            aiPath.enabled = false;
        }
    }
}
