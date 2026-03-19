using UnityEngine;

public class GameOverState : State
{
    public GameOverState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Game over enter");
    }

    public override void Update()
    {
        Debug.Log("Game Over update");

    }

    public override void Exit()
    {
        Debug.Log("Game over exit");

    }
}
