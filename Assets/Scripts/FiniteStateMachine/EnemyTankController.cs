using UnityEngine;

public class EnemyTankController : AdvancedFiniteStateMachine
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


    public float WaypointDistance => waypointDistance;
    public float ChaseDistance => chaseDistance;

    protected override void Initialize()
    {
        // Build the actual FSM
        PatrolState patrolState = new(this, waypoints);
        // Define the transition possible for patrol state
        patrolState.AddTransition(TransitionID.SawPlayer, StateID.Chase);

        ChaseState chaseState = new();

        AddState(patrolState);
        AddState(chaseState);
    }

    protected override void UpdateFiniteStateMachine()
    {
        CurrentState.Run(this.transform, player);
        CurrentState.CheckTransition(this.transform, player);
    }

    public void MoveToTarget(Transform currentTarget)
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
}
