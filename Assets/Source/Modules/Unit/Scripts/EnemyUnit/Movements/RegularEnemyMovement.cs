using Modules.Configs.Scripts;
using Modules.Unit.Interfaces;
using System;
using UnityEngine;

namespace Modules.Unit.Enemy.Scripts
{
    public class RegularEnemyMovement : IUnitMovement, IUnitSpeed, IDisposable
    {
        private IUnitAttacker _attacker;
        private IDamagable _health;
        private Transform _unitTransform;
        private Transform _playerTransform;
        private Vector3 _moveTarget;
        private float _maxMoveSpeed;
        private float _currentMoveSpeed;
        private float _accelerationElapsedTime;
        private float _accelerationDuration;
        private bool _canMove = true;

        public float CurrentSpeed => _currentMoveSpeed;
        public float MaxSpeed => _maxMoveSpeed;

        public RegularEnemyMovement(EnemyUnitConfig config, IUnitAttacker attacker, IDamagable health, Transform unitTransform, Transform playerTransform)
        {
            _attacker = attacker;
            _health = health;
            _unitTransform = unitTransform;
            _playerTransform = playerTransform;
            _maxMoveSpeed = config.MoveSpeed;
            _accelerationDuration = config.AcceleretionDuration;

            _attacker.OnAttackStarted += StopMove;
            _attacker.OnAttackEnded += StartMove;
            _health.OnDied += StopMove;
        }

        public void StopMove() => _canMove = false;
        private void StartMove() => _canMove = true;

        public void MoveToTarget()
        {
            if (_canMove == false) 
            {
                _currentMoveSpeed = 0f;
                return;
            }

            SetMoveTarget(_playerTransform.position);

            Acceleration();
            Rotate(_playerTransform.position);

            _unitTransform.position = Vector3.MoveTowards(_unitTransform.position, _moveTarget, _currentMoveSpeed * Time.deltaTime);
        }

        public void SetMoveTarget(Vector3 target)
        {
            _moveTarget = target;
        }

        private void Rotate(Vector3 target)
        {
            Vector3 targetDirection = target - _unitTransform.position;

            float rotateSpeed = 10f;
            float singleStep = rotateSpeed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(_unitTransform.forward, targetDirection, singleStep, 0.0f);

            Debug.DrawRay(_unitTransform.position, newDirection, Color.red);

            _unitTransform.rotation = Quaternion.LookRotation(newDirection);
        }

        private void Acceleration()
        {
            _accelerationElapsedTime += Time.deltaTime;
            if (_accelerationElapsedTime > _accelerationDuration) _accelerationElapsedTime = _accelerationDuration;

            float percentageComplete = _accelerationElapsedTime / _accelerationDuration;
            _currentMoveSpeed = Mathf.Lerp(_currentMoveSpeed, _maxMoveSpeed, percentageComplete);
        }

        public void Dispose()
        {
            _attacker.OnAttackStarted -= StartMove;
            _attacker.OnAttackEnded -= StopMove;
            _health.OnDied -= StopMove;
        }
    }
}
