using UnityEngine;

public class MoleDig : BaseState
{
    private GolfStateMachine _golfStateMachine;

    //Change this method name the same as the class name
    public MoleDig(GolfStateMachine stateMachine)
    {
        _golfStateMachine = stateMachine;
    }

    public override void EnterState()
    {
        _golfStateMachine.MoleObject.gameObject.SetActive(true);
        _golfStateMachine.MoleObject.StartDigging();
    }

    public override void UpdateState()
    {
        if (_golfStateMachine.MoleObject.FinishedDigging())
        {
            _golfStateMachine.SetState(_golfStateMachine.LandFallState);
        }
    }

    public override void ExitState()
    {
        _golfStateMachine.MoleObject.gameObject.SetActive(false);
    }
}
