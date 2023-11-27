using Pathfinding;
using System;
using UnityEngine;
using Yarn;
using Yarn.Unity;
using DG.Tweening;
using static Yarn.Unity.DialogueRunner;

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

    private DialogueRunner dialogueRunner;


    private Vector2 targetPosition;

    public  bool isMoving;
    private bool isDialogueCompleted;
    private bool isFacingRight;

    public Action OnDialogStart;
    public Action OnDialogComplete;

    [Header("On Node Start")]
    [Space]
    public StringUnityEvent onNodeStart;

    [Header("On Node Complete")]
    [Space]
    public StringUnityEvent onNodeComplete;


    private void Awake()
    {
        currentSpeed = speedMovement;
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    private void Update()
    {
        HandleInput();
        MoveTowardsTarget();

        dialogueRunner.onNodeStart = onNodeStart;
        dialogueRunner.onNodeComplete = onNodeComplete;
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving && isDialogueCompleted)
        {
            SetTargetPosition();
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

        CharacterDirection();

        float step = speedMovement * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, path.vectorPath[currentWaypoint], step);

        if (Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]) < 0.2f)
        {
            currentWaypoint++;

            isMoving = true;
            animator.SetBool("isWalking", isMoving);
        }
    }

    public void HandleDialogComplete()
    {   
        speedMovement = currentSpeed;
        isDialogueCompleted = true;
        animator.SetBool("isTalking", isDialogueCompleted);

        isMoving = false;
        animator.SetBool("isWalking", isMoving);

    }

    public void HandleDialogStart()
    {
        speedMovement = 0;
        isDialogueCompleted = false;
        animator.SetBool("isTalking", isDialogueCompleted);

        isMoving = false;
        animator.SetBool("isWalking", isMoving);
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
