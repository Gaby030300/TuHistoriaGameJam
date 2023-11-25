using Pathfinding;
using System;
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

    [Header("Animator")]
    [SerializeField] private Animator animator;


    private Vector2 targetPosition;

    private bool isMoving;
    private bool isDialogueCompleted;
    private bool isFacingRight;

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
            animator.SetBool("isWalking", isMoving);
            return;
        }

        isMoving = true;

        CharacterDirection();

        float step = speedMovement * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, path.vectorPath[currentWaypoint], step);

        if (Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]) < 0.2f)
        {
            currentWaypoint++;
        }
        animator.SetBool("isWalking", isMoving);

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

    private void CharacterDirection()
    {
        Vector2 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        if (direction.x < 0 && !isFacingRight)
        {
            Flip();
        }
        else if (direction.x > 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }

}
