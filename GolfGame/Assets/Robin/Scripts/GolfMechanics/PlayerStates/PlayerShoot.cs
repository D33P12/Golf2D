using UnityEngine;

public class PlayerShoot : BaseState
{
    private GolfStateMachine _golfStateMachine;

    //Change this method name the same as the class name
    public PlayerShoot(GolfStateMachine stateMachine)
    {
        _golfStateMachine = stateMachine;
    }

    public override void EnterState()
    {
        _golfStateMachine.PlayerController.ShootBall();
    }

    public override void UpdateState()
    {
        if (_golfStateMachine.PlayerController.isBallStopped())
        {
            _golfStateMachine.SetState(_golfStateMachine.MoleSetPositionState);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Finish shooting");
    }
}
