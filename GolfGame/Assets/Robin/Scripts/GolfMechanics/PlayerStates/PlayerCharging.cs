using UnityEngine;

public class PlayerCharging : BaseState
{
    private GolfStateMachine _golfStateMachine;

    //Change this method name the same as the class name
    public PlayerCharging(GolfStateMachine stateMachine)
    {
        _golfStateMachine = stateMachine;
    }

    public override void EnterState()
    {
        //Enter the charging state, start charging before shoot
        _golfStateMachine.PlayerSpriteMover.MovetoPlayer();
        Debug.Log("Start charging");
    }

    public override void UpdateState()
    {
        //Charging to look for better force to shoot
        _golfStateMachine.PlayerController.HandlingCharging();
        _golfStateMachine.PlayerController.HandlingAiming();

        if (!_golfStateMachine.PlayerController.isCharging)
            _golfStateMachine.SetState(_golfStateMachine.PlayerShootState);

        else if (_golfStateMachine.PlayerController.isFouledCharging)
        {
            Debug.Log("Fouled!!");
            _golfStateMachine.SetState(_golfStateMachine.MoleSetPositionState);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Finish charging");
        _golfStateMachine.PlayerController.FinishCharging();
        //_golfStateMachine.PlayerController.CanChangeDirection(false);
    }
}
