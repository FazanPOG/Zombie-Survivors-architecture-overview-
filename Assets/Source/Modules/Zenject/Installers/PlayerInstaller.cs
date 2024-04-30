using Modules.Player.Scripts;
using Modules.UI.Scripts;
using UnityEngine;
using Zenject;

namespace Modules.Zenject.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerRoot _player;
        [SerializeField] private CameraMovement _camera;
        [Header("UI")]
        [SerializeField] private RectTransform _playerStatsUI;
        [SerializeField] private PlayerHealthUI _playerHealthUI;
        [SerializeField] private PlayerStaminaUI _playerStaminaUI;

        [Inject] private Canvas _rootCanvas;

        public override void InstallBindings()
        {
            BindCamera();
            BindPlayer();
            
            BindPlayerStatsUI(_rootCanvas.transform);
        }

        private void BindPlayer() 
        {
            var playerInstance = Container.InstantiatePrefabForComponent<PlayerRoot>(_player);
            Container.Bind<PlayerRoot>()
                .FromInstance(playerInstance)
                .AsSingle();
        }

        private void BindCamera() 
        {
            var camera = Container.InstantiatePrefabForComponent<CameraMovement>(_camera);
            Container.Bind<CameraMovement>()
                .FromInstance(camera)
                .AsSingle();
        }

        private void BindPlayerStatsUI(Transform rootCanvasTransform)
        {
            var playerStatsUI = Container.InstantiatePrefab(_playerStatsUI, rootCanvasTransform);

            BindPlayerHealthUI(playerStatsUI.transform);
            BindPlayerStaminaUI(playerStatsUI.transform);
        }

        private void BindPlayerHealthUI(Transform rootCanvasTransform)
        {
            var playerHealthUI = Container.InstantiatePrefabForComponent<PlayerHealthUI>(_playerHealthUI, rootCanvasTransform);

            Container.Bind<PlayerHealthUI>()
                .FromInstance(playerHealthUI)
                .AsSingle();
        }

        private void BindPlayerStaminaUI(Transform rootCanvasTransform)
        {
            var playerStaminaUI = Container.InstantiatePrefabForComponent<PlayerStaminaUI>(_playerStaminaUI, rootCanvasTransform);

            Container.Bind<PlayerStaminaUI>().
                FromInstance(playerStaminaUI).
                AsSingle();
        }
    }
}
