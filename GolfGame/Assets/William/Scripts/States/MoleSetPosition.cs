using UnityEngine;

public class MoleSetPosition : BaseState
{
    private GolfStateMachine _golfStateMachine;

    //Change this method name the same as the class name
    public MoleSetPosition(GolfStateMachine stateMachine)
    {
        _golfStateMachine = stateMachine;
    }

    public override void EnterState()
    {
        Debug.Log("MoleSetPosition");
        _golfStateMachine.PathfinderObject.Pathfind();
        _golfStateMachine.UIMoleThinkingText.SetActive(true);
        _golfStateMachine.PlayerController.SetPathObjectRb(true);
    }

    public override void UpdateState()
    {
        if (_golfStateMachine.PathfinderObject.FinishedRunning())
        {
            _golfStateMachine.SetState(_golfStateMachine.MoleDigState);
        }
    }

    public override void ExitState()
    {
        _golfStateMachine.UIMoleThinkingText.SetActive(false);
        _golfStateMachine.PlayerController.SetPathObjectRb(false);
    }
}
