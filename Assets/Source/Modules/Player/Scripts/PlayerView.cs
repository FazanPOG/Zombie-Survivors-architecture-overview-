using Modules.Configs.Scripts;
using UnityEngine;

namespace Modules.Player.Scripts
{
    public class PlayerView : MonoBehaviour
    {
        private const string MoveSpeed = nameof(MoveSpeed);
        private const string Dead = nameof(Dead);

        private PlayerMovement _playerMovement;
        private PlayerHealth _playerHealth;
        private float _maxMoveSpeed;
        private Animator _animator;

        internal void Init(PlayerConfig config, PlayerMovement playerMovement, PlayerHealth playerHealth)
        {
            _maxMoveSpeed = config.MaxMoveSpeed;
            _playerMovement = playerMovement;
            _playerHealth = playerHealth;

            _playerHealth.OnDied += PlayerHealth_OnDied;
        }

        private void PlayerHealth_OnDied() => _animator.SetTrigger(Dead);

        private void Awake() => _animator = GetComponent<Animator>();

        private void Update()
        {
            _animator.SetFloat(MoveSpeed, _playerMovement.MoveSpeed / _maxMoveSpeed);
        }
    }
}