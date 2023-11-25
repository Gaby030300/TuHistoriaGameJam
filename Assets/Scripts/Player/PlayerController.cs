using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Configurations")]
    [SerializeField] private float speedMovement;
    private float currentSpeed;

    [Header("Pathfinding")]
    [SerializeField] private Seeker seeker;
    private Path path;
    private int currentWaypoint = 0;

    private Vector2 targetPosition;

    private bool isMoving;
    private bool isDialogueCompleted;

    public Action OnDialogStart;
    public Action OnDialogComplete;

    private void Awake()
    {
        currentSpeed = speedMovement;
    }

    private void Update()
    {
        HandleInput();
        MoveTowardsTarget();

        OnDialogStart += HandleDialogStart;
        OnDialogComplete += HandleDialogComplete;       
    }


    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving && isDialogueCompleted)
        {
            SetTargetPosition();
            isMoving = true;
            FindPath();
        }
    }

    private void FindPath()
    {
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        path = p;
        currentWaypoint = 0;
    }

    private void SetTargetPosition()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void MoveTowardsTarget()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            isMoving = false;
            return;
        }

        float step = speedMovement * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, path.vectorPath[currentWaypoint], step);

        if (Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]) < 0.2f)
        {
            currentWaypoint++;
        }
    }

    public void HandleDialogComplete()
    {
        speedMovement = currentSpeed;
        isDialogueCompleted = true;
    }

    public void HandleDialogStart()
    {
        speedMovement = 0;
    }

}
