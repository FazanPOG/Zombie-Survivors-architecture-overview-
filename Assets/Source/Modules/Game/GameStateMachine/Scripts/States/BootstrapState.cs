namespace Modules.Game.StateMachine.Scripts.States
{
    public class BootstrapState : IGameState
    {
        private IGameStateMachine _gameStateMachine;

        public BootstrapState(IGameStateMachine gameStateMachine) 
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _gameStateMachine.EnterIn<GameState>();
        }

        public void Exit()
        {

        }
    }
}
