using Modules.Configs.Scripts;
using Modules.Services.Scripts;
using Source.Modules.Input.Scripts;
using UnityEngine;
using Zenject;

namespace Modules.Player.Scripts
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerHealth))]
    [RequireComponent(typeof(PlayerAttacker))]
    public class PlayerRoot : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerAttacker _playerAttacker;
        [SerializeField] private PlayerView _view;

        [Inject]
        private void Construct(PlayerConfig config, IInputService inputService, CameraMovement cameraMovement, IAudioService audioService)
        {
            _playerAttacker.Init(config, audioService);
            _playerHealth.Init(config);
            _movement.Init(config, inputService, _playerAttacker);
            _view.Init(config, _movement, _playerHealth);
            cameraMovement.Init(transform);
        }
    }
}
