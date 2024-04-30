using Modules.Game.StateMachine.Scripts;
using Modules.Game.StateMachine.Scripts.States;
using UnityEngine;
using Zenject;

namespace Modules.Game
{
    public class Bootstrap : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Start()
        {
            _gameStateMachine.EnterIn<BootstrapState>();
        }
    }
}
