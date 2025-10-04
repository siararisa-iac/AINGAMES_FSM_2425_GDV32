using System.Collections.Generic;
using UnityEngine;

public class AdvancedFiniteStateMachine : FiniteStateMachine
{
    public FSMState CurrentState => _currentState;

    private FSMState _currentState;
    private StateID _currentStateId;

    private Dictionary<StateID, FSMState> _states = new();

    /// <summary>
    /// Add a state to the state machine
    /// </summary>
    /// <param name="state"></param>
    public void AddState(FSMState state)
    {
        if (state == null)
        {
            return;
        }
        if (_states.Count == 0)
        {
            _states.Add(state.StateID, state);
            _currentState = state;
            _currentStateId = state.StateID;
            Debug.Log($"Current state: {_currentStateId}");
        }
        if (_states.ContainsKey(state.StateID))
        {
            return;
        }
        _states.Add(state.StateID, state);
    }

    public void RemoveState(FSMState state)
    {
        if (state == null)
        {
            return;
        }
        if (_states.ContainsKey(state.StateID))
        {
            _states.Remove(state.StateID);
        }
    }

    public void PerformTransition(TransitionID transitionID)
    {
        if(transitionID == TransitionID.None)
        {
            return;
        }

        StateID stateId = _currentState.GetOutputState(transitionID);
        if(stateId == StateID.None)
        {
            return;
        }
        _currentStateId = stateId;
        _currentState = _states[stateId];
    }

    protected override void Initialize()
    {
      
    }
}
