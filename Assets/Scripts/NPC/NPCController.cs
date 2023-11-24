using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using Unity.VisualScripting;
using Cinemachine;
using Yarn.Unity;
using Yarn.Unity.Example;

public class NPCController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float detectionDistance;

    [Header("AI System")]
    [SerializeField] private AIPath aiPath;

    [Header("Dialogue System")] [SerializeField]
    private CharacterInteraction characterInteraction;
    private YarnCharacter character;

    [HideInInspector] public bool isReached;

    private void Start()
    {
        characterInteraction = FindObjectOfType<CharacterInteraction>();
        character = GetComponent<YarnCharacter>();
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
        Debug.Log("Se acerca camara y hace alguna funci�n especifica");
        characterInteraction.LaunchDialogue(character.firstDialogue);
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
