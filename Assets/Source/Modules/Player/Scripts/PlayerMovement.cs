using Modules.Configs.Scripts;
using Source.Modules.Input.Scripts;
using UnityEngine;

namespace Modules.Player.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private IInputService _inputService;
        private PlayerAttacker _playerAttacker;
        private float _targetMoveSpeed;
        private float _maxMoveSpeed;
        private float _currentMoveSpeed;
        private float _accelerationElapsedTime;
        private float _accelerationDuration;

        public bool IsMoving { get; private set; }
        public float MoveSpeed => _currentMoveSpeed;

        internal void Init(PlayerConfig config, IInputService inputService, PlayerAttacker playerAttacker) 
        {
            _inputService = inputService;
            _playerAttacker = playerAttacker;
            _maxMoveSpeed = config.MaxMoveSpeed;
            _accelerationDuration = config.AccelerationDuration / 10f;
        }

        private void Update()
        {
            MovementHandler();
        }

        private void MovementHandler()
        {
            Vector3 inputVectorNormalized = _inputService.MoveVector.normalized;
            Vector3 moveDir = new Vector3(inputVectorNormalized.x, 0f, inputVectorNormalized.y);

            if (inputVectorNormalized == Vector3.zero)
            {
                _targetMoveSpeed = 0f;
            }
            else 
            { 
                _targetMoveSpeed = _maxMoveSpeed;
                _accelerationElapsedTime = 0f;
            }

            Acceleration();

            if (_playerAttacker.EnemyTransform == null)
                Rotate(moveDir);
            else
                Rotate(_playerAttacker.EnemyTransform.position);

            transform.position += moveDir * _currentMoveSpeed * Time.deltaTime;

            CheckIsMoving();
        }

        private void Rotate(Vector3 targetRotation)
        {
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, targetRotation, Time.deltaTime * rotateSpeed);
        }

        private void Acceleration() 
        {
            _accelerationElapsedTime += Time.deltaTime;
            float percentageComplete = _accelerationElapsedTime / _accelerationDuration;
            _currentMoveSpeed = Mathf.Lerp(_currentMoveSpeed, _targetMoveSpeed, percentageComplete);
        }

        private void CheckIsMoving() => IsMoving = _currentMoveSpeed != 0f;
    }
}
