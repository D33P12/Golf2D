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
        _golfStateMachine.PlayerController.TurnStaticThenDynamic();
        //_golfStateMachine.PlayerController.IsLandStopped(false);
        _golfStateMachine.FieldObject.DropGridFromMole();
        _golfStateMachine.SetState(_golfStateMachine.PlayerAimingState);
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        //_golfStateMachine.PlayerController.IsLandStopped(true);
    }
}
