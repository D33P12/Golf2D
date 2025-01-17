using UnityEngine;

public class PlayerAiming : BaseState
{
    private GolfStateMachine _golfStateMachine;

    public PlayerAiming(GolfStateMachine stateMachine)
    {
        _golfStateMachine = stateMachine;
    }
    
    public override void EnterState()
    {
        _golfStateMachine.PlayerController.StartAiming();
    }

    public override void UpdateState()
    {
        _golfStateMachine.PlayerController.Aim();
    }

    public override void ExitState()
    {

    }
}
