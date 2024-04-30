using Modules.Game.StateMachine.Scripts.States;
using Modules.Services.Scripts;
using System;
using System.Collections.Generic;
using Zenject;

namespace Modules.Game.StateMachine.Scripts
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IGameState> _states;
        private IGameState _currentGameState;

        [Inject]
        public GameStateMachine(EnemySpawnService enemySpawnService, ISceneService sceneService, ITimerService timerService)
        {
            _states = new Dictionary<Type, IGameState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this),
                [typeof(GameState)] = new GameState(enemySpawnService, timerService, this),
                [typeof(EndGameState)] = new EndGameState(sceneService, timerService),
            };
        }

        public void EnterIn<T>() where T : IGameState
        {
            if (_states.TryGetValue(typeof(T), out IGameState gameState) == false) return;

            _currentGameState?.Exit();

            _currentGameState = gameState;
            _currentGameState.Enter();
        }
    }
}
