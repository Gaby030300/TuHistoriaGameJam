using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedMovement;

    private Vector2 targetPosition;
    private bool isMoving;

    private void Update()
    {
        HandleInput();
        MoveTowardsTarget();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetTargetPosition();
            isMoving = true;
        }
    }

    private void SetTargetPosition()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void MoveTowardsTarget()
    {
        if (isMoving && (Vector2) transform.position != targetPosition)
        {
            float step = Time.deltaTime * speedMovement;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        }
        else
        {
            isMoving = false;
        }
    }

}
