using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using Unity.VisualScripting;
using Cinemachine;
using Yarn.Unity;

public class NPCController : MonoBehaviour
{
    private static NPCController _instance;

    [Header("Player")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float detectionDistance;

    [Header("AI System")]
    [SerializeField] private AIPath aiPath;

    [Header("Camera System")]
    [SerializeField] private CinemachineVirtualCamera zoomCamera;
    [SerializeField] private CinemachineVirtualCamera firstCamera;


    [Header("Dialogue System")]
    [SerializeField] private DialogueRunner dialogueRunner;

    public Action OnDestinationReached;
    public Action OnDestinationLeft;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        OnDestinationReached += HandleDestinationReached;
        OnDestinationLeft += HandleDestinationLeft;

        zoomCamera.Priority = 9;
        firstCamera.Priority = 10;
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
