using UnityEngine;
using Pathfinding;
using Yarn.Unity.Example;
using DG.Tweening;

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

    [Header("Animator")]
    [SerializeField] private Animator animator;

    [HideInInspector] public bool isReached;

    private bool isTalking;

    [Header("Position")]
    private Vector2 lastPosition;

    private void Start()
    {
        characterInteraction = FindObjectOfType<CharacterInteraction>();
        character = GetComponent<YarnCharacter>();

        lastPosition = (Vector2) transform.position;
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
            else
            {
                transform.DOMove(lastPosition, 2);
                isTalking = false;
            }
        }
    }

    private void HandleDestinationReached()
    {
        Debug.Log("Se acerca camara y hace alguna funcion especifica");
        characterInteraction.LaunchDialogue(character.firstDialogue);
        isTalking = true;
        animator.SetBool("isTalking", isTalking);
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
            isTalking = false;
            animator.SetBool("isTalking", isTalking);
        }
    }
}
