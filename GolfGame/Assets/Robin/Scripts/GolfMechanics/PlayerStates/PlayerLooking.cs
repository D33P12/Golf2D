using UnityEngine;

public class PlayerLooking : BaseState
{
    private GolfStateMachine _golfStateMachine;

    public PlayerLooking(GolfStateMachine stateMachine)
    {
        _golfStateMachine = stateMachine;
    }

    public override void EnterState()
    {
        //Enter Looking state, we look around
        Debug.Log("Start looking");

        _golfStateMachine.PlayerController.LookingInitialization();
    }

    public override void UpdateState()
    {
        //During the looking state, we press arrow keys for aiming
        _golfStateMachine.PlayerController.HandlingLooking();

        if (!_golfStateMachine.PlayerController.isLooking)  // if release x
            _golfStateMachine.SetState(_golfStateMachine.PlayerAimingState);    //go back to aiming state
    }

    public override void ExitState()
    {
        Debug.Log("Finish looking");
    }
}
