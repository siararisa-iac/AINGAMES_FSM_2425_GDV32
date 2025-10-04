using UnityEngine;

public abstract class FiniteStateMachine : MonoBehaviour
{
    protected abstract void Initialize();
    protected virtual void UpdateFiniteStateMachine()
    {
        Debug.Log("This is update from base class");
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        UpdateFiniteStateMachine();
    }
}
