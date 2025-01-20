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
        Debug.Log("Shoot!");
        _golfStateMachine.PlayerController.ShootBall();
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {
        Debug.Log("Finish Shooting");
    }
}
