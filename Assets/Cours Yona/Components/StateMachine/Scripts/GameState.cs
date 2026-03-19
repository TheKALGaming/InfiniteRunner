using UnityEngine;

public class GameState : State
{
    public GameState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {

        Debug.Log("Game started");
        EventSystem.OnPlayerLifeUpdated += HandlePlayerLifeUpdated;

    }

    public override void Update()
    {
        Debug.Log("Game updated");

    }

    public override void Exit()
    {
        Debug.Log("Game exit");
        EventSystem.OnPlayerLifeUpdated -= HandlePlayerLifeUpdated;

    }

    private void HandlePlayerLifeUpdated(int playerLife)
    {
        if (playerLife > 0)
        {
            return;
        }

        var gameOverState = new GameOverState(StateMachine);
        StateMachine.ChangeState(gameOverState);
    }
}
