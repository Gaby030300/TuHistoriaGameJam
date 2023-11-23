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
    [Header("Player")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float detectionDistance;

    [Header("AI System")]
    [SerializeField] private AIPath aiPath;

    [Header("Dialogue System")]
    [SerializeField] private DialogueRunner dialogueRunner;

    [Header("Camera Manager")]
    CameraZoom cameraZoom;

    [HideInInspector] public bool isReached;   

    void Start()
    {
        cameraZoom = GetComponentInParent<CameraZoom>();
        UnsubscribeAction();
        SubscribeAction();
    }

    private void Update()
    {
        ActivateActions();
        DistanceToFollowPlayer();
    }

    private void ActivateActions()
    {
        bool destinationReached = aiPath.reachedDestination;

        if (destinationReached != isReached)
        {
            isReached = destinationReached;

            if (isReached)
            {
                HandleDestinationReached();
            }
        }
    }


    private void HandleDestinationReached()
    {
        UnsubscribeAction();
        //Aqui va código cuando player se acerca
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

    private void UnsubscribeAction()
    {
        cameraZoom.OnDestinationReached -= HandleDestinationReached;
    }
    private void SubscribeAction()
    {
        cameraZoom.OnDestinationReached += HandleDestinationReached;
    }
}
