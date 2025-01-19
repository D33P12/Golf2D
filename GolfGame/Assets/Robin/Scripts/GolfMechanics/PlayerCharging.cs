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
        Debug.Log("Start charging");

        _golfStateMachine.PlayerController.StartCharging();
    }

    public override void UpdateState()
    {
        //Charging to look for better force to shoot
        _golfStateMachine.PlayerController.HandlingCharging();

        if (!_golfStateMachine.PlayerController.isCharging)
        {
            _golfStateMachine.SetState(_golfStateMachine.PlayerShootState);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Finish charging");
    }
}
