using UnityEngine;

public class PlayerStart : BaseState
{
    private GolfStateMachine _golfStateMachine;

    //Change this method name the same as the class name
    public PlayerStart(GolfStateMachine stateMachine)
    {
        _golfStateMachine = stateMachine;
    }

    public override void EnterState()
    {
        Debug.Log("Game Start!");
        //Initialize the mole object and mole UI
        _golfStateMachine.UIMoleThinkingText.SetActive(false);
        _golfStateMachine.MoleObject.gameObject.SetActive(false);
    }

    public override void UpdateState()
    {
        if (_golfStateMachine.PlayerController.isBallStopped())
        {
            _golfStateMachine.SetState(_golfStateMachine.PlayerAimingState);
        }
    }

    public override void ExitState()
    {

    }
}
