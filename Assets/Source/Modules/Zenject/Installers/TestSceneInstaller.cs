using Modules.Configs.Scripts;
using Modules.Game.StateMachine.Scripts;
using Modules.Services.Scripts;
using UnityEngine;
using Zenject;

namespace Modules.Zenject.Installers
{
    public class TestSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _environment;
        [SerializeField] private EnemiesData _enemiesData;
        [SerializeField] private ClipReferences _clipReferences;
        [SerializeField] private AudioSource _mainAudioSource;
        [SerializeField] private TimerService _timerServicePrefab;

        public override void InstallBindings()
        {
            BindServices();
            BindEnemiesConfigReferences();
            BindGameStateMachine();
            InstantiateEnvironment();
        }

        private void InstantiateEnvironment() => Container.InstantiatePrefab(_environment);

        private void BindGameStateMachine() 
        { 
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
        }

        private void BindEnemiesConfigReferences() => Container.BindInstance(_enemiesData).AsSingle().NonLazy();

        private void BindServices() 
        {
            BindEnemySpawnService();
            BindAudioService();
            BindSceneService();
            BindTimerService();
        }

        private void BindEnemySpawnService() 
        {
            Container.Bind<EnemySpawnService>().FromNew().AsSingle().NonLazy();
        }

        private void BindAudioService() 
        {
            var instance = Container.InstantiatePrefabForComponent<AudioSource>(_mainAudioSource);
            Container.Bind<AudioSource>().FromInstance(instance).AsSingle();

            Container.Bind<ClipReferences>().FromInstance(_clipReferences).AsSingle();
            Container.Bind<IAudioService>().To<AudioService>().FromNew().AsSingle();
        }

        private void BindSceneService() 
        {
            Container.Bind<ISceneService>().To<SceneService>().FromNew().AsSingle();
        }

        private void BindTimerService() 
        {
            var instance = Container.InstantiatePrefabForComponent<TimerService>(_timerServicePrefab);
            Container.Bind<ITimerService>().To<TimerService>().FromInstance(instance).AsSingle();
        }
    }
}