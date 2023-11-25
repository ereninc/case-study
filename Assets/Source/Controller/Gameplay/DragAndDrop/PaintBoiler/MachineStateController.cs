using Sirenix.OdinInspector;

public class MachineStateController : ControllerBaseModel
{
    [ShowInInspector] private MachineState _currentState;
    
    private void ChangeState(MachineState state)
    {
        _currentState = state;
    }

    public MachineState GetState()
    {
        return _currentState;
    }

    public void SetIdle()
    {
        ChangeState(MachineState.Idle);
    }

    public void SetInProduction()
    {
        ChangeState(MachineState.InProduction);
    }
}

public enum MachineState
{
    Idle,
    InProduction
}