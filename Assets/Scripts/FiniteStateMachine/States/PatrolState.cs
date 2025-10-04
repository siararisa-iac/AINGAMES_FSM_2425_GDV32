using UnityEngine;

public class PatrolState : FSMState
{
    public override StateID StateID => StateID.Patrol;

    private Transform[] _waypoints;
    private Transform _currentTarget;
    private EnemyTankController _enemyTankController;

    public PatrolState(EnemyTankController enemyTankController, Transform[] waypoints)
    {
        _enemyTankController = enemyTankController;
        _waypoints = waypoints;
        RandomizeWaypointTarget();
    }

    public override void CheckTransition(Transform agent, Transform player)
    {
        if(Vector3.Distance(agent.position, player.position) <= _enemyTankController.ChaseDistance)
        {
            _enemyTankController.PerformTransition(TransitionID.SawPlayer);
        }
    }

    public override void Run(Transform agent, Transform player)
    {
        if(Vector3.Distance(agent.position, _currentTarget.position) <= _enemyTankController.WaypointDistance)
        {
            RandomizeWaypointTarget();
        }
        else
        {
            _enemyTankController.MoveToTarget(_currentTarget);
        }
    }

    private void RandomizeWaypointTarget()
    {
        // Randomize a value from the array.
        // Random.Range when int, max is exclusive so we can directly use the length of array
        int randomIndex = Random.Range(0, _waypoints.Length);

        // Ensure that the next randomized waypoint is unique
        while (_waypoints[randomIndex] == _currentTarget)
        {
            randomIndex = Random.Range(0, _waypoints.Length);
        }
        SetCurrentTarget(_waypoints[randomIndex]);
    }

    private void SetCurrentTarget(Transform target)
    {
        _currentTarget = target;
    }
}
