using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State _currentState;
    protected State CurrentState => _currentState;

    private void Update()
    {
        _currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }
}
