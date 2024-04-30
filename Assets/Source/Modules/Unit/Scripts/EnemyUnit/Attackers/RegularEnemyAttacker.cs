using Modules.Configs.Scripts;
using Modules.Player.Scripts;
using Modules.Unit.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Modules.Unit.Enemy.Scripts
{
    public class RegularEnemyAttacker : IUnitAttacker
    {
        private EnemyUnitConfig _config;
        private Transform _unitTransform;
        private Transform _playerTransform;
        private int _defaultDamage;
        private bool _attackOnDelay;

        public event Action OnAttackStarted;
        public event Action OnAttackEnded;

        public RegularEnemyAttacker(EnemyUnitConfig config, Transform unitTransform, Transform playerTransform)
        {
            _config = config;
            _unitTransform = unitTransform;
            _playerTransform = playerTransform;

            _defaultDamage = _config.DefaultDamage;
        }

        public void Attack()
        {
            if (_playerTransform.TryGetComponent(out IDamagable damagable))
                damagable.TakeDamage(_defaultDamage);
            else
                throw new MissingComponentException($"Missing {nameof(IDamagable)} component");

            _attackOnDelay = true;
            OnAttackStarted?.Invoke();
        }

        public bool CanAttack()
        {
            return Vector3.Distance(_unitTransform.position, _playerTransform.position) <= _config.AttackRange 
                && _attackOnDelay == false; 
        }

        public IEnumerator AttackDelay()
        {
            yield return new WaitForSeconds(_config.AttackDelay);
            OnAttackEnded?.Invoke();
            _attackOnDelay = false;
        }
    }
}
