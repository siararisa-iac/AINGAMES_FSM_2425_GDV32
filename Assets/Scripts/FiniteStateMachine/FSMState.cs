using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState
{
    public abstract void Run();
    public abstract void CheckTransition();

    // This collection will containe the conditions and what state it will transition to when the condition is met
    protected Dictionary<TransitionID, StateID> _transitionMap = new Dictionary<TransitionID, StateID>();
    // property, a getter property
    public StateID StateID => _stateID;
    // just a shortcut for:
    /*
    public StateID StateID
    {
        get
        {
            return _stateID;
        }
    }*/

    protected StateID _stateID;


    // Utility function to add/remove data from the dictionary

    public void AddTransition(TransitionID transitionId, StateID stateId)
    {
        // Validitiy checks
        if (transitionId == TransitionID.None || stateId == StateID.None)
        {
            return;
        }
        // Dictionaries can only have 1 unique key
        if (_transitionMap.ContainsKey(transitionId))
        {
            return;
        }
        // Data is now valid and can be added
        _transitionMap.Add(transitionId, stateId);
    }

    public void RemoveTransition(TransitionID transitionId)
    {
        if(transitionId == TransitionID.None)
        {
            return;
        }
        if (_transitionMap.ContainsKey(transitionId))
        {
            _transitionMap.Remove(transitionId);
        }
    }

    public StateID GetOutputState(TransitionID transitionId)
    {
        if (transitionId == TransitionID.None)
        {
            return StateID.None;
        }
        if (_transitionMap.ContainsKey(transitionId))
        {
            return _transitionMap[transitionId];
        }
        return StateID.None;
    }
}
