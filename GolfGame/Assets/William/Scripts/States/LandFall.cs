using UnityEngine;

public class LandFall : BaseState
{
    private GolfStateMachine _golfStateMachine;

    //Change this method name the same as the class name
    public LandFall(GolfStateMachine stateMachine)
    {
        _golfStateMachine = stateMachine;
    }

    public override void EnterState()
    {
        //_golfStateMachine.PlayerController.TurnStaticThenDynamic();
        _golfStateMachine.FieldObject.DropGridFromMole();
    }

    public override void UpdateState()
    {
        if (_golfStateMachine.PlayerController.isBallStopped())
            _golfStateMachine.SetState(_golfStateMachine.PlayerAimingState);

        _golfStateMachine.SetState(_golfStateMachine.PlayerAimingState);
    }

    public override void ExitState()
    {

    }
}
