using Modules.Services.Scripts;

namespace Modules.Game.StateMachine.Scripts.States
{
    public class EndGameState : IGameState
    {
        private ISceneService _sceneService;
        private ITimerService _timerService;

        public EndGameState(ISceneService sceneService, ITimerService timerService) 
        {
            _sceneService = sceneService;
            _timerService = timerService;
        }

        public void Enter()
        {
            float reloadSceneDelay = 2f;
            _timerService.StartTimer(reloadSceneDelay, _sceneService.ReloadCurrentScene);
        }

        public void Exit()
        {

        }
    }
}
