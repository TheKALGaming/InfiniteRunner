using UnityEngine;

public class GameState : State
{
    public GameState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {

        Debug.Log("Game started");

    }

    public override void Update()
    {
        Debug.Log("Game updated");

    }

    public override void Exit()
    {
        Debug.Log("Game exit");

    }
}
