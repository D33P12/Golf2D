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
        //Enter the aiming state, we start aiming
        Debug.Log("Start aiming");

        _golfStateMachine.PlayerController.StartAiming();
    }

    public override void UpdateState()
    {
        //During the aiming state, we press left/right for aiming
        _golfStateMachine.PlayerController.HandlingAiming();

        if (_golfStateMachine.PlayerController.isCharging)
        {
            _golfStateMachine.SetState(_golfStateMachine.PlayerChargingState);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Finish aiming");
    }
}
