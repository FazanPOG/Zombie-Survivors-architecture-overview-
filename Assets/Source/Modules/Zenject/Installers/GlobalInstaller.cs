using Source.Modules.Input.Scripts;
using Zenject;
using UnityEngine;
using Modules.Configs.Scripts;
using UnityEngine.EventSystems;

namespace Modules.Zenject.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        [Header("Configs")]
        [SerializeField] private PlayerConfig _playerConfig;
        [Header("UI")]
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private FloatingJoystick _joystick;

        public override void InstallBindings()
        {
            BindConfigs();
            BindUI();
            BindInputHandler();
        }

        private void BindUI() 
        {
            Canvas canvas = BindCanvas();

            BindEventSystem();
            BindJoystickUI(canvas.transform);
        }

        private Canvas BindCanvas() 
        {
            var canvas = Container.InstantiatePrefabForComponent<Canvas>(_canvas);
            Container.Bind<Canvas>().FromInstance(canvas).AsSingle();
            return canvas;
        }

        private void BindEventSystem()
        {
            var eventSystem = Container.InstantiatePrefabForComponent<EventSystem>(_eventSystem);
            Container.Bind<EventSystem>().FromInstance(eventSystem).AsSingle();
        }

        private void BindJoystickUI(Transform rootCanvasTransform) 
        {
            var joystick = Container.InstantiatePrefabForComponent<FloatingJoystick>(_joystick, rootCanvasTransform);

            Container.Bind<FloatingJoystick>().FromInstance(joystick).AsSingle();
        }

        private void BindConfigs()
        {
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
        }

        private void BindInputHandler() => Container.Bind<IInputService>().To<InputService>().AsSingle();
    }
}
