// Used for SimpleFSM
public enum State 
{ 
    Patrol,
    Chase,
    Attack
}


// Used for AdvancedFSM
public enum StateID
{
    None,
    Patrol,
    Chase,
    Attack
}

public enum TransitionID
{
    None,
    SawPlayer,
    ReachPlayer,
    LostPlayer
}
