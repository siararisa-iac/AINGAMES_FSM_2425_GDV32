using UnityEngine;

public class SimpleFSM : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotationSpeed;

    [Header("AI Variables")]
    [Tooltip("How close to the player is considered a chasing range.")]
    [SerializeField]
    private float chaseDistance;
    [Tooltip("How close to the target waypoint before moving to the next.")]
    [SerializeField]
    private float waypointDistance;
    [Tooltip("Points that the tank will randomly move towards during patrol state")]
    [SerializeField]
    private Transform[] waypoints;
    [Tooltip("Reference to the player tank")]
    [SerializeField]
    private Transform player;

    private State currentState;
    private Transform currentTarget;
    private float distanceToPlayer;

    private void Start()
    {
        currentState = State.Patrol;
        RandomizeWaypointTarget();
    }

    private void Update()
    {
        TrackDistanceFromPlayer();
        switch (currentState)
        {
            case State.Patrol:
                DoPatrol();
                break;
            case State.Chase:
                DoChase();
                break;
            case State.Attack:
                DoAttack();
                break;
        }
    }

    private void SetCurrentTarget(Transform target)
    {
        currentTarget = target;
    }

    private void TrackDistanceFromPlayer()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
    }

    private void RandomizeWaypointTarget()
    {
        //Randomize a value from the array
        // Random.Range when int, max is exclusive so we can directly use length of array
        int randomIndex = Random.Range(0, waypoints.Length);
        SetCurrentTarget(waypoints[randomIndex]);
    }
    private void DoAttack()
    {
       
    }

    private void DoChase()
    {
        if(distanceToPlayer > chaseDistance)
        {
            currentState = State.Patrol;
            RandomizeWaypointTarget();
        }
        else
        {
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        // Get the direction towards the target
        Vector3 targetDirection = currentTarget.position - transform.position;
        // Get the rotation that faces the targetDirection
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        // Do the actual rotation by making the enemy look towards the targetDirection
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        // Once facing the target, move forward
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    private void DoPatrol()
    {
        MoveToTarget();

        float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);
        if(distanceToTarget <= waypointDistance)
        {
            RandomizeWaypointTarget();
        }
        else if(distanceToPlayer <= chaseDistance)
        {
            currentState = State.Chase;
            SetCurrentTarget(player);
        }
    
    }
}
