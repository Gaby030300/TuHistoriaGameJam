using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedMovement;

    private Vector2 lastClickPosition;
    private bool isMoving;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            lastClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;
        }
        if(isMoving && (Vector2)transform.position != lastClickPosition) 
        {
            float step  = Time.deltaTime * speedMovement;
            transform.position = Vector2.MoveTowards(transform.position, lastClickPosition, step);
        }
        else
        {
            isMoving = false;
        }
    }

}
